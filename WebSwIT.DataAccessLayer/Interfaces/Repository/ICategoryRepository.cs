using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Categories;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Interfaces.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public Task<Category> GetByNameAsync(string name);
        public IQueryable<Category> GetFilteredCategories(CategoryFilterModel filter);
    }
}
