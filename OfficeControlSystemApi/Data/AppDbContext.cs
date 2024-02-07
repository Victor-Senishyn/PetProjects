﻿using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Models;
using System.Reflection.Metadata;

namespace OfficeControlSystemApi.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessCard>()
                .HasMany(ac => ac.VisitHistories)
                .WithOne(vh => vh.AccessCard)
                .HasForeignKey(vh => vh.AccessCardId);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<AccessCard> AccessCards { get; set; }
        public DbSet<VisitHistory> VisitHistories { get; set; }
    }
}
