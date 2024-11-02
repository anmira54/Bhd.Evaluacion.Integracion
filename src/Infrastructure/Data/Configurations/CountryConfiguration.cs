using Bhd.Evaluacion.Integracion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Data.Configurations;
public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> entity)
    {
        entity.HasKey(e => e.Code);

        entity.HasMany(e => e.Cities)
            .WithOne()
            .HasForeignKey(e => e.CountryCode);
    }
}
