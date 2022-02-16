using System;

namespace WebSwIT.ViewModels.Orders
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StartOfProject { get; set; }
        public DateTime EndOfProject { get; set; }
        public DateTime Estimate { get; set; }
        public Guid CategoryId { get; set; }
    }
}
