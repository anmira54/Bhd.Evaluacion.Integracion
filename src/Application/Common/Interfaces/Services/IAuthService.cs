namespace Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Services;
public interface IAuthService
{
    public Task<string> GetUserToken(Guid userId, CancellationToken cancellationToken = default);
}
