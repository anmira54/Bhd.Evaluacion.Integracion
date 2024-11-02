namespace Bhd.Evaluacion.Integracion.Domain.Entities;

public class UserPhone
{
    public Guid UserId { get; set; }
    public string Number { get; set; } = null!;
    public string CityCode { get; set; } = null!;
    public string CountryCode { get; set; } = null!;
}
