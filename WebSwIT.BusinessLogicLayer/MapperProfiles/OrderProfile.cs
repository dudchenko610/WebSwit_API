using AutoMapper;
using WebSwIT.Entities.Entities;
using WebSwIT.ViewModels.Orders;

namespace WebSwIT.BusinessLogicLayer.MapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderModel, Order>();
            CreateMap<Order, CreateOrderModel>();

            CreateMap<OrderModel, Order>();
            CreateMap<Order, OrderModel>();

            CreateMap<UpdateOrderModel, Order>();
            CreateMap<Order, UpdateOrderModel>();
        }
            
    }
}
