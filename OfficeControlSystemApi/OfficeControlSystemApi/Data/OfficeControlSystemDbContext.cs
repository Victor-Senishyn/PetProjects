using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Data
{
    public class OfficeControlSystemDbContext : DbContext
    {
        public DbSet<AccessCard> AccessCards { get; set; }
        public DbSet<AccessHistory> AccessHistories { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Username=postgres;Database=postgres";
            optionsBuilder.UseNpgsql(connectionString, npgsqlOptionsAction: options =>
            {
                options.MigrationsAssembly(typeof(OfficeControlSystemDbContext).Assembly.FullName);
                options.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessCard>()
                .HasOne(ac => ac.Employee)
                .WithOne(e => e.AccessCard)
                .HasForeignKey<Employee>(e => e.AccessCardId);

            modelBuilder.Entity<AccessHistory>()
                .HasOne(ah => ah.AccessCard)
                .WithMany(ac => ac.AccessHistory)
                .HasForeignKey(ah => ah.AccessCardId);
        }
    }
}