using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using WebSwIT.Entities.Interfaces;

namespace WebSwIT.Entities.Entities
{
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RefreshToken { get; set; }
        public string PictureName { get; set; }
        public List<Contact> Contacts1 { get; set; }
        public List<Contact> Contacts2 { get; set; }
    }
}
