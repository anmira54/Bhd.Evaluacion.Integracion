using Bhd.Evaluacion.Integracion.Application.Services.User.DTOs;

namespace Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Services;
public interface IUserService
{
    public Task<Domain.Entities.User> Create(UserSignUpDto userSignUpDto, CancellationToken cancellationToken = default);
}
