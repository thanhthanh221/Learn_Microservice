using System.Linq.Expressions;
using play.Common;

namespace Play.Common
{
    public interface IRepository<T> where T :IEntity // Tổng quát hóa đối tượng nhưng phải có ID
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(Guid Id);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Guid Id);
        Task<T> GetAsync(Expression<Func<T,bool>> filter);

        Task UpdateAsync(T entity);
    }
}