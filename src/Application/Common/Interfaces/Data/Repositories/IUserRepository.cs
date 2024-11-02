namespace Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data.Repositories;
public interface IUserRepository : IBaseRepository<Domain.Entities.User>
{
    public Task<Domain.Entities.User?> Get(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> ExistsEmail(string email, CancellationToken cancellationToken = default);
}
