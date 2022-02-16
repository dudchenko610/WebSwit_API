using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Orders;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Interfaces.Repository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public Task<Order> GetByNameAsync(string name);
        public Task<IEnumerable<Order>> GetFilteredOrdersAsync(OrderFilterModel filter);
    }
}
