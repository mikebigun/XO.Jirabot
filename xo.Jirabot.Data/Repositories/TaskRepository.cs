using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using xo.Jirabot.Contracts;
using xo.Jirabot.Contracts.Entities.Tasks;
using xo.Jirabot.Contracts.Globals;
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
            base.Post("INSERT INTO Tasks (Type, Status, Reference, PlannedTime) VALUES (@Type, @Status, @Reference, @PlannedTime)",
                new Dictionary<string, object>
                {
                    { "@Type", (int)task.Type },
                    { "@Status", (int)task.Status },
                    { "@Reference", task.Reference },
                    { "@PlannedTime", task.PlannedTime.HasValue ? task.PlannedTime.Value.ToString(Constants.DateTimeFormat) : string.Empty }
                });
        }

        public bool IsPlanned(int reference)
        {
            var query = base.Get("SELECT Id WHERE Status = 'PLANNED' AND Reference = @Reference LIMIT 1",
                new Dictionary<string, object> { { "@Reference", reference } });

            return query != null && query.Any();
        }

        public Task GetLatestRunByReference(int reference)
        {
            return
                base.Get(@"SELECT Id, Status, Type, Reference, PlannedTime, ProcessedTime FROM Tasks 
                            WHERE (Status = @StatusCompleted OR Status = @StatusFailed) AND Reference = @Reference 
                            ORDER BY Id DESC LIMIT 1", 
                new Dictionary<string, object>
                {
                    { "@StatusCompleted", (int)TaskStatus.COMPLETED },
                    { "@StatusFailed", (int)TaskStatus.FAILED },
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
                Id = ValueOrDefault<int>(record, "Id"),
                Status = (TaskStatus)ValueOrDefault<int>(record, "Status"),
                Type = (TaskType)ValueOrDefault<int>(record, "Type"),
                Reference = ValueOrDefault<int>(record, "Reference"),
                PlannedTime = ValueOrDefault<DateTime?>(record, "PlannedTime"),
                ProcessedTime = ValueOrDefault<DateTime?>(record, "ProcessedTime")
            };
        }
    }
}
