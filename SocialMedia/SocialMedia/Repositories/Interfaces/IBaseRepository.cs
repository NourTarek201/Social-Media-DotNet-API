using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IBaseRepository<T> 
    {
        public Task<T> add(T entity);
        public Task<T> update(T entity);
        public Task<T> delete(T entity);
        public Task<IEnumerable<T>> getAll();
        public Task<T> getById(Guid id);
        

    }
}
