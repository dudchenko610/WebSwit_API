using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Employees;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        public Task<Employee> GetEmployeeByEmailAsync(string email);
        public Task<IEnumerable<Employee>> GetFilteredEmployeeAsync(EmployeeFilterModel model);
    }
}
