using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Master.Service.Base.Infra.Helper
{
    public class HelperApiClient : IDisposable
    {
        #region Constants

        // Error Messages
        private const string ErrorTokenNullOrEmpty = "Token cannot be null or empty";
        private const string ErrorHeaderNameNullOrEmpty = "Header name cannot be null or empty";
        private const string ErrorCannotAddHeader = "Cannot add header '{0}'. This header may be a content header or restricted header. Error: {1}";
        private const string ErrorUrlNullOrEmpty = "URL cannot be null or empty";
        private const string ErrorRequestTimeout = "Request timeout";
        private const string ErrorNetworkError = "Network error: {0}";
        private const string ErrorUnexpected = "Unexpected error: {0}";
        private const string ErrorFailedToReadResponse = "Failed to read response content: {0}";
        private const string ErrorResponseNotValidJson = "Response is not valid JSON. First 100 chars: {0}";
        private const string ErrorFailedToDeserialize = "Failed to deserialize response: {0}. Content preview: {1}";
        private const string ErrorRequestFailedWithStatus = "Request failed with status {0}";
        private const string ErrorRequestFailedWithStatusAndContent = "Request failed with status {0}: {1}";

        // Content Types and Headers
        private const string ContentTypeApplicationJson = "application/json";
        private const string HeaderAccept = "Accept";
        private const string HeaderAcceptJson = "application/json, text/plain, */*";
        private const string HeaderUserAgent = "User-Agent";
        private const string HeaderAcceptLanguage = "Accept-Language";
        private const string HeaderAcceptEncoding = "Accept-Encoding";
        private const string HeaderAcceptEncodingValue = "gzip, deflate";
        private const string HeaderDnt = "DNT";
        private const string HeaderDntValue = "1";
        private const string HeaderConnection = "Connection";
        private const string HeaderConnectionValue = "keep-alive";
        private const string HeaderUpgradeInsecureRequests = "Upgrade-Insecure-Requests";
        private const string HeaderUpgradeInsecureRequestsValue = "1";
        private const string HeaderSecFetchDest = "Sec-Fetch-Dest";
        private const string HeaderSecFetchDestValue = "empty";
        private const string HeaderSecFetchMode = "Sec-Fetch-Mode";
        private const string HeaderSecFetchModeValue = "cors";
        private const string HeaderSecFetchSite = "Sec-Fetch-Site";
        private const string HeaderSecFetchSiteValue = "cross-site";
        private const string HeaderSecChUa = "sec-ch-ua";
        private const string HeaderSecChUaFormat = "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"{0}\", \"Google Chrome\";v=\"{0}\"";
        private const string HeaderSecChUaMobile = "sec-ch-ua-mobile";
        private const string HeaderSecChUaMobileValue = "?0";
        private const string HeaderSecChUaPlatform = "sec-ch-ua-platform";
        private const string HeaderSecChUaPlatformFormat = "\"{0}\"";

        // User Agent Templates
        private const string UserAgentTemplate = "Mozilla/5.0 ({0}) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{1} Safari/537.36";
        private const string WindowsNt10Win64 = "Windows NT 10.0; Win64; x64";
        private const string WindowsNt10Wow64 = "Windows NT 10.0; WOW64";

        // Chrome Versions
        private const string ChromeVersion120 = "120.0.0.0";
        private const string ChromeVersion121 = "121.0.0.0";
        private const string ChromeVersion122 = "122.0.0.0";
        private const string ChromeVersion123 = "123.0.0.0";
        private const string ChromeVersionShort120 = "120";
        private const string ChromeVersionShort121 = "121";
        private const string ChromeVersionShort122 = "122";
        private const string ChromeVersionShort123 = "123";

        // Accept Headers
        private const string AcceptHeader1 = "application/json, text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8";
        private const string AcceptHeader2 = "application/json, application/xml, text/plain, text/html, *.*";
        private const string AcceptHeader3 = "application/json, text/plain, */*";
        private const string AcceptHeader4 = "application/json, text/html, application/xhtml+xml, application/xml;q=0.9, */*;q=0.8";

        // Accept-Language Headers
        private const string AcceptLanguage1 = "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7";
        private const string AcceptLanguage2 = "pt-BR,pt;q=0.9,en;q=0.8";
        private const string AcceptLanguage3 = "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7,es;q=0.6";
        private const string AcceptLanguage4 = "pt-BR,pt;q=0.8,en-US;q=0.6,en;q=0.4";
        private const string AcceptLanguage5 = "pt-BR,en-US;q=0.9,en;q=0.8";

        // Platforms
        private const string PlatformWindows = "Windows";
        private const string PlatformMacOs = "macOS";
        private const string PlatformLinux = "Linux";

        // Authorization
        private const string AuthSchemeBearer = "Bearer";

        // JSON Delimiters
        private const string JsonObjectStart = "{";
        private const string JsonArrayStart = "[";

        // Misc
        private const string ColonSpace = ": ";

        #endregion

        #region Pre-allocated Arrays

        private static readonly string[] ChromeVersions = { ChromeVersion120, ChromeVersion121, ChromeVersion122, ChromeVersion123 };
        private static readonly string[] ChromeVersionsShort = { ChromeVersionShort120, ChromeVersionShort121, ChromeVersionShort122, ChromeVersionShort123 };
        private static readonly string[] WindowsVersions = { WindowsNt10Win64, WindowsNt10Wow64 };
        private static readonly string[] AcceptHeaders = { AcceptHeader1, AcceptHeader2, AcceptHeader3, AcceptHeader4 };
        private static readonly string[] AcceptLanguages = { AcceptLanguage1, AcceptLanguage2, AcceptLanguage3, AcceptLanguage4, AcceptLanguage5 };

        #endregion

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly bool _disposeHttpClient;
        private bool _disposed;
        private static readonly Random _random = new Random();

        // Parameterless constructor for flexible usage
        public HelperApiClient()
            : this(new HttpClient(), token: null, disposeHttpClient: true)
        {
        }

        // Constructor for manual instantiation with base URL
        public HelperApiClient(string baseUrl = null, string token = null, TimeSpan? timeout = null, bool emulateBrowser = false)
            : this(CreateHttpClient(baseUrl, timeout, emulateBrowser), token, disposeHttpClient: true)
        {
        }

        // Constructor for dependency injection (preferred)
        public HelperApiClient(HttpClient httpClient, string token = null, bool disposeHttpClient = false)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _disposeHttpClient = disposeHttpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = false,
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
            };

            if (!string.IsNullOrWhiteSpace(token))
            {
                SetBearerToken(token);
            }
        }

        private static HttpClient CreateHttpClient(string baseUrl, TimeSpan? timeout, bool emulateBrowser = false)
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.All,
                UseCookies = true,
                AllowAutoRedirect = true,
                MaxAutomaticRedirections = 5
            };

            var client = new HttpClient(handler, disposeHandler: true)
            {
                Timeout = timeout ?? TimeSpan.FromSeconds(100)
            };

            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                client.BaseAddress = new Uri(baseUrl);
            }

            if (emulateBrowser)
            {
                ConfigureBrowserHeaders(client);
            }
            else
            {
                client.DefaultRequestHeaders.Add(HeaderAccept, HeaderAcceptJson);
            }

            return client;
        }

        /// <summary>
        /// Configura headers para emular um navegador real com randomização moderada
        /// </summary>
        private static void ConfigureBrowserHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();

            // User-Agent randomizado (versões recentes do Chrome)
            var userAgent = GetRandomUserAgent();
            TryAddHeader(client, HeaderUserAgent, userAgent);

            // Accept: JSON prioritário mas com variação
            var accept = GetRandomAcceptHeader();
            TryAddHeader(client, HeaderAccept, accept);

            // Accept-Language: Baseado em locais comuns do Brasil
            var acceptLanguage = GetRandomAcceptLanguage();
            TryAddHeader(client, HeaderAcceptLanguage, acceptLanguage);

            // Accept-Encoding: Sempre o mesmo (descompressão automática)
            TryAddHeader(client, HeaderAcceptEncoding, HeaderAcceptEncodingValue);

            // DNT: Variação (nem todo mundo usa)
            if (_random.Next(100) < 70) // 70% chance
            {
                TryAddHeader(client, HeaderDnt, HeaderDntValue);
            }

            TryAddHeader(client, HeaderConnection, HeaderConnectionValue);

            // Upgrade-Insecure-Requests: Nem sempre presente
            if (_random.Next(100) < 85) // 85% chance
            {
                TryAddHeader(client, HeaderUpgradeInsecureRequests, HeaderUpgradeInsecureRequestsValue);
            }

            // Sec-Fetch headers
            TryAddHeader(client, HeaderSecFetchDest, HeaderSecFetchDestValue);
            TryAddHeader(client, HeaderSecFetchMode, HeaderSecFetchModeValue);
            TryAddHeader(client, HeaderSecFetchSite, HeaderSecFetchSiteValue);

            // Chrome hints com versão variada
            var chromeVersion = GetRandomChromeVersion();
            TryAddHeader(client, HeaderSecChUa, string.Format(HeaderSecChUaFormat, chromeVersion));
            TryAddHeader(client, HeaderSecChUaMobile, HeaderSecChUaMobileValue);

            // Platform: Windows é o mais comum, mas varia
            var platform = GetRandomPlatform();
            TryAddHeader(client, HeaderSecChUaPlatform, string.Format(HeaderSecChUaPlatformFormat, platform));
        }

        /// <summary>
        /// Tenta adicionar header, ignorando se já existir ou não for permitido
        /// </summary>
        private static void TryAddHeader(HttpClient client, string name, string value)
        {
            try
            {
                client.DefaultRequestHeaders.Remove(name);
                client.DefaultRequestHeaders.Add(name, value);
            }
            catch (Exception)
            {
                // Alguns headers não podem ser adicionados via DefaultRequestHeaders
                // (ex: Content-Type, Content-Length) - ignorar silenciosamente
            }
        }

        /// <summary>
        /// Retorna um User-Agent aleatório de versões recentes do Chrome
        /// </summary>
        private static string GetRandomUserAgent()
        {
            // Versões do Chrome dos últimos meses (120-123)
            var version = ChromeVersions[_random.Next(ChromeVersions.Length)];

            // Versões do Windows
            var winVersion = WindowsVersions[_random.Next(WindowsVersions.Length)];

            return string.Format(UserAgentTemplate, winVersion, version);
        }

        /// <summary>
        /// Retorna um Accept header aleatório priorizando JSON
        /// </summary>
        private static string GetRandomAcceptHeader()
        {
            return AcceptHeaders[_random.Next(AcceptHeaders.Length)];
        }

        /// <summary>
        /// Retorna um Accept-Language aleatório baseado em regiões do Brasil
        /// </summary>
        private static string GetRandomAcceptLanguage()
        {
            return AcceptLanguages[_random.Next(AcceptLanguages.Length)];
        }

        /// <summary>
        /// Retorna uma versão recente aleatória do Chrome
        /// </summary>
        private static string GetRandomChromeVersion()
        {
            return ChromeVersionsShort[_random.Next(ChromeVersionsShort.Length)];
        }

        /// <summary>
        /// Retorna uma plataforma aleatória (Windows é o mais comum)
        /// </summary>
        private static string GetRandomPlatform()
        {
            // 85% Windows, 10% macOS, 5% Linux
            var rand = _random.Next(100);

            if (rand < 85)
                return PlatformWindows;
            else if (rand < 95)
                return PlatformMacOs;
            else
                return PlatformLinux;
        }

        /// <summary>
        /// Ativa modo de emulação de navegador com headers randomizados
        /// </summary>
        public void EnableBrowserEmulation()
        {
            ConfigureBrowserHeaders(_httpClient);
        }

        public void SetBearerToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException(ErrorTokenNullOrEmpty, nameof(token));

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(AuthSchemeBearer, token);
        }

        public void AddDefaultHeader(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ErrorHeaderNameNullOrEmpty, nameof(name));

            try
            {
                _httpClient.DefaultRequestHeaders.Remove(name);
                _httpClient.DefaultRequestHeaders.Add(name, value);
            }
            catch (InvalidOperationException ex)
            {
                // Alguns headers não podem ser adicionados via DefaultRequestHeaders
                throw new InvalidOperationException(string.Format(ErrorCannotAddHeader, name, ex.Message), ex);
            }
        }

        public void RemoveDefaultHeader(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ErrorHeaderNameNullOrEmpty, nameof(name));

            _httpClient.DefaultRequestHeaders.Remove(name);
        }

        #region GET Methods

        public Task<ApiResponse<T>> GetAsync<T>(string url)
            => GetAsync<T>(url, CancellationToken.None);

        public async Task<ApiResponse<T>> GetAsync<T>(string url, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException(ErrorUrlNullOrEmpty, nameof(url));

            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
                return await ProcessResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                return CreateErrorResponse<T>(HttpStatusCode.RequestTimeout, ErrorRequestTimeout, ex);
            }
            catch (HttpRequestException ex)
            {
                return CreateErrorResponse<T>(HttpStatusCode.ServiceUnavailable, string.Format(ErrorNetworkError, ex.Message), ex);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<T>(HttpStatusCode.InternalServerError, string.Format(ErrorUnexpected, ex.Message), ex);
            }
            finally
            {
                response?.Dispose();
            }
        }

        #endregion

        #region POST Methods

        public Task<ApiResponse<T>> PostAsync<T>(string url, object? body = null)
            => PostAsync<T>(url, body, CancellationToken.None);

        public async Task<ApiResponse<T>> PostAsync<T>(string url, object? body, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException(ErrorUrlNullOrEmpty, nameof(url));

            HttpResponseMessage response = null;
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Post, url);

                if (body != null)
                {
                    var json = JsonSerializer.Serialize(body, _jsonOptions);
                    request.Content = new StringContent(json, Encoding.UTF8, ContentTypeApplicationJson);
                }

                response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
                return await ProcessResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                return CreateErrorResponse<T>(HttpStatusCode.RequestTimeout, ErrorRequestTimeout, ex);
            }
            catch (HttpRequestException ex)
            {
                return CreateErrorResponse<T>(HttpStatusCode.ServiceUnavailable, string.Format(ErrorNetworkError, ex.Message), ex);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<T>(HttpStatusCode.InternalServerError, string.Format(ErrorUnexpected, ex.Message), ex);
            }
            finally
            {
                response?.Dispose();
            }
        }

        #endregion

        #region PUT Methods

        public Task<ApiResponse<T>> PutAsync<T>(string url, object? body = null)
            => PutAsync<T>(url, body, CancellationToken.None);

        public async Task<ApiResponse<T>> PutAsync<T>(string url, object? body, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException(ErrorUrlNullOrEmpty, nameof(url));

            HttpResponseMessage response = null;
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Put, url);

                if (body != null)
                {
                    var json = JsonSerializer.Serialize(body, _jsonOptions);
                    request.Content = new StringContent(json, Encoding.UTF8, ContentTypeApplicationJson);
                }

                response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
                return await ProcessResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                return CreateErrorResponse<T>(HttpStatusCode.RequestTimeout, ErrorRequestTimeout, ex);
            }
            catch (HttpRequestException ex)
            {
                return CreateErrorResponse<T>(HttpStatusCode.ServiceUnavailable, string.Format(ErrorNetworkError, ex.Message), ex);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<T>(HttpStatusCode.InternalServerError, string.Format(ErrorUnexpected, ex.Message), ex);
            }
            finally
            {
                response?.Dispose();
            }
        }

        #endregion

        #region DELETE Methods

        public Task<ApiResponse<T>> DeleteAsync<T>(string url)
            => DeleteAsync<T>(url, CancellationToken.None);

        public async Task<ApiResponse<T>> DeleteAsync<T>(string url, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException(ErrorUrlNullOrEmpty, nameof(url));

            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.DeleteAsync(url, cancellationToken).ConfigureAwait(false);
                return await ProcessResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                return CreateErrorResponse<T>(HttpStatusCode.RequestTimeout, ErrorRequestTimeout, ex);
            }
            catch (HttpRequestException ex)
            {
                return CreateErrorResponse<T>(HttpStatusCode.ServiceUnavailable, string.Format(ErrorNetworkError, ex.Message), ex);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<T>(HttpStatusCode.InternalServerError, string.Format(ErrorUnexpected, ex.Message), ex);
            }
            finally
            {
                response?.Dispose();
            }
        }

        #endregion

        #region Batch Operations

        public Task<ApiResponse<T>[]> WaitForAllAsync<T>(params string[] urls)
            => WaitForAllAsync<T>(urls, CancellationToken.None);

        public async Task<ApiResponse<T>[]> WaitForAllAsync<T>(IEnumerable<string> urls, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            var urlList = urls?.ToList() ?? throw new ArgumentNullException(nameof(urls));

            if (!urlList.Any())
                return Array.Empty<ApiResponse<T>>();

            var tasks = urlList.Select(url => GetAsync<T>(url, cancellationToken));
            return await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        public async Task<ApiResponse<T>[]> WaitForAllAsync<T>(params Task<ApiResponse<T>>[] tasks)
        {
            ThrowIfDisposed();

            if (tasks == null || tasks.Length == 0)
                return Array.Empty<ApiResponse<T>>();

            return await Task.WhenAll(tasks).ConfigureAwait(false);
        }

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

        #endregion

        #region Helper Methods

        private async Task<ApiResponse<T>> ProcessResponseAsync<T>(
            HttpResponseMessage response,
            CancellationToken cancellationToken)
        {
            string content;

            try
            {
#if NETSTANDARD2_0 || NET472 || NET48
                content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#else
                content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#endif

                if (!string.IsNullOrEmpty(content) && content.Contains('\0'))
                {
                    var bytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    content = Encoding.UTF8.GetString(bytes);
                }
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<T>(
                    response.StatusCode,
                    string.Format(ErrorFailedToReadResponse, ex.Message),
                    ex);
            }

            var apiResponse = new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode,
                RawContent = content
            };

            if (response.IsSuccessStatusCode)
            {
                if (!string.IsNullOrWhiteSpace(content))
                {
                    try
                    {
                        var cleanContent = content.Trim();

                        if (!cleanContent.StartsWith(JsonObjectStart) && !cleanContent.StartsWith(JsonArrayStart))
                        {
                            apiResponse.IsSuccess = false;
                            apiResponse.ErrorMessage = string.Format(ErrorResponseNotValidJson,
                                cleanContent.Substring(0, Math.Min(100, cleanContent.Length)));
                            return apiResponse;
                        }

                        apiResponse.Data = JsonSerializer.Deserialize<T>(cleanContent, _jsonOptions);
                    }
                    catch (JsonException ex)
                    {
                        apiResponse.IsSuccess = false;
                        apiResponse.ErrorMessage = string.Format(ErrorFailedToDeserialize,
                            ex.Message,
                            content.Substring(0, Math.Min(200, content.Length)));
                        apiResponse.Exception = ex;
                    }
                }
            }
            else
            {
                apiResponse.ErrorMessage = string.Format(ErrorRequestFailedWithStatus, response.StatusCode);

                if (!string.IsNullOrWhiteSpace(content))
                {
                    apiResponse.ErrorMessage = string.Format(ErrorRequestFailedWithStatusAndContent, response.StatusCode, content);
                }
            }

            return apiResponse;
        }

        private ApiResponse<T> CreateErrorResponse<T>(HttpStatusCode statusCode, string errorMessage, Exception ex = null)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                IsSuccess = false,
                ErrorMessage = errorMessage,
                Exception = ex
            };
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(HelperApiClient));
        }

        #endregion

        #region Dispose Pattern

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

        #endregion
    }

    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string RawContent { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public Exception? Exception { get; set; }

        public bool HasData => IsSuccess && Data != null;
        public bool IsTooManyRequests => StatusCode == HttpStatusCode.TooManyRequests;
    }
}
