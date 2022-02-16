using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.WorkSamples;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.WorkSamples;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface IWorkSampleService
    {
        public Task<WorkSampleModel> CreateAsync(CreateWorkSampleModel model);
        public Task DeleteAsync(Guid id);
        public Task<WorkSampleModel> GetByIdAsync(Guid id);
        public Task<PagedResponseModel<WorkSampleModel>> GetFilteredAsync(WorkSampleFilterModel model, PaginationFilterModel pagination);
        public Task<IEnumerable<WorkSampleModel>> GetOnHomeAsync();
        public Task<WorkSampleModel> UpdateAsync(UpdateWorkSampleModel model);
        public Task SetMainPictureAsync(SetMainWorkSamplePictureModel model);
    }
}
