using System;
using System.Threading.Tasks;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Interfaces.Repository
{
    public interface IRoleEmployeeRepository : IBaseRepository<RoleEmployee>
    {
        public Task<RoleEmployee> GetByNameAsync(string name);
    }
}
