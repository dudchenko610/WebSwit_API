using System;
using System.Collections.Generic;
using WebSwIT.ViewModels.EmployeeInRoleEmployees;

namespace WebSwIT.ViewModels.RoleEmployees
{
    public class RoleEmployeeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public List<EmployeeInRoleEmployeeModel> EmployeeInRoleEmployees { get; set; }
    }
}
