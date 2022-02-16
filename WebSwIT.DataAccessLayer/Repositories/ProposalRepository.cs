using System.Linq;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Proposals;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class ProposalRepository : BaseRepository<Proposal>, IProposalRepository
    {
        public ProposalRepository(ApplicationContext context) : base(context)
        {
        }

        public IQueryable<Proposal> GetFiltered(ProposalFilterModel filter)
        {
            IQueryable<Proposal> proposals = _dbSet
                                        .Where(ord => filter.Email == null || ord.Email.Contains(filter.Email))
                                        .Where(ord => filter.PhoneNumber == null || ord.PhoneNumber.Contains(filter.PhoneNumber));

            return proposals;
        }
    }
}
