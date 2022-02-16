using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.Entities.Entities;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class EmployeeAndRoleEmployeeService : IEmployeeAndRoleEmployeeService
    {
        private readonly IEmployeeAndRoleEmployeeRepository _employeeAndRoleEmployeeRepository;

        public EmployeeAndRoleEmployeeService(IEmployeeAndRoleEmployeeRepository employeeAndRoleEmployeeRepository)
        {
            _employeeAndRoleEmployeeRepository = employeeAndRoleEmployeeRepository;
        }

        public async Task AddRoleEmployeeToEmployeeAsync(List<Guid> roleIds, Guid employeeId)
        {
            var employeeInRole = new List<EmployeeInRoleEmployee>();

            roleIds.ForEach(id => employeeInRole.Add(new EmployeeInRoleEmployee
            {
                EmployeeId = employeeId,
                RoleEmployeeId = id
            }));

            await _employeeAndRoleEmployeeRepository.CreateRangeAsync(employeeInRole);
        }
    }
}
