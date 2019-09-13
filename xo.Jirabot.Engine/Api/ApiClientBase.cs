using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.Engine.Api
{
    public abstract class ApiClientBase
    {
        protected readonly HttpClient _httpClient;

        protected readonly string _token;

        public ApiClientBase(Uri remoteAddress, string username, string password)
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseDefaultCredentials = false,
                Proxy = WebRequest.DefaultWebProxy,
                Credentials = WebRequest.DefaultWebProxy.Credentials,
                UseProxy = true,
            };

            this._httpClient = new HttpClient(handler)
            {
                BaseAddress = remoteAddress,
            };

            this._token = EncodeCredentials(username, password);
        }

        private string EncodeCredentials(string username, string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        }

        protected HttpRequestMessage FormatRequest(HttpMethod method, string uri)
        {
            var request = new HttpRequestMessage(method, uri);

            if (!string.IsNullOrWhiteSpace(_token))
            {
                request.Headers.Add("Authorization", $"Basic { _token }");
            }
            
            return request;
        }

        protected async Task<ApiResponse> SendToApiAsync(HttpRequestMessage message)
        {
            try
            {
                var response = await _httpClient.SendAsync(message);
                return ProcessResponse(response);
            }
            catch (AggregateException)
            {
                return new ApiResponse(HttpStatusCode.ServiceUnavailable);
            }
            catch (HttpRequestException)
            {
                return new ApiResponse(HttpStatusCode.ServiceUnavailable);
            }
        }

        protected async Task<ApiResponse<T>> SendToApiAsync<T>(HttpRequestMessage message)
        {
            try
            {
                var response = await _httpClient.SendAsync(message);
                var tt = await response.Content.ReadAsStringAsync();
                return ProcessResponse<T>(response);
            }
            catch (AggregateException)
            {
                return new ApiResponse<T>(HttpStatusCode.ServiceUnavailable);
            }
            catch (HttpRequestException)
            {
                return new ApiResponse<T>(HttpStatusCode.ServiceUnavailable);
            }
        }

        protected static ApiResponse ProcessResponse(HttpResponseMessage result)
        {
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return new ApiResponse();
            }
            else
            {
                return new ApiResponse(result.StatusCode);
            }
        }

        private static ApiResponse<T> ProcessResponse<T>(HttpResponseMessage result)
        {
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return new ApiResponse<T>(result.Content.ReadAsAsync<T>().Result);
            }
            else
            {
                return new ApiResponse<T>(result.StatusCode);
            }
        }
    }
}
