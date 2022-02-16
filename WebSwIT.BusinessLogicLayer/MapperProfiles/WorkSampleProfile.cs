using AutoMapper;
using WebSwIT.Entities.Entities;
using WebSwIT.ViewModels.WorkSamples;

namespace WebSwIT.BusinessLogicLayer.MapperProfiles
{
    public class WorkSampleProfile : Profile
    {
        public WorkSampleProfile()
        {
            CreateMap<CreateWorkSampleModel, WorkSample>();
            CreateMap<WorkSample, CreateWorkSampleModel>();

            CreateMap<WorkSampleModel, WorkSample>();
            CreateMap<WorkSample, WorkSampleModel>()
                .ForMember(i => i.Pictures, model => model.MapFrom(c => c.WorkSamplePictures));

            CreateMap<UpdateWorkSampleModel, WorkSample>();
            CreateMap<WorkSample, UpdateWorkSampleModel>();
        }
    }
}
