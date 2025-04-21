using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using SocialMedia.Models.Context;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly SocialDbContext _context;
        protected DbSet<T> dbSet;
        public BaseRepository(SocialDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public virtual async Task<T> add(T entity){
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> update(T entity)
        {
            dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> delete(T entity)
        {
            await dbSet.Where(d => d.Id == entity.Id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<T>> getAll()
        {
            return await dbSet.ToListAsync();
        }
        public async Task<T> getById(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
