using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebSwIT.ViewModels.Employees
{
    public class UpdateEmployeeModel
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public List<Guid> RoleIds { get; set; }
    }
}
