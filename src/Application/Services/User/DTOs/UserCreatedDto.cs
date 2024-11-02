using Bhd.Evaluacion.Integracion.Application.Common.Mappings;
using Bhd.Evaluacion.Integracion.Application.Services.User.Commands;

namespace Bhd.Evaluacion.Integracion.Application.Services.User.DTOs;

public record UserCreatedDto : IMapFrom<Domain.Entities.User>, IMapFrom<CreateUserCommand>
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }
    public DateTime? LastLogin { get; set; }
    public string Token { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.User, UserCreatedDto>();

        profile.CreateMap<CreateUserCommand, UserCreatedDto>();
    }
}
