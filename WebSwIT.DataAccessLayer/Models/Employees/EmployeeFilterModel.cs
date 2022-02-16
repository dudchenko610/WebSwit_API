using System;

namespace WebSwIT.DataAccessLayer.Models.Employees
{
    public class EmployeeFilterModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Guid RoleId { get; set; }
    }
}
