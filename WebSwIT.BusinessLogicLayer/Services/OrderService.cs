using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Orders;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Exceptions;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Orders;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _imapper;
        private readonly IProfileService _profileService;

        public OrderService(
            IOrderRepository orderRepository,
            IMapper imapper,
            IProfileService profileService
            )
        {
            _orderRepository = orderRepository;
            _imapper = imapper;
            _profileService = profileService;
        }

        public async Task<OrderModel> CreateAsync(CreateOrderModel model)
        {
            //var existsOrder = await _orderRepository.GetByNameAsync(model.Name);

            //if (existsOrder is not null)
            //{
            //    throw new ServerException("", HttpStatusCode.Conflict);
            //}

            var user = await _profileService.GetMyUserAsync();

            if (user is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            model.UserId = user.Id;

            var newOrder = _imapper.Map<Order>(model);

            await _orderRepository.CreateAsync(newOrder);

            return _imapper.Map<OrderModel>(newOrder);
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            await _orderRepository.DeleteAsync(order);
        }

        public async Task<OrderModel> GetByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            var orderModel = _imapper.Map<OrderModel>(order);

            return orderModel;
        }

        public async Task<PagedResponseModel<OrderModel>> GetFilteredAsync(OrderFilterModel model, PaginationFilterModel pagination)
        {
            var orders = await _orderRepository.GetFilteredOrdersAsync(model);

            var ordersCount = orders.Count();

            orders = orders.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);

            var result = _imapper.Map<IEnumerable<OrderModel>>(orders);

            var pagedResponse = new PagedResponseModel<OrderModel>
            {
                Data = result,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalItems = ordersCount
            };

            return pagedResponse;
        }

        public async Task<OrderModel> UpdateAsync(UpdateOrderModel model)
        {
            var order = await _orderRepository.GetByIdAsync(model.Id);

            if (order is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            var user = await _profileService.GetMyUserAsync();

            if (user is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            model.UserId = user.Id;

            order = _imapper.Map<Order>(model);

            await _orderRepository.UpdateAsync(order);

            var roleModel = _imapper.Map<OrderModel>(order);

            return roleModel;

        }
    }
}
