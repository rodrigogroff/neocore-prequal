using ApiTestFramework;
using Master.Entity.Dto.Response.Domain.Auth;
using Master.QA.Infra;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Master.QA.Capacity.UseCase
{
    [TestClass]
    public sealed class LoginQACapacity : BaseQATestClass
    {
        private ApiTestClient _client = null!;

        [TestInitialize]
        public void Setup()
        {
            _client = new ApiTestClient(MasterUrl);
            // Optimize for high concurrent connections
            System.Net.ServicePointManager.DefaultConnectionLimit = 1000;
            ThreadPool.SetMinThreads(100, 100);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _client?.Dispose();
        }

        [TestMethod]
        public async Task OK()
        {
            var loginData = this.LoginDataOk;
            var response = await _client.PostAsync<DtoResponseToken>("/api/authenticate", loginData);
            Assert.IsTrue(response.IsSuccess);
        }

        [TestMethod]
        [DataRow(1, 10)]   // 1 request/sec for 10 seconds
        [DataRow(2, 10)]   // 2 requests/sec for 10 seconds
        [DataRow(3, 10)]   // 3 requests/sec for 10 seconds
        [DataRow(5, 10)]   // 5 requests/sec for 10 seconds
        [DataRow(10, 10)]  // 10 requests/sec for 10 seconds
        [DataRow(15, 10)]  // 15 requests/sec for 10 seconds
        public async Task CapacityTest_RequestsPerSecond(int requestsPerSecond, int durationSeconds)
        {
            var loginData = this.LoginDataOk;
            var results = new ConcurrentBag<LoadTestResult>();
            var stopwatch = Stopwatch.StartNew();
            var totalRequests = requestsPerSecond * durationSeconds;
            var delayBetweenRequests = TimeSpan.FromMilliseconds(1000.0 / requestsPerSecond);

            Console.WriteLine($"Starting capacity test: {requestsPerSecond} req/sec for {durationSeconds} seconds");
            Console.WriteLine($"Total expected requests: {totalRequests}");
            Console.WriteLine($"Delay between requests: {delayBetweenRequests.TotalMilliseconds:F2}ms");
            Console.WriteLine($"Max thread count: {Environment.ProcessorCount}");

            var tasks = new List<Task>();
            var requestCounter = 0;

            for (int second = 0; second < durationSeconds; second++)
            {
                for (int req = 0; req < requestsPerSecond; req++)
                {
                    var requestId = ++requestCounter;
                    var task = Task.Run(async () =>
                    {
                        var client = new ApiTestClient(MasterUrl);
                        var requestStopwatch = Stopwatch.StartNew();

                        try
                        {
                            var response = await client.PostAsync<DtoResponseToken>("/api/authenticate", loginData);
                            requestStopwatch.Stop();

                            results.Add(new LoadTestResult
                            {
                                RequestId = requestId,
                                Success = response.IsSuccess,
                                StatusCode = (int)response.StatusCode,
                                ResponseTime = requestStopwatch.ElapsedMilliseconds,
                                ErrorMessage = response.ErrorMessage
                            });
                        }
                        catch (Exception ex)
                        {
                            requestStopwatch.Stop();
                            results.Add(new LoadTestResult
                            {
                                RequestId = requestId,
                                Success = false,
                                StatusCode = 0,
                                ResponseTime = requestStopwatch.ElapsedMilliseconds,
                                ErrorMessage = ex.Message
                            });
                        }
                        finally
                        {
                            client.Dispose();
                        }
                    });

                    tasks.Add(task);

                    // Pace the requests
                    if (req < requestsPerSecond - 1)
                    {
                        await Task.Delay(delayBetweenRequests);
                    }
                }

                // Wait until the end of the second before starting the next batch
                var elapsed = stopwatch.Elapsed.TotalSeconds;
                var waitTime = (second + 1) - elapsed;
                if (waitTime > 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(waitTime));
                }
            }

            // Wait for all requests to complete
            await Task.WhenAll(tasks);
            stopwatch.Stop();

            // Analyze results
            var successCount = results.Count(r => r.Success);
            var failureCount = results.Count(r => !r.Success);
            var avgResponseTime = results.Average(r => r.ResponseTime);
            var minResponseTime = results.Min(r => r.ResponseTime);
            var maxResponseTime = results.Max(r => r.ResponseTime);
            var p95ResponseTime = CalculatePercentile(results.Select(r => r.ResponseTime).ToList(), 95);
            var p99ResponseTime = CalculatePercentile(results.Select(r => r.ResponseTime).ToList(), 99);

            // Print detailed results
            Console.WriteLine("\n========== CAPACITY TEST RESULTS ==========");
            Console.WriteLine($"Total Duration: {stopwatch.Elapsed.TotalSeconds:F2}s");
            Console.WriteLine($"Total Requests: {results.Count}");
            Console.WriteLine($"Successful: {successCount} ({(successCount * 100.0 / results.Count):F2}%)");
            Console.WriteLine($"Failed: {failureCount} ({(failureCount * 100.0 / results.Count):F2}%)");
            Console.WriteLine($"Actual req/sec: {results.Count / stopwatch.Elapsed.TotalSeconds:F2}");
            Console.WriteLine("\nResponse Times (ms):");
            Console.WriteLine($"  Min: {minResponseTime}ms");
            Console.WriteLine($"  Avg: {avgResponseTime:F2}ms");
            Console.WriteLine($"  Max: {maxResponseTime}ms");
            Console.WriteLine($"  P95: {p95ResponseTime:F2}ms");
            Console.WriteLine($"  P99: {p99ResponseTime:F2}ms");

            if (failureCount > 0)
            {
                Console.WriteLine("\nFailure Details:");
                var errorGroups = results.Where(r => !r.Success)
                    .GroupBy(r => r.ErrorMessage ?? "Unknown Error")
                    .OrderByDescending(g => g.Count());

                foreach (var group in errorGroups.Take(5))
                {
                    Console.WriteLine($"  {group.Key}: {group.Count()} occurrences");
                }
            }

            Console.WriteLine("==========================================\n");

            // Assert that at least 95% of requests succeeded
            var successRate = successCount * 100.0 / results.Count;
            Assert.IsTrue(successRate >= 95, $"Success rate {successRate:F2}% is below 95% threshold");
        }

        private double CalculatePercentile(List<long> values, int percentile)
        {
            if (values.Count == 0) return 0;

            values.Sort();
            var index = (int)Math.Ceiling((percentile / 100.0) * values.Count) - 1;
            return values[Math.Max(0, Math.Min(index, values.Count - 1))];
        }

        private class LoadTestResult
        {
            public int RequestId { get; set; }
            public bool Success { get; set; }
            public int StatusCode { get; set; }
            public long ResponseTime { get; set; }
            public string? ErrorMessage { get; set; }
            public int WorkerId { get; set; }
        }
    }
}
