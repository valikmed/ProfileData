using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.ID);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.MiddleName)
                .HasMaxLength(100);

            builder.Property(u => u.BirthDate)
                .IsRequired();

            builder.Property(u => u.Description)
                .HasMaxLength(1024);

            builder.HasOne(u => u.Avatar)
                .WithMany()
                .HasForeignKey(u => u.AvatarID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}

