using System;
using System.Net;
using System.Threading.Tasks;
using xo.Jirabot.Engine.Api;
using xo.Jirabot.Engine.Models;

namespace xo.Jirabot.Engine.Requests
{
    public class JiraRequestManager
    {
        public async Task Search(JiraConfiguration configuration, Action<string> onSuccess, Action<string> onFail)
        {
            try
            {
                var api = new ApiClientJira(
                    configuration.BaseAddress, 
                    configuration.Username, 
                    configuration.Password);

                var response = await api.Search(configuration.ApiPath, configuration.Query);

                if (response.Result == HttpStatusCode.OK)
                {
                    onSuccess?.Invoke(response.Item1.ToString());
                }
                else
                {
                    onFail?.Invoke($"Failed to return JIRA data, HttpStatusCode: { response.Result }");
                }
            }
            catch (Exception ex)
            {
                onFail?.Invoke(ex.Message);
            }
        }
    }
}
