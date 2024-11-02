using Bhd.Evaluacion.Integracion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity
            .HasKey(x => x.Id);

        entity
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}
