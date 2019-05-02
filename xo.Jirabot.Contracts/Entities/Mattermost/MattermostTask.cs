using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.Contracts.Entities.Mattermost
{
    public class MattermostTask : IEntity
    {
        public int Id { get; set; }

        public int Status { get; set; }
    }
}
