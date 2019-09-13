using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.Engine.Models
{
    public class JiraConfiguration
    {
        public string BaseAddress { get; set; }

        public string ApiPath { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Query { get; set; }
    }
}
