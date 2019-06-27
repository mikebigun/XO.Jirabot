using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xo.Jirabot.Contracts.Entities.Tasks
{
    public class Task : IEntity
    {
        public int Id { get; set; }

        public TaskType Type { get; set; }

        public TaskStatus Status { get; set; }

        public string RunTicks { get; set; }

        public int Reference { get; set; }
    }
}
