using WebSwIT.ViewModels.Users;

namespace WebSwIT.ViewModels.Contacts
{
    public class ContactModel
    {
        public UserModel User { get; set; }
        public int UnreadCount { get; set; }
        public long MillisecondsSince1970 { get; set; }
    }
}
