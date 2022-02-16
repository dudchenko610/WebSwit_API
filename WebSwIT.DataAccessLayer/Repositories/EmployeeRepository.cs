using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Employees;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            return await _dbSet.AsNoTracking().Where(emp => emp.Email == email).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetFilteredEmployeeAsync(EmployeeFilterModel model)
        {
            IQueryable<Employee> employees = _dbSet
                                               .Include(employeeInRole => employeeInRole.EmployeeInRoleEmployees)
                                                    .ThenInclude(role => role.RoleEmployee)
                                               .Where(employee => model.FirstName == null || employee.FirstName.Contains(model.FirstName))
                                               .Where(employee => model.LastName == null || employee.LastName.Contains(model.LastName))
                                               .Where(employee => model.Email == null || employee.Email.Contains(model.Email))
                                               .Where(employee => model.RoleId == Guid.Empty || employee.EmployeeInRoleEmployees
                                                                                                .Any(role => role.RoleEmployee.Id == model.RoleId));

            var result = await employees.ToListAsync();

            return result;
        }
    }
}
