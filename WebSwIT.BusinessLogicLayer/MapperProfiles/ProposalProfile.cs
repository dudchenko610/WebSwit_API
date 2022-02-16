using AutoMapper;
using WebSwIT.Entities.Entities;
using WebSwIT.ViewModels.Proposals;

namespace WebSwIT.BusinessLogicLayer.MapperProfiles
{
    public class ProposalProfile : Profile
    {
        public ProposalProfile()
        {
            CreateMap<CreateProposalModel, Proposal>();
            CreateMap<Proposal, ProposalModel>();
        }
    }
}
