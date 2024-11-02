using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data;
using Bhd.Evaluacion.Integracion.Infrastructure.Contexts;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Data;
public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken);
}
