using System;
using System.ComponentModel.DataAnnotations;

namespace WebSwIT.ViewModels.RoleEmployees
{
    public class UpdateRoleEmployeeModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
