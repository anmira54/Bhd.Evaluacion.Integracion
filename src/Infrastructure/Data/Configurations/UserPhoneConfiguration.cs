using Bhd.Evaluacion.Integracion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Data.Configurations;
public class UserPhoneConfiguration : IEntityTypeConfiguration<UserPhone>
{
    public void Configure(EntityTypeBuilder<UserPhone> entity)
    {
        entity.HasKey(e => new { e.UserId, e.Number });
    }
}
