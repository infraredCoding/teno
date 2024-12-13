using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace tenants.Models;

public class TenantContext : DbContext{
    public DbSet<Property> Properties { get; set; }
    public DbSet<Tenant> Tenants { get; set; }

    public string DbPath { get; }

    public TenantContext(DbContextOptions<TenantContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Property>()
            .HasOne(e => e.Tenant)
            .WithOne(e => e.Property)
            .HasForeignKey<Tenant>(e => e.PropertyId)
            .IsRequired(false);
    }
} 

public class Property {
    public int Id { get; set;}
    public string Name { get; set;}
    public decimal Rent { get; set; }

    public Tenant? Tenant { get; set; }
}

public class Tenant {
    public int Id { get; set;}
    public string Name { get; set;}
    public string FathersName { get; set; } = string.Empty;

    public int? PropertyId { get; set; }

    [JsonIgnore]
    public Property? Property { get; set;}
}