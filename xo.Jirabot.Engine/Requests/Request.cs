using System.Net;
using xo.Jirabot.Contracts.Requests;

namespace xo.Jirabot.Engine.Requests
{
    public class Request : IRequest
    {
        public HttpWebRequest CreateRequest(string baseUrl)
        {
            return null;
            //https://topshelf.readthedocs.io/en/latest/overview/faq.html#is-topshelf-just-for-windows

        }
    }
}
