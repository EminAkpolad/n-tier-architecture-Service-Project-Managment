using System.Linq.Expressions;
using EA.Core.Commons;
using EA.DataAccess.ContextC;
using Microsoft.EntityFrameworkCore;

namespace EA.DataAccess.Repositories;

public class GenericRepositories<T>:IGenericRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _appDbContext;
    private readonly DbSet<T> _dbSet;
    public GenericRepositories(AppDbContext appDbContext)
    {
        _appDbContext=appDbContext;
        _dbSet=appDbContext.Set<T>();
    }

    public async Task AddAsync(T entity)=>await _dbSet.AddAsync(entity);

    public void Delete(T entity)=> _dbSet.Remove(entity);

    public async Task<IEnumerable<T>> GetAllAsync()=>await _dbSet.ToListAsync();

    public async Task<T?> GetById(int id)=>await _dbSet.FindAsync(id);

    public void Update(T entity)=>_dbSet.Update(entity);

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }
}
