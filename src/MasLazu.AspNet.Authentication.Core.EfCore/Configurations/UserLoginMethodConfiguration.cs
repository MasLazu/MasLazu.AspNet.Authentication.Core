using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MasLazu.AspNet.Authentication.Core.Domain.Entities;

namespace MasLazu.AspNet.Authentication.Core.EfCore.Configurations;

public class UserLoginMethodConfiguration : IEntityTypeConfiguration<UserLoginMethod>
{
    public void Configure(EntityTypeBuilder<UserLoginMethod> builder)
    {
        builder.HasKey(ulm => ulm.Id);

        builder.Property(ulm => ulm.UserId)
            .IsRequired();

        builder.Property(ulm => ulm.LoginMethodCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(ulm => ulm.User)
            .WithMany(u => u.UserLoginMethods)
            .HasForeignKey(ulm => ulm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ulm => new { ulm.UserId, ulm.LoginMethodCode })
            .IsUnique();
    }
}
