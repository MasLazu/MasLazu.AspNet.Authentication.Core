using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.EfCore.Configurations;

public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.ToTable("user_refresh_tokens");

        builder.HasKey(urt => urt.Id);

        builder.Property(urt => urt.UserId)
            .IsRequired();

        builder.Property(urt => urt.RefreshTokenId)
            .IsRequired();

        builder.HasOne(urt => urt.User)
            .WithMany()
            .HasForeignKey(urt => urt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(urt => urt.RefreshToken)
            .WithMany()
            .HasForeignKey(urt => urt.RefreshTokenId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(urt => new { urt.UserId, urt.RefreshTokenId })
            .IsUnique();
    }
}
