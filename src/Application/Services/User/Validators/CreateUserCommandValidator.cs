using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data.Repositories;
using Bhd.Evaluacion.Integracion.Application.Services.User.Commands;
using Microsoft.Extensions.Configuration;

namespace Bhd.Evaluacion.Integracion.Application.Services.User.Validators;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IUserRepository userRepository, ICountryRepository countryRepository, IConfiguration configuration)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .Matches(configuration["RegularExpressions:Email"])
            .WithMessage(configuration["Error:Validation:UserCreation:EmailFormat"])
            .DependentRules(() =>
            {
                RuleFor(x => x.Email)
                    .MustAsync(async (email, cancellationToken) => !await userRepository.ExistsEmail(email, cancellationToken))
                    .WithMessage(configuration["Error:Validation:UserCreation:EmailAlreadyExists"]);
            });

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(configuration["RegularExpressions:Password"])
            .WithMessage(configuration["Error:Validation:UserCreation:PasswordComplexity"]);

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleForEach(x => x.Phones)
            .SetValidator(new UserPhoneDtoValidator(countryRepository, configuration));
    }
}
