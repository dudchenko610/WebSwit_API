using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.WorkSamples;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Interfaces.Repository
{
    public interface IWorkSampleRepository : IBaseRepository<WorkSample>
    {
        public Task<IEnumerable<WorkSample>> GetFilteredWorkSamplesAsync(WorkSampleFilterModel filter);
        public Task<IEnumerable<WorkSample>> GetOnHomeAsync();
    }
}
