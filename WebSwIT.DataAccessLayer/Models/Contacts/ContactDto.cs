using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Models.Contacts
{
    public class ContactDto
    {
        public User User { get; set; }
        public int UnreadCount { get; set; }
        public long MillisecondsSince1970 { get; set; }
    }
}
