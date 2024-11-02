namespace Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data.Repositories;

public interface ICountryRepository : IBaseRepository<Domain.Entities.Country>
{
    public Task<bool> ExistsCountryWithCity(string countryCode, string cityCode, CancellationToken cancellationToken = default);
}
