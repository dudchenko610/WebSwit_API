using System;
using System.Collections.Generic;
using WebSwIT.ViewModels.EmployeeInRoleEmployees;

namespace WebSwIT.ViewModels.Employees
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        public Guid MainPictureId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public List<EmployeeInRoleEmployeeModel> EmployeeInRoleEmployees { get; set; }
    }
}
