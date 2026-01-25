using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace Master.Service.Infra
{
    public class ApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        public ApiClient(string baseUrl, string token = null, TimeSpan? timeout = null)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = timeout ?? TimeSpan.FromSeconds(30)
            };
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
        /// <summary>
        /// Set Bearer token for authorization
        /// </summary>
        public void SetBearerToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
        /// <summary>
        /// GET request
        /// </summary>
        public async Task<ApiResponse<T>> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode,
                RawContent = content
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
        /// <summary>
        /// POST request with body
        /// </summary>
        public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object? body = null)
        {
            Console.WriteLine("PostAsync");

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            if (body != null)
            {
                var json = JsonSerializer.Serialize(body, _jsonOptions);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode,
                RawContent = content
            };
            if (response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("PostAsync Success " + content);

                try
                {
                    apiResponse.Data = JsonSerializer.Deserialize<T>(content, _jsonOptions);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("PostAsync FAIL " + ex.ToString());

                    apiResponse.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }

                
            }
            else if (!response.IsSuccessStatusCode)
            {
                apiResponse.ErrorMessage = $"Request failed with status {response.StatusCode}: {content}";

                Console.WriteLine(apiResponse.ErrorMessage);

            }

            Console.WriteLine("PostAsync OK!");

            return apiResponse;
        }
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
    /// <summary>
    /// API response wrapper
    /// </summary>
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string RawContent { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
    }
}
/*
 * 
var client = new ApiClient("https://api.example.com");
client.SetBearerToken("your-token-here");

// GET example
var getResponse = await client.GetAsync<User>("/users/123");
if (getResponse.IsSuccess)
{
    Console.WriteLine($"User: {getResponse.Data?.Name}");
}

// POST example
var data = new { Name = "John", Email = "john@example.com" };
var response = await client.PostAsync<User>("/users", data);
if (response.IsSuccess)
{
    Console.WriteLine($"Created user: {response.Data?.Name}");
}
 * 
 * */