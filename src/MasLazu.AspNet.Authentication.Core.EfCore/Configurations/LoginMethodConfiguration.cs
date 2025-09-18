using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.EfCore.Configurations;

public class LoginMethodConfiguration : IEntityTypeConfiguration<LoginMethod>
{
    public void Configure(EntityTypeBuilder<LoginMethod> builder)
    {
        builder.HasKey(lm => lm.Id);

        builder.Property(lm => lm.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(lm => lm.Code)
            .IsUnique();
    }
}
