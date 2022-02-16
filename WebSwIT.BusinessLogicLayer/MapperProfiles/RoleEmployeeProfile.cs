using AutoMapper;
using WebSwIT.Entities.Entities;
using WebSwIT.ViewModels.RoleEmployees;

namespace WebSwIT.BusinessLogicLayer.MapperProfiles
{
    public class RoleEmployeeProfile : Profile
    {
        public RoleEmployeeProfile()
        {
            CreateMap<CreateRoleEmployeeModel, RoleEmployee>();
            CreateMap<RoleEmployee, CreateRoleEmployeeModel>();

            CreateMap<RoleEmployeeModel, RoleEmployee>();
            CreateMap<RoleEmployee, RoleEmployeeModel>();

            CreateMap<UpdateRoleEmployeeModel, RoleEmployee>();
            CreateMap<RoleEmployee, UpdateRoleEmployeeModel>();
        }
    }
}
