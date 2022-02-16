using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface IEmployeeAndRoleEmployeeService
    {
        public Task AddRoleEmployeeToEmployeeAsync(List<Guid> roleIds, Guid employeeId);
    }
}
