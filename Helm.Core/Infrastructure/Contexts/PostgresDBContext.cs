using Helm.Core.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Infrastructure.Contexts
{
    public class PostgresDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public PostgresDBContext(DbContextOptions<PostgresDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.HasIndex(entity => entity.Login).IsUnique();
                entity.HasIndex(entity => entity.ADLogin).IsUnique();
                entity.Property(entity => entity.Name).IsRequired();
                entity.Property(entity => entity.Name).IsRequired();
                entity.Property(entity => entity.Version).IsRequired().HasDefaultValue(Int32.MinValue);
                entity.Property(entity => entity.Enabled).IsRequired().HasDefaultValue(true);
                entity.Property(entity => entity.Deleted).IsRequired().HasDefaultValue(false);
                entity.HasMany(entity => entity.Roles).WithMany(r => r.Users);
            });
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles");
                entity.HasKey(e => e.Id);
                entity.HasIndex(entity => entity.Name).IsUnique();
                entity.HasMany(entity=>entity.Users).WithMany(u => u.Roles);
            });
        }
    }
}
