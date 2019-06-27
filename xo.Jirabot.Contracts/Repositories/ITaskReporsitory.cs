﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xo.Jirabot.Contracts.Entities.Tasks;

namespace xo.Jirabot.Contracts.Repositories
{
    public interface ITaskReporsitory : IRepository
    {
        Task GetLatestTaskByRequest(int request);

        void CreateTask(Task task);

        void UpdateStatus(int id, TaskStatus status);
    }
}
