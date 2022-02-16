using System;

namespace WebSwIT.DataAccessLayer.Models.Orders
{
    public class OrderFilterModel
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public DateTime? StartOfProject { get; set; }
        public DateTime? EndOfProject { get; set; }
        public Guid CategoryId { get; set; }
    }
}
