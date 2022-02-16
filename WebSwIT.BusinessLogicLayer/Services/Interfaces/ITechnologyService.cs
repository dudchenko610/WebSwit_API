using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Technologies;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Technologies;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface ITechnologyService
    {
        public Task<TechnologyModel> CreateAsync(CreateTechnologyModel model);
        public Task DeleteAsync(Guid id);
        public Task<TechnologyModel> GetByIdAsync(Guid id);
        public Task<PagedResponseModel<TechnologyModel>> GetFilteredAsync(TechnologyFilterModel model, PaginationFilterModel pagination);
        public Task<TechnologyModel> UpdateAsync(UpdateTechnologyModel model);
        public Task<IEnumerable<TechnologyModel>> GetAllAsync();
    }
}
