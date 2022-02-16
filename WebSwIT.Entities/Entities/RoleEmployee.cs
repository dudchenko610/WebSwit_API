using System.Collections.Generic;

namespace WebSwIT.Entities.Entities
{
    public class RoleEmployee : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public List<EmployeeInRoleEmployee> EmployeeInRoleEmployee { get; set; }

        public RoleEmployee()
        {
            EmployeeInRoleEmployee = new List<EmployeeInRoleEmployee>();
        }
    }
}
