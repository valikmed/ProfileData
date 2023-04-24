using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class ProfileDataContext : DbContext
    {
        private const string connectionString = @"server=localhost; database=UserProfile; Integrated Security=true;";


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Avatar> Avatars { get; set; }

        public ProfileDataContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Role>()
        //              .Property(b => b.RoleName)
        //              .IsRequired();

        //    modelBuilder.Entity<User>()
        //           .Property(b => b.FirstName)
        //           .IsRequired();
        //    modelBuilder.Entity<User>()
        //           .Property(b => b.LastName)
        //           .IsRequired();



        //    modelBuilder.Entity<Role>().HasData(
        //        new Role
        //        {
        //            ID = 1,
        //            RoleName = "C-Level"
        //        },
        //        new Role
        //        {
        //            ID = 2,
        //            RoleName = "Manager"
        //        },
        //        new Role
        //        {
        //            ID = 3,
        //            RoleName = "Worker"
        //        }
        //    );

        //}
    }
}
