using AutoMapper;
using WebSwIT.Entities.Entities;
using WebSwIT.ViewModels.Technologies;

namespace WebSwIT.BusinessLogicLayer.MapperProfiles
{
    public class TechnologyProfile : Profile
    {
        public TechnologyProfile()
        {
            CreateMap<CreateTechnologyModel, Technology>();
            CreateMap<Technology, CreateTechnologyModel>();

            CreateMap<TechnologyModel, Technology>();
            CreateMap<Technology, TechnologyModel>();

            CreateMap<UpdateTechnologyModel, Technology>();
            CreateMap<Technology, UpdateTechnologyModel>();
        }
    }
}
