using AutoMapper;
using WebSwIT.Entities.Entities;
using WebSwIT.ViewModels.WorkSamplePictures;

namespace WebSwIT.BusinessLogicLayer.MapperProfiles
{
    class WorkSamplePictureProfile : Profile
    {
        public WorkSamplePictureProfile()
        {
            CreateMap<CreateWorkSamplePictureModel, WorkSamplePicture>()
                .ForMember(x => x.Name, model => model.MapFrom(i => i.File.FileName));
            CreateMap<WorkSamplePicture, CreateWorkSamplePictureModel>();

            CreateMap<WorkSamplePicture, WorkSamplePictureModel>();
        }
    }
}
