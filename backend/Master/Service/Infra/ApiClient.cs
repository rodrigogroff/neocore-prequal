using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Master.Service.Infra
{
    public class ApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly bool _disposeHttpClient;
        private bool _disposed;

        // Parameterless constructor for flexible usage
        public ApiClient()
            : this(new HttpClient(), token: null, disposeHttpClient: true)
        {
        }

        // Constructor for manual instantiation with base URL
        public ApiClient(string baseUrl, string token = null, TimeSpan? timeout = null)
            : this(CreateHttpClient(baseUrl, timeout), token, disposeHttpClient: true)
        {
        }

        // Constructor for dependency injection (preferred)
        public ApiClient(HttpClient httpClient, string token = null, bool disposeHttpClient = false)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _disposeHttpClient = disposeHttpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            if (!string.IsNullOrWhiteSpace(token))
            {
                SetBearerToken(token);
            }
        }

        private static HttpClient CreateHttpClient(string baseUrl, TimeSpan? timeout)
        {
            var client = new HttpClient
            {
                Timeout = timeout ?? TimeSpan.FromSeconds(30)
            };

            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                client.BaseAddress = new Uri(baseUrl);
            }

            return client;
        }

        public void SetBearerToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token cannot be null or empty", nameof(token));

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public Task<ApiResponse<T>> GetAsync<T>(string url)
            => GetAsync<T>(url, CancellationToken.None);

        public async Task<ApiResponse<T>> GetAsync<T>(string url, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be null or empty", nameof(url));

            var response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            return await ProcessResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
        }

        public Task<ApiResponse<T>> PostAsync<T>(string url, object? body = null)
            => PostAsync<T>(url, body, CancellationToken.None);

        public async Task<ApiResponse<T>> PostAsync<T>(string url, object? body, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be null or empty", nameof(url));

            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (body != null)
            {
                var json = JsonSerializer.Serialize(body, _jsonOptions);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            return await ProcessResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes multiple requests simultaneously and waits for all to complete (same type)
        /// </summary>
        public Task<ApiResponse<T>[]> WaitForAllAsync<T>(params string[] urls)
            => WaitForAllAsync<T>(urls, CancellationToken.None);

        /// <summary>
        /// Executes multiple requests simultaneously and waits for all to complete (same type)
        /// </summary>
        public async Task<ApiResponse<T>[]> WaitForAllAsync<T>(IEnumerable<string> urls, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            var urlList = urls?.ToList() ?? throw new ArgumentNullException(nameof(urls));

            if (!urlList.Any())
                return Array.Empty<ApiResponse<T>>();

            var tasks = urlList.Select(url => GetAsync<T>(url, cancellationToken));
            return await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes multiple tasks simultaneously and waits for all to complete (same type)
        /// </summary>
        public async Task<ApiResponse<T>[]> WaitForAllAsync<T>(params Task<ApiResponse<T>>[] tasks)
        {
            ThrowIfDisposed();

            if (tasks == null || tasks.Length == 0)
                return Array.Empty<ApiResponse<T>>();

            return await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes multiple POST requests simultaneously and waits for all to complete (same type)
        /// </summary>
        public async Task<ApiResponse<T>[]> WaitForAllPostAsync<T>(
            IEnumerable<(string url, object? body)> requests,
            CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            var requestList = requests?.ToList() ?? throw new ArgumentNullException(nameof(requests));

            if (!requestList.Any())
                return Array.Empty<ApiResponse<T>>();

            var tasks = requestList.Select(req => PostAsync<T>(req.url, req.body, cancellationToken));
            return await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        private async Task<ApiResponse<T>> ProcessResponseAsync<T>(
            HttpResponseMessage response,
            CancellationToken cancellationToken)
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

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
                    apiResponse.IsSuccess = false;
                    apiResponse.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else if (!response.IsSuccessStatusCode)
            {
                apiResponse.ErrorMessage = $"Request failed with status {response.StatusCode}: {content}";
            }

            return apiResponse;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ApiClient));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing && _disposeHttpClient)
                {
                    _httpClient?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string RawContent { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
    }
}