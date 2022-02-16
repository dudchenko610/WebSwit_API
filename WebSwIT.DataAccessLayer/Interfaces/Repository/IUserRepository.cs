using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Users;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<IEnumerable<User>> GetFilterUserAsync(UserFilterModel filter);
        public Task<User> GetUserWithContactsByIdAsync(Guid id);
    }
}
