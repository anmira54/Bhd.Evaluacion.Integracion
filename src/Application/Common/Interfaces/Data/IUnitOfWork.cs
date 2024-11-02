namespace Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data;
public interface IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
