using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xo.Jirabot.Engine;

namespace xo.Jirabot.WinService
{
    class Program
    {
        static void Main(string[] args)
        {
            var e = new Engine.Engine();
            e.GetSheduledTask();


            // on tick
            // send jira requests one by one with some intervals
            // on response, notify observers 
            // observer is a mattermost user
            // on update, use mattermost API to send request
        }
    }
}
