using Microsoft.EntityFrameworkCore;
using Multitenant_Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multitenant_Sample.Context
{
    public class MultiTenantDbContext : DbContext
    {
        public MultiTenantDbContext(DbContextOptions<MultiTenantDbContext> options) : base(options) { }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantData> TenantData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.TenantData)
                .WithOne(d => d.Tenant)
                .HasForeignKey(d => d.TenantId);

            modelBuilder.Entity<Tenant>()
                .HasIndex(t => t.Subdomain)
                .IsUnique();

            // Seed data 
            #region seed data
            modelBuilder.Entity<Tenant>().HasData(
                    new Tenant { Id = 1, Name = "Tenant 1", Subdomain = "tenant1" }
                );

            modelBuilder.Entity<TenantData>().HasData(
                new TenantData { Id = 1, TenantId = 1, Data = "Tenant 1 Data" }
            );

            modelBuilder.Entity<Tenant>().HasData(
                new Tenant { Id = 2, Name = "Tenant 2", Subdomain = "tenant2" }
            );

            modelBuilder.Entity<TenantData>().HasData(
                new TenantData { Id = 2, TenantId = 2, Data = "Tenant 2 Data" }
            ); 
            #endregion

        }
    }
}