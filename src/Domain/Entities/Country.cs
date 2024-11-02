namespace Bhd.Evaluacion.Integracion.Domain.Entities;

public class Country
{
    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public ICollection<City> Cities { get; set; } = [];
}
