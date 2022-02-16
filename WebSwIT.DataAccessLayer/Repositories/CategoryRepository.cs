using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Categories;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository 
    {
        public CategoryRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _dbSet.AsNoTracking().Where(tech => tech.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public IQueryable<Category> GetFilteredCategories(CategoryFilterModel filter)
        {
            IQueryable<Category> technology = _dbSet
                                                    .Include(cat => cat.Technologies)
                                                    .Where(cat => filter.Name == null || cat.Name.Contains(filter.Name));

            return technology;
        }
    }
}
