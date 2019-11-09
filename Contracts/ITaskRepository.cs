using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface ITaskRepository : IRepositoryBase<Task>
    {
        IEnumerable<Task> TasksByUser(Guid userId);

        void CreateTask(Task task);

        void UpdateTask(Task task);

        void DeleteTask(Task task);

        Task GetTaskById(int taskId);
    }
}
