using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface ITaskRepository : IRepositoryBase<Task>
    {
        IEnumerable<Task> TasksByUser(Guid userId);
    }
}
