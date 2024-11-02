using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data.Repositories;
using Bhd.Evaluacion.Integracion.Domain.Entities;
using Bhd.Evaluacion.Integracion.Infrastructure.Contexts;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Data.Repositories;
public class UserRepository(ApplicationDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<bool> ExistsEmail(string email, CancellationToken cancellationToken = default)
        => await AnyAsync(x => x.Email == email, cancellationToken);

    public async Task<User?> Get(Guid id, CancellationToken cancellationToken = default)
        => await GetFirstOrDefault(x => x.Id == id, cancellationToken);
}
