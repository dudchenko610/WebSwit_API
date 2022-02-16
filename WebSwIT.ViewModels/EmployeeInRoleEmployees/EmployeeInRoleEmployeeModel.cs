using System;
using WebSwIT.ViewModels.RoleEmployees;

namespace WebSwIT.ViewModels.EmployeeInRoleEmployees
{
    public class EmployeeInRoleEmployeeModel
    {
        public Guid EmployeeId { get; set; }
        public Guid RoleEmployeeId { get; set; }
        public RoleEmployeeModel RoleEmployee { get; set; }
    }
}
