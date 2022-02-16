using System.Collections.Generic;

namespace WebSwIT.Entities.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }
        
        public List<EmployeeInRoleEmployee> EmployeeInRoleEmployees { get; set; }

        public Employee()
        {
            EmployeeInRoleEmployees = new List<EmployeeInRoleEmployee>();
        }
    }
}
