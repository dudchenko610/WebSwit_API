using System;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Orders;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Orders;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderModel> CreateAsync(CreateOrderModel model);
        public Task DeleteAsync(Guid id);
        public Task<OrderModel> GetByIdAsync(Guid id);
        public Task<PagedResponseModel<OrderModel>> GetFilteredAsync(OrderFilterModel model, PaginationFilterModel pagination);
        public Task<OrderModel> UpdateAsync(UpdateOrderModel model);
    }
}
