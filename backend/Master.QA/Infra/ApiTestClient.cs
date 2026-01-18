using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ApiTestFramework
{
    /// <summary>
    /// Flexible API client for testing REST endpoints with comprehensive HTTP method support
    /// </summary>
    public class ApiTestClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _defaultHeaders;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiTestClient(string baseUrl, TimeSpan? timeout = null)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = timeout ?? TimeSpan.FromSeconds(30)
            };

            _defaultHeaders = new Dictionary<string, string>();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// Set default headers that will be included in all requests
        /// </summary>
        public void SetDefaultHeader(string key, string value)
        {
            _defaultHeaders[key] = value;
        }

        /// <summary>
        /// Set authorization header (Bearer token)
        /// </summary>
        public void SetBearerToken(string token)
        {
            _defaultHeaders["Authorization"] = $"Bearer {token}";
        }

        /// <summary>
        /// Set basic authentication
        /// </summary>
        public void SetBasicAuth(string username, string password)
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            _defaultHeaders["Authorization"] = $"Basic {credentials}";
        }

        /// <summary>
        /// Clear all default headers
        /// </summary>
        public void ClearDefaultHeaders()
        {
            _defaultHeaders.Clear();
        }

        /// <summary>
        /// GET request returning deserialized object
        /// </summary>
        public async Task<ApiResponse<T>> GetAsync<T>(string endpoint, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Get, endpoint, headers);
            return await SendRequestAsync<T>(request);
        }

        /// <summary>
        /// GET request returning raw response
        /// </summary>
        public async Task<ApiResponse> GetAsync(string endpoint, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Get, endpoint, headers);
            return await SendRequestAsync(request);
        }

        /// <summary>
        /// POST request with body
        /// </summary>
        public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Post, endpoint, headers, body);
            return await SendRequestAsync<T>(request);
        }

        /// <summary>
        /// POST request returning raw response
        /// </summary>
        public async Task<ApiResponse> PostAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Post, endpoint, headers, body);
            return await SendRequestAsync(request);
        }

        /// <summary>
        /// PUT request with body
        /// </summary>
        public async Task<ApiResponse<T>> PutAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Put, endpoint, headers, body);
            return await SendRequestAsync<T>(request);
        }

        /// <summary>
        /// PUT request returning raw response
        /// </summary>
        public async Task<ApiResponse> PutAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Put, endpoint, headers, body);
            return await SendRequestAsync(request);
        }

        /// <summary>
        /// PATCH request with body
        /// </summary>
        public async Task<ApiResponse<T>> PatchAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Patch, endpoint, headers, body);
            return await SendRequestAsync<T>(request);
        }

        /// <summary>
        /// PATCH request returning raw response
        /// </summary>
        public async Task<ApiResponse> PatchAsync(string endpoint, object? body = null, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Patch, endpoint, headers, body);
            return await SendRequestAsync(request);
        }

        /// <summary>
        /// DELETE request
        /// </summary>
        public async Task<ApiResponse<T>> DeleteAsync<T>(string endpoint, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Delete, endpoint, headers);
            return await SendRequestAsync<T>(request);
        }

        /// <summary>
        /// DELETE request returning raw response
        /// </summary>
        public async Task<ApiResponse> DeleteAsync(string endpoint, Dictionary<string, string>? headers = null)
        {
            var request = CreateRequest(HttpMethod.Delete, endpoint, headers);
            return await SendRequestAsync(request);
        }

        /// <summary>
        /// Upload file with multipart/form-data
        /// </summary>
        public async Task<ApiResponse<T>> UploadFileAsync<T>(string endpoint, string filePath, string fileFieldName = "file", Dictionary<string, string>? formData = null, Dictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            ApplyHeaders(request, headers);

            var content = new MultipartFormDataContent();

            var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
            content.Add(fileContent, fileFieldName, Path.GetFileName(filePath));

            if (formData != null)
            {
                foreach (var kvp in formData)
                {
                    content.Add(new StringContent(kvp.Value), kvp.Key);
                }
            }

            request.Content = content;
            return await SendRequestAsync<T>(request);
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string endpoint, Dictionary<string, string>? headers, object? body = null)
        {
            var request = new HttpRequestMessage(method, endpoint);
            ApplyHeaders(request, headers);

            if (body != null)
            {
                var json = JsonSerializer.Serialize(body, _jsonOptions);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return request;
        }

        private void ApplyHeaders(HttpRequestMessage request, Dictionary<string, string>? headers)
        {
            foreach (var header in _defaultHeaders)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }

        private async Task<ApiResponse<T>> SendRequestAsync<T>(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            var apiResponse = new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode,
                RawContent = content,
                Headers = response.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value))
            };

            if (response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(content))
            {
                try
                {
                    apiResponse.Data = JsonSerializer.Deserialize<T>(content, _jsonOptions);
                }
                catch (JsonException ex)
                {
                    apiResponse.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else if (!response.IsSuccessStatusCode)
            {
                apiResponse.ErrorMessage = $"Request failed with status {response.StatusCode}: {content}";
            }

            return apiResponse;
        }

        private async Task<ApiResponse> SendRequestAsync(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return new ApiResponse
            {
                StatusCode = response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode,
                RawContent = content,
                Headers = response.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value)),
                ErrorMessage = response.IsSuccessStatusCode ? null : $"Request failed with status {response.StatusCode}: {content}"
            };
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

    /// <summary>
    /// Generic API response wrapper
    /// </summary>
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string RawContent { get; set; } = string.Empty;
        public Dictionary<string, string> Headers { get; set; } = new();
        public string? ErrorMessage { get; set; }
    }

    /// <summary>
    /// Non-generic API response wrapper
    /// </summary>
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string RawContent { get; set; } = string.Empty;
        public Dictionary<string, string> Headers { get; set; } = new();
        public string? ErrorMessage { get; set; }
    }


    // Example usage in MSTest
    /*
    [TestClass]
    public class ApiTests
    {
        private ApiTestClient _client = null!;

        [TestInitialize]
        public void Setup()
        {
            _client = new ApiTestClient("https://api.example.com");
            _client.SetBearerToken("your-token-here");
            _client.SetDefaultHeader("X-Custom-Header", "value");
        }

        [TestMethod]
        public async Task GetUser_ReturnsValidUser()
        {
            var response = await _client.GetAsync<User>("/users/1");
        
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(1, response.Data.Id);
        }

        [TestMethod]
        public async Task CreateUser_WithValidData_ReturnsCreated()
        {
            var newUser = new { Name = "John Doe", Email = "john@example.com" };
            var response = await _client.PostAsync<User>("/users", newUser);
        
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Data);
        }

        [TestMethod]
        public async Task UpdateUser_ReturnsSuccess()
        {
            var updateData = new { Name = "Jane Doe" };
            var response = await _client.PutAsync("/users/1", updateData);
        
            Assert.IsTrue(response.IsSuccess);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _client?.Dispose();
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
    */
}