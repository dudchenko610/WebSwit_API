using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.Entities.Interfaces;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> 
        where TEntity : class, IBaseEntity
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task CreateAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task UpdateAsync(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task CreateRangeAsync(IEnumerable<TEntity> item)
        {
            await _dbSet.AddRangeAsync(item);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> item)
        {
            _dbSet.UpdateRange(item);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task RemoveRangeAsync(IList<TEntity> items)
        {
            _dbSet.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<int> GetCountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}
