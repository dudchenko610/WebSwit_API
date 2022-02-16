using AutoMapper;
using WebSwIT.Entities.Entities;
using WebSwIT.ViewModels.EmployeeInRoleEmployees;

namespace WebSwIT.BusinessLogicLayer.MapperProfiles
{
    public class EmployeeInRoleEmployeeProfile : Profile
    {
        public EmployeeInRoleEmployeeProfile()
        {
            CreateMap<EmployeeInRoleEmployee, EmployeeInRoleEmployeeModel>();
            CreateMap<EmployeeInRoleEmployeeModel, EmployeeInRoleEmployee>();
        }
    }
}
