using System.Text.Json;
using System.Text;

namespace VisionOfChosen_BE.Utils
{
    public interface IHttpHelper
    {
        Task<string> GetAsync(string url);
        Task<string> PostJsonRawAsync<TRequest>(string url, TRequest data);
        Task<T> GetJsonAsync<T>(string url);
        Task<TResponse> PostJsonAsync<TRequest, TResponse>(string url, TRequest data);
    }

    public class HttpHelper : IHttpHelper
    {
        private readonly HttpClient _httpClient;

        public HttpHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync(string url)
        {
            using var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<T> GetJsonAsync<T>(string url)
        {
            var responseJson = await GetAsync(url);
            return JsonSerializer.Deserialize<T>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        }

        public async Task<string> PostJsonRawAsync<TRequest>(string url, TRequest data)
        {
            var json = JsonSerializer.Serialize(data);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<TResponse> PostJsonAsync<TRequest, TResponse>(string url, TRequest data)
        {
            var raw = await PostJsonRawAsync(url, data);
            return JsonSerializer.Deserialize<TResponse>(raw, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        }
    }
}
