using Bhd.Evaluacion.Integracion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Data.Configurations;
public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> entity)
    {
        entity.HasKey(e => new { e.CountryCode, e.Code });
    }
}
