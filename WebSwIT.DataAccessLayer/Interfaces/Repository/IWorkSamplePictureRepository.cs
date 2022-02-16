using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Interfaces.Repository
{
    public interface IWorkSamplePictureRepository : IBaseRepository<WorkSamplePicture>
    {
        Task<IEnumerable<WorkSamplePicture>> GetByWorkSampleIdAsync(Guid workSampleId);
    }
}
