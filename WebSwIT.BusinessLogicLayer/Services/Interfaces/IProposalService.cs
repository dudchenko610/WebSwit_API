using System;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Proposals;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Proposals;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface IProposalService
    {
        public Task<ProposalModel> CreateAsync(CreateProposalModel model);
        public Task DeleteAsync(Guid id);
        public Task<ProposalModel> GetByIdAsync(Guid id);
        public Task<PagedResponseModel<ProposalModel>> GetFilteredAsync(ProposalFilterModel model, PaginationFilterModel pagination);
    }
}
