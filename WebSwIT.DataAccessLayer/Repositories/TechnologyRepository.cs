using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Technologies;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class TechnologyRepository : BaseRepository<Technology>, ITechnologyRepository
    {
        public TechnologyRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Technology> GetByNameAsync(string name)
        {
            return await _dbSet.AsNoTracking().Where(tech => tech.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public IQueryable<Technology> GetFilteredTechonologies(TechnologyFilterModel filter)
        {
            IQueryable<Technology> technology = _dbSet
                                                    .Include(cat => cat.Category)
                                                    .Where(cat => filter.Name == null || cat.Name.Contains(filter.Name));

            return technology;
        }
    }
}
