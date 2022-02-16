using System;
using System.ComponentModel.DataAnnotations;
using WebSwIT.Entities.Interfaces;

namespace WebSwIT.Entities.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
