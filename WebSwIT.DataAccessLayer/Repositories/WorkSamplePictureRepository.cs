using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class WorkSamplePictureRepository : BaseRepository<WorkSamplePicture>, IWorkSamplePictureRepository
    {
        public WorkSamplePictureRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WorkSamplePicture>> GetByWorkSampleIdAsync(Guid workSampleId)
        {
            IQueryable<WorkSamplePicture> workSamplePictures = _dbSet
                                                    .Where(x => x.WorkSampleId == workSampleId);

            return await workSamplePictures.ToListAsync();
        }
    }
}
