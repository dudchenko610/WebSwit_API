using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Proposals;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Interfaces.Repository
{
    public interface IProposalRepository : IBaseRepository<Proposal>
    {
        public IQueryable<Proposal> GetFiltered(ProposalFilterModel filter);
    }
}
