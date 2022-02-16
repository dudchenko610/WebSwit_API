using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Orders;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Order> GetByNameAsync(string name)
        {
            return await _dbSet.AsNoTracking().Where(tech => tech.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetFilteredOrdersAsync(OrderFilterModel filter)
        {
            IQueryable<Order> orders = _dbSet
                                        .Include(cat => cat.Category)
                                        .Where(ord => filter.Name == null || ord.Name.Contains(filter.Name))
                                        .Where(ord => filter.Price == null || ord.Price == filter.Price)
                                        .Where(ord => filter.StartOfProject == null || ord.StartOfProject == filter.StartOfProject)
                                        .Where(ord => filter.EndOfProject == null || ord.EndOfProject == filter.EndOfProject)
                                        .Where(ord => filter.CategoryId == Guid.Empty || ord.CategoryId == filter.CategoryId);

            return await orders.ToListAsync();
        }
    }
}
