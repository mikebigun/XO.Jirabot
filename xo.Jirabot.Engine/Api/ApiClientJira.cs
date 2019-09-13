using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace xo.Jirabot.Engine.Api
{
    public class ApiClientJira : ApiClientBase
    {
        public ApiClientJira(string remoteAddress, string username, string password) :
            base (new Uri(remoteAddress), username, password)
        {
        }
       
        public Task<ApiResponse<object>> Search(string api, string query)
        {
            var request = FormatRequest(HttpMethod.Get, $"{ api }?{ query }");
            return SendToApiAsync<object>(request);
        }
    }
}
