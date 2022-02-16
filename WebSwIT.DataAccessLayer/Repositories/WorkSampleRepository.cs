using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.WorkSamples;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class WorkSampleRepository : BaseRepository<WorkSample>, IWorkSampleRepository
    {
        public WorkSampleRepository(ApplicationContext context) : base(context)
        {
        }

        public async override Task<WorkSample> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.WorkSamplePictures)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<WorkSample>> GetFilteredWorkSamplesAsync(WorkSampleFilterModel filter)
        {
            IQueryable<WorkSample> workSamples = _dbSet
                                                    .Include(x => x.Technology)
                                                    .Where(x => filter.Name == null || x.Name.Contains(filter.Name));

            return await workSamples.ToListAsync();
        }

        public async Task<IEnumerable<WorkSample>> GetOnHomeAsync()
        {
            return await _dbSet.AsNoTracking()
               .Include(x => x.WorkSamplePictures)
               .Where(x => x.ShowOnHome && x.MainPictureId != Guid.Empty).ToListAsync();
        }
    }
}
