using System.ComponentModel.DataAnnotations;

namespace WebSwIT.ViewModels.Accounts
{
    public class ForgotPasswordModel
    {
        [Required]
        public string Email { get; set; }
    }
}
