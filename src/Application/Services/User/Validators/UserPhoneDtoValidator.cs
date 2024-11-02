using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data.Repositories;
using Bhd.Evaluacion.Integracion.Application.Services.User.DTOs;
using Microsoft.Extensions.Configuration;

namespace Bhd.Evaluacion.Integracion.Application.Services.User.Validators;
public class UserPhoneDtoValidator : AbstractValidator<UserPhoneDto>
{
    public UserPhoneDtoValidator(ICountryRepository countryRepository, IConfiguration configuration)
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .Matches(configuration["RegularExpressions:Phone"])
            .WithMessage(configuration["Error:Validation:UserCreation:PhoneFormat"]);

        RuleFor(x => x.CountryCode)
            .NotEmpty()
            .WithMessage(configuration["Error:Validation:UserCreation:EmptyCountryCode"])
            .DependentRules(() =>
            {
                RuleFor(x => x.CityCode)
                    .NotEmpty()
                    .WithMessage(configuration["Error:Validation:UserCreation:EmptyCityCode"])
                    .DependentRules(() =>
                    {
                        RuleFor(x => x)
                            .MustAsync(async (phone, cancellationToken) => await countryRepository.ExistsCountryWithCity(phone.CountryCode, phone.CityCode, cancellationToken))
                            .WithMessage(configuration["Error:Validation:UserCreation:CountryCityNotFound"]);
                    });
            });
    }
}
