using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class TaskRepository : RepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public void CreateTask(Task task)
        {
            Create(task);
        }

        public void DeleteTask(Task task)
        {
            Delete(task);
        }

        public void UpdateTask(Task task)
        {
            Update(task);
        }

        public IEnumerable<Task> TasksByUser(Guid userId)
        {
            return FindByCondition(t => t.UserId.Equals(userId)).ToList();
        }

        public Task GetTaskById(int taskId)
        {
            return FindByCondition(task => task.Id.Equals(taskId))
                .FirstOrDefault();
        }


    }
}
