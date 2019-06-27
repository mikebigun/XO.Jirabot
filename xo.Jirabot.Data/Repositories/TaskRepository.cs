using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using xo.Jirabot.Contracts;
using xo.Jirabot.Contracts.Entities.Tasks;
using xo.Jirabot.Contracts.Repositories;

namespace xo.Jirabot.Data.Repositories
{
    public class TaskRepository : BaseRepository<Task>, ITaskReporsitory
    {
        public TaskRepository(ITargetDatabase database) : base(database)
        {
        }

        public void CreateTask(Task task)
        {
            base.Post("INSERT INTO Tasks (Type, Status, Request) VALUES (@Type, @Status, @Request)",
                new Dictionary<string, object>
                {
                    { "@Type", (int)task.Type },
                    { "@Status", (int)task.Status },
                    { "@Request", task.Request }
                });
        }

        public Task GetLatestTaskByRequest(int request)
        {
            return
                base.Get("SELECT Id, Status, Type, Request FROM Tasks WHERE Status = @Status AND Request = @Request ORDER BY Id", 
                new Dictionary<string, object>
                {
                    { "@Status", (int)TaskStatus.COMPLETED },
                    { "@Request", request }
                }).FirstOrDefault();
        }

        public void UpdateStatus(int id, TaskStatus status)
        {
            base.Post("UPDATE Tasks SET Status = @Status WHERE Id = @Id", new Dictionary<string, object>
            {
                { "@Id", id },
                { "@Status", (int)status }
            });
        }

        protected override Task MapEntity(IDataRecord record)
        {
            return new Task
            {
                Id = Convert.ToInt32(record["Id"]),
                Status = (TaskStatus)Convert.ToInt32(record["Status"]),
                Type = (TaskType)Convert.ToInt32(record["Type"]),
                Request = Convert.ToInt32(record["Request"])
            };
        }
    }
}
