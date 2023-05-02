using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(b => b.RoleName)
                   .IsRequired();

            builder.HasData(
                new Role
                {
                    ID = 1,
                    RoleName = "C-Level"
                },
                new Role
                {
                    ID = 2,
                    RoleName = "Manager"
                },
                new Role
                {
                    ID = 3,
                    RoleName = "Worker"
                }
            );
        }
    }
}


