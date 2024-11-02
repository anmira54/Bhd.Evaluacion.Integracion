using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Services;
using Bhd.Evaluacion.Integracion.Application.Services.User.DTOs;

namespace Bhd.Evaluacion.Integracion.Application.Services.User.Commands;

public class CreateUserCommand : UserSignUpDto, IRequest<UserCreatedDto>
{
}

public class CreateUserCommandHandler(
    IUserService userService,
    IAuthService authService,
    IMapper mapper
) : IRequestHandler<CreateUserCommand, UserCreatedDto>
{
    private readonly IUserService _userService = userService;
    private readonly IAuthService _authService = authService;
    private readonly IMapper _mapper = mapper;

    public async Task<UserCreatedDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var createdUser = await _userService.Create(_mapper.Map<UserSignUpDto>(request), cancellationToken);

        var createdUserDto = _mapper.Map<UserCreatedDto>(createdUser);

        createdUserDto.Token = await _authService.GetUserToken(createdUser.Id, cancellationToken);

        return createdUserDto;
    }
}
