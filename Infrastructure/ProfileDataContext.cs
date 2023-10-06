using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EntityConfigurations;

namespace Application
{
    public class ProfileDataContext : DbContext
    {
        // private const string connectionString = @"server=localhost; database=ProfileData; User ID=sa; Password=Yukon900; TrustServerCertificate=true;";


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Avatar> Avatars { get; set; }

        public ProfileDataContext(DbContextOptions options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("DatabaseConection");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AvatarConfiguration());

        }
    }
}
