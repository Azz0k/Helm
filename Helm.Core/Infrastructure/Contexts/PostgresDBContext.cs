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
        public PostgresDBContext(DbContextOptions<PostgresDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
