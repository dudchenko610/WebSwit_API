using System.ComponentModel.DataAnnotations;

namespace WebSwIT.ViewModels.RoleEmployees
{
    public class CreateRoleEmployeeModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
