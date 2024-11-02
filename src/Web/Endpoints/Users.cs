using Bhd.Evaluacion.Integracion.Application.Services.User.Commands;
using Bhd.Evaluacion.Integracion.Application.Services.User.DTOs;
using NSwag.Annotations;

namespace Bhd.Evaluacion.Integracion.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(Create);
    }


    [SwaggerResponse(StatusCodes.Status200OK, typeof(UserCreatedDto))]
    public async Task<IResult> Create(ISender sender, CreateUserCommand command, CancellationToken cancellationToken)
        => Results.Ok(await sender.Send(command, cancellationToken));
}
