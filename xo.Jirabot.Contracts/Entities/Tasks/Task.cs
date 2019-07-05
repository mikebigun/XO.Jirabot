using System;

namespace xo.Jirabot.Contracts.Entities.Tasks
{
    public class Task : IEntity
    {
        public int Id { get; set; }

        public TaskType Type { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime? PlannedTime { get; set; }

        public DateTime? ProcessedTime { get; set; }

        public int Reference { get; set; }
    }
}
