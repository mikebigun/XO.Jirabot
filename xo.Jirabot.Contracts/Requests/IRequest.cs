using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.Contracts.Requests
{
    public interface IRequest
    {
        HttpWebRequest CreateRequest(string baseUrl);
    }
}
