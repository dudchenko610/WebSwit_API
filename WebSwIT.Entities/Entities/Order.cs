using System;

namespace WebSwIT.Entities.Entities
{
    public class Order : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StartOfProject { get; set; }
        public DateTime EndOfProject { get; set; }
        public DateTime Estimate { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
