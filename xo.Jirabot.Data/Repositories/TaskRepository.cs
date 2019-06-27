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
            base.Post("INSERT INTO Tasks (Type, Status, Reference) VALUES (@Type, @Status, @Reference)",
                new Dictionary<string, object>
                {
                    { "@Type", (int)task.Type },
                    { "@Status", (int)task.Status },
                    { "@Reference", task.Reference }
                });
        }

        public Task GetLatestTaskByReference(int reference)
        {
            return
                base.Get("SELECT Id, Status, Type, Reference, RunTicks FROM Tasks WHERE Status = @Status AND Reference = @Reference ORDER BY Id", 
                new Dictionary<string, object>
                {
                    { "@Status", (int)TaskStatus.COMPLETED },
                    { "@Reference", reference }
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
                Reference = Convert.ToInt32(record["Reference"]),
                RunTicks = record["RunTicks"].ToString()
            };
        }
    }
}
