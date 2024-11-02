using System.Linq.Expressions;

namespace Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data.Repositories;
public interface IBaseRepository<T> where T : class
{
    public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    public Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    public Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}
