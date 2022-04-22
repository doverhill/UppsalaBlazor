using System.Text;
using System.Text.Json;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System;

namespace UppsalaBlazor.Client.Helpers
{
    public class APIResult<T>
    {
        public bool Success;
        public HttpStatusCode StatusCode;
        public T Result;

        public APIResult(bool success, HttpStatusCode statusCode)
        {
            Success = success;
            StatusCode = statusCode;
            Result = default(T);
        }

        public APIResult(T result)
        {
            Success = true;
            StatusCode = HttpStatusCode.OK;
            Result = result;
        }
    }

    public static class API
    {
        private static JsonSerializerOptions JsonSettings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static async Task<APIResult<TResult>> MakeStreamRequest<TResult>(HttpClient httpClient, string url, Stream stream, HttpMethod method)
        {
            var request = BuildRequest(method, url, new StreamContent(stream));
            return await SendAndParseResult<TResult>(httpClient, request);
        }

        public static async Task<APIResult<TResult>> MakeRequest<TResult>(HttpClient httpClient, string url, object payload, HttpMethod method)
        {
            var request = BuildRequest(method, url, new StringContent(JsonSerializer.Serialize(payload, JsonSettings), Encoding.UTF8, "application/json"));
            return await SendAndParseResult<TResult>(httpClient, request);
        }

        public static async Task<APIResult<TResult>> MakeRequest<TResult>(HttpClient httpClient, string url, HttpMethod method)
        {
            var request = BuildRequest(method, url, null);
            return await SendAndParseResult<TResult>(httpClient, request);
        }

        private static async Task<APIResult<TResult>> SendAndParseResult<TResult>(HttpClient client, HttpRequestMessage request)
        {
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Read content: " + content);
                    if (string.IsNullOrEmpty(content)) return new APIResult<TResult>(true, response.StatusCode);
                    if (typeof(TResult) == typeof(string)) return new APIResult<TResult>((TResult)(object)content);

                    Console.WriteLine("Deserializing");
                    var result = JsonSerializer.Deserialize<TResult>(content, JsonSettings);
                    return new APIResult<TResult>(result);
                }
                catch (Exception)
                {
                    return new APIResult<TResult>(false, response.StatusCode);
                    throw;
                }
            }

            return new APIResult<TResult>(false, response.StatusCode);
        }

        private static HttpRequestMessage BuildRequest(HttpMethod method, string url, HttpContent content)
        {
            return new HttpRequestMessage(method, "http://localhost:7237/" + url)
            {
                Content = content
            };
        }
    }
}
