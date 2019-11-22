using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid userId);
        User GetUserByUsername(string userId);

        User GetUserWithDetails(Guid userId);

        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);
    }
}
