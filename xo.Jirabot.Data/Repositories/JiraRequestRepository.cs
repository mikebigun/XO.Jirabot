using System;
using System.Collections.Generic;
using System.Data;
using xo.Jirabot.Contracts;
using xo.Jirabot.Contracts.Entities.Jira;
using xo.Jirabot.Contracts.Repositories;

namespace xo.Jirabot.Data.Repositories
{
    public class JiraRequestRepository : BaseRepository<JiraRequest>, IJiraRequestRepository
    {
        public JiraRequestRepository(ITargetDatabase database): base(database)
        {

        }

        public IList<JiraRequest> GetAllQueries()
        {
            throw new NotImplementedException();
        }

        protected override JiraRequest MapEntity(IDataRecord record)
        {
            throw new NotImplementedException();
        }
    }
}
