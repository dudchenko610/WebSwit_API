
namespace WebSwIT.DataAccessLayer.Models.Users
{
    public class UserFilterModel
    {
        public bool? EmailConfirmed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
