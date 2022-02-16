using AutoMapper;
using WebSwIT.Entities.Entities;
using WebSwIT.ViewModels.Users;

namespace WebSwIT.BusinessLogicLayer.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserModel, User>();
            CreateMap<User, CreateUserModel>();

            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();

            CreateMap<UpdateUserModel, User>();
            CreateMap<User, UpdateUserModel>();
        }
    }
}
