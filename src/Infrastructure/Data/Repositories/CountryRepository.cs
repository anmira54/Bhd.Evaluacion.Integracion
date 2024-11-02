using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data.Repositories;
using Bhd.Evaluacion.Integracion.Domain.Entities;
using Bhd.Evaluacion.Integracion.Infrastructure.Contexts;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Data.Repositories;

public class CountryRepository(ApplicationDbContext context) : BaseRepository<Country>(context), ICountryRepository
{
    public async Task<bool> ExistsCountryWithCity(string countryCode, string cityCode, CancellationToken cancellationToken = default)
        => await AnyAsync(c => c.Code == countryCode && c.Cities.Any(c => c.Code == cityCode), cancellationToken);
}
