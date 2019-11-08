using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll()
                .OrderBy(u => u.Firstname)
                .ToList();
        }

        public User GetUserById(Guid userId)
        {
            return FindByCondition(user => user.Id.Equals(userId))
                .FirstOrDefault();
        }



        public User GetUserWithDetails(Guid userId)
        {
            return FindByCondition(user => user.Id.Equals(userId))
                .Include(ts => ts.Tasks)
                .FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }
    }
}
