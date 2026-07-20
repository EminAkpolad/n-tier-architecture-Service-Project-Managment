using System.Linq.Expressions;
using EA.Core.Commons;

namespace EA.DataAccess.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    Task<T?> GetById(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Delete(T entity);
    void Update(T entity);
}
