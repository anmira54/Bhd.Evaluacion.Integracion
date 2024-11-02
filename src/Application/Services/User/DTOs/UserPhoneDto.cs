namespace Bhd.Evaluacion.Integracion.Application.Services.User.DTOs;

public record UserPhoneDto
{
    public string Number { get; set; } = string.Empty;
    public string CityCode { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
}
