using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xo.Jirabot.Contracts.Entities.Jira;

namespace xo.Jirabot.Contracts.Repositories
{
    public interface IJiraRequestRepository : IRepository
    {
        IList<JiraRequest> GetAllQueries();
    }

}
