using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{

    public class AvatarConfiguration : IEntityTypeConfiguration<Avatar>
    {
        public void Configure(EntityTypeBuilder<Avatar> builder)
        {
            builder.HasKey(e => e.ID);

            builder.Property(e => e.Image)
                .IsRequired();

            builder.HasMany(e => e.Users)
                .WithOne(e => e.Avatar)
                .HasForeignKey(e => e.AvatarID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}

