using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Users;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetFilterUserAsync(UserFilterModel filter)
        {
            IQueryable<User> users = _dbSet
                                     .Where(user => filter.FirstName == null || user.FirstName.Contains(filter.FirstName))
                                     .Where(user => filter.LastName == null || user.FirstName.Contains(filter.LastName))
                                     .Where(user => filter.EmailConfirmed == null || user.EmailConfirmed == filter.EmailConfirmed)
                                     .Where(user => filter.Email == null || user.Email.Contains(filter.Email));

            return await users.ToListAsync();
        }

        public async Task<User> GetUserWithContactsByIdAsync(Guid id)
        {
            User user = await _dbSet
                .Include(u => u.Contacts1)
                    .ThenInclude(ch => ch.User2)
                .Include(u => u.Contacts2)
                    .ThenInclude(ch => ch.User1)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }
    }
}
