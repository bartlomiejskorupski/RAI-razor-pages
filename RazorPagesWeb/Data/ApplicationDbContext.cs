using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RazorPagesLibrary.Model;

namespace RazorPagesWeb.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Water> Waters { get; set; }
    public DbSet<WaterType> WaterTypes { get; set; } = null!;
    public DbSet<Company> Companies { get; set; }
    public DbSet<Ion> Ions { get; set; }
    public DbSet<Packaging> Packagings { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<Pallet> Pallets { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleUnit> SaleUnits { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Water>()
            .HasMany(e => e.Ions)
            .WithMany(e => e.Waters);
    }
}
