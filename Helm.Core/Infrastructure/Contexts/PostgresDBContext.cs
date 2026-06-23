using Helm.Core.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Infrastructure.Contexts
{
    public class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
    {
        public DateTimeOffsetConverter()
            : base(
                d => d.ToUniversalTime(),
                d => d.ToUniversalTime())
        {
        }
    }
    public class PostgresDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EquipmentTemplate> EquipmentTemplates { get; set; }
        public DbSet<EquipmentReceipt> EquipmentReceipts { get; set; }
        public PostgresDBContext(DbContextOptions<PostgresDBContext> options) : base(options)
        {
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<DateTimeOffset>()
                .HaveConversion<DateTimeOffsetConverter>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(entity => entity.Id);
                entity.HasIndex(entity => entity.Login).IsUnique();
                entity.HasIndex(entity => entity.ADLogin).IsUnique();
                entity.Property(entity => entity.Name).IsRequired();
                entity.Property(entity => entity.Version).IsRequired().HasDefaultValue(Int32.MinValue);
                entity.Property(entity => entity.Enabled).IsRequired().HasDefaultValue(true);
                entity.HasMany(entity => entity.Roles).WithMany(r => r.Users);
            });
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles");
                entity.HasKey(entity => entity.Id);
                entity.HasIndex(entity => entity.Name).IsUnique();
                entity.HasMany(entity => entity.Users).WithMany(u => u.Roles);
            });
            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.ToTable("Equipment");
                entity.HasKey(e => e.Id);
                entity.HasIndex(entity => entity.Name).IsUnique();
            });
            modelBuilder.Entity<EquipmentTemplate>(entity =>
            {
                entity.ToTable("EquipmentTemplates");
                entity.HasKey(entity => entity.Id);
                entity.HasIndex(entity => entity.Name).IsUnique();
            });
            modelBuilder.Entity<EquipmentReceipt>(entity =>
            {
                entity.ToTable("EquipmentReceipts");
                entity.HasKey(entity => entity.Id);
            });
        }
    }
}
