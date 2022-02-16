using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class RoleEmployeeRepository : BaseRepository<RoleEmployee>, IRoleEmployeeRepository
    {
        public RoleEmployeeRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<RoleEmployee> GetByNameAsync(string name)
        {
            var result = await _dbSet.AsNoTracking().Where(role => role.Name.Equals(name)).FirstOrDefaultAsync();
            return result;
        }
    }
}
