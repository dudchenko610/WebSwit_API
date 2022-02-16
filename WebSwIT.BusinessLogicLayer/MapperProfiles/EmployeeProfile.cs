using AutoMapper;
using WebSwIT.Entities.Entities;
using WebSwIT.ViewModels.Employees;

namespace WebSwIT.BusinessLogicLayer.MapperProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeModel, Employee>();
            CreateMap<Employee, CreateEmployeeModel>();

            CreateMap<EmployeeModel, Employee>();
            CreateMap<Employee, EmployeeModel>();

            CreateMap<UpdateEmployeeModel, Employee>();
            CreateMap<Employee, UpdateEmployeeModel>();
        }
    }
}
