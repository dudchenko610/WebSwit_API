using System;

namespace WebSwIT.Entities.Entities
{
    public class Contact: BaseEntity
    {
        public Guid UserId1 { get; set; }
        public User User1 { get; set; }
        public Guid UserId2 { get; set; }
        public User User2 { get; set; }
    }
}