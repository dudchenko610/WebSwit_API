using System;

namespace WebSwIT.Entities.Entities
{
    public class Technology : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
