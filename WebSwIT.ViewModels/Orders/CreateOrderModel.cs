using System;

namespace WebSwIT.ViewModels.Orders
{
    public class CreateOrderModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StartOfProject { get; set; }
        public DateTime EndOfProject { get; set; }
        public DateTime Estimate { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}
