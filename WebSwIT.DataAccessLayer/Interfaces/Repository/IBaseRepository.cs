using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.Entities.Interfaces;

namespace WebSwIT.DataAccessLayer.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> 
        where TEntity : class, IBaseEntity
    {
        public Task<TEntity> GetByIdAsync(Guid id);
        public Task UpdateAsync(TEntity item);
        public Task CreateAsync(TEntity item);
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task DeleteAsync(TEntity item);
        public Task CreateRangeAsync(IEnumerable<TEntity> item);
        public Task UpdateRangeAsync(IEnumerable<TEntity> item);
        public Task RemoveRangeAsync(IList<TEntity> items);
        public Task<int> GetCountAsync();
    }
}
