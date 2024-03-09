using BloodBankManager.Core.Donor;
using BloodBankManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManager.Infrastructure.Persistence;

public class BloodManagementDbContext : DbContext
{
    public BloodManagementDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Donor> Donors { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<BloodStorage> BloodStorage { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BloodManagementDbContext).Assembly);
    }
}
