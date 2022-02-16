using System;

namespace WebSwIT.Entities.Entities
{
    public class EmployeeInRoleEmployee : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Guid RoleEmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual RoleEmployee RoleEmployee { get; set; }
    }
}
