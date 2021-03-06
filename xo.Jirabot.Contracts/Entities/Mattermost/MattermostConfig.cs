﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.Contracts.Entities.Mattermost
{
    public class MattermostConfig : IEntity
    {
        public int Id { get; set; }

        public string Domain { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public int Frequency { get; set; }

        public IEnumerable<MattermostUser> Users { get; set; }
    }
}
