using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.EfCore.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .HasMaxLength(255);

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(u => u.Username)
            .HasMaxLength(50);

        builder.Property(u => u.LanguageCode)
            .HasMaxLength(10);

        builder.Property(u => u.ProfilePicture)
            .HasMaxLength(500);

        builder.Property(u => u.GenderCode)
            .HasMaxLength(10);

        builder.HasOne(u => u.Timezone)
            .WithMany()
            .HasForeignKey(u => u.TimezoneId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(u => u.Gender)
            .WithMany()
            .HasForeignKey(u => u.GenderCode)
            .HasPrincipalKey(g => g.Code)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(u => u.Language)
            .WithMany()
            .HasForeignKey(u => u.LanguageCode)
            .HasPrincipalKey(l => l.Code)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.UserLoginMethods)
            .WithOne(ulm => ulm.User)
            .HasForeignKey(ulm => ulm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(u => u.Email);
        builder.HasIndex(u => u.Username);
        builder.HasIndex(u => u.PhoneNumber);
    }
}
