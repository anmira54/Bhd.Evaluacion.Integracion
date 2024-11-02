using System.Linq.Expressions;
using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Data.Repositories;
public class BaseRepository<T>(DbContext context) : IBaseRepository<T> where T : class
{
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => await _dbSet.AnyAsync(predicate, cancellationToken);

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        return entity;
    }

    public virtual async Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);

        return entities;
    }
}
