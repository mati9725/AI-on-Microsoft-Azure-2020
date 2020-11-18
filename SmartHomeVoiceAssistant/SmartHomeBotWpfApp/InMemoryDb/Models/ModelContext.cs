using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace InMemoryDb.Models
{
    public class ModelContext : DbContext
    {
        public ModelContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Intent> Intent { get; set; }
        public DbSet<LuisEntity> LuisEntity { get; set; }
        public DbSet<Device> Device { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
