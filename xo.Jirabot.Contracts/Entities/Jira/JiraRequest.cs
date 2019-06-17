using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.Contracts.Entities.Jira
{
    public class JiraRequest : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Query { get; set; }

        public string Conditions { get; set; }

        public string Frequency { get; set; }
    }
}
