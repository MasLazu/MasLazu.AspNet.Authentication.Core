using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.EfCore.Configurations;

public class TimezoneConfiguration : IEntityTypeConfiguration<Timezone>
{
    public void Configure(EntityTypeBuilder<Timezone> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Identifier)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.OffsetMinutes)
            .IsRequired();

        builder.HasIndex(t => t.Identifier)
            .IsUnique();
    }
}
