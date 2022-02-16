using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Technologies;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Interfaces.Repository
{
    public interface ITechnologyRepository : IBaseRepository<Technology>
    {
        public Task<Technology> GetByNameAsync(string name);
        public IQueryable<Technology> GetFilteredTechonologies(TechnologyFilterModel filter);
    }
}
