using System.Text.Json;
using System.Text;

namespace VisionOfChosen_BE.Utils
{
    public interface IHttpHelper
    {
        Task<string> GetAsync(string url, Dictionary<string, string>? headers = null);
        Task<string> PostJsonRawAsync<TRequest>(string url, TRequest data, Dictionary<string, string>? headers = null);
        Task<T> GetJsonAsync<T>(string url, Dictionary<string, string>? headers = null);
        Task<TResponse> PostJsonAsync<TRequest, TResponse>(string url, TRequest data, Dictionary<string, string>? headers = null);
    }

    public class HttpHelper : IHttpHelper
    {
        private readonly HttpClient _httpClient;

        public HttpHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void AddHeaders(HttpRequestMessage request, Dictionary<string, string>? headers)
        {
            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    request.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value);
                }
            }
        }

        public async Task<string> GetAsync(string url, Dictionary<string, string>? headers = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            AddHeaders(request, headers);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<T> GetJsonAsync<T>(string url, Dictionary<string, string>? headers = null)
        {
            var responseJson = await GetAsync(url, headers);
            return JsonSerializer.Deserialize<T>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        }

        public async Task<string> PostJsonRawAsync<TRequest>(string url, TRequest data, Dictionary<string, string>? headers = null)
        {
            var json = JsonSerializer.Serialize(data);
            using var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            AddHeaders(request, headers);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<TResponse> PostJsonAsync<TRequest, TResponse>(string url, TRequest data, Dictionary<string, string>? headers = null)
        {
            var raw = await PostJsonRawAsync(url, data, headers);
            return JsonSerializer.Deserialize<TResponse>(raw, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        }
    }
}
