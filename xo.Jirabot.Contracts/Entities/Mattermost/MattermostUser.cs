using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.Contracts.Entities.Mattermost
{
    public class MattermostUser : IEntity
    {
        public int Id { get; set; }

        public string Username { get; set; }
    }
}
