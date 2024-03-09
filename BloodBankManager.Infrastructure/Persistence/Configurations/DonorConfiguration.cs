using BloodBankManager.Core.Donor;
using BloodBankManager.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManager.Infrastructure.Persistence.Configurations;
public class DonorConfiguration : IEntityTypeConfiguration<Donor>
{
    public void Configure(EntityTypeBuilder<Donor> builder)
    {
        builder
            .HasKey(d => d.Id);

        builder
            .HasMany(d => d.Donations)
            .WithOne(d => d.Donor)
            .HasForeignKey(d => d.DonorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .OwnsOne(a => a.Address)
            .Property(s => s.Street)
            .HasColumnName("Street");

        builder
            .OwnsOne(a => a.Address)
            .Property(s => s.State)
            .HasColumnName("State");

        builder
            .OwnsOne(a => a.Address)
            .Property(c => c.City)
            .HasColumnName("City");

        builder
            .OwnsOne(a => a.Address)
            .Property(z => z.ZipCode)
            .HasColumnName("ZipCode");

        builder
            .Property(g => g.Gender)
            .HasConversion(g => g.ToString(), g => (GenderEnum)Enum.Parse(typeof(GenderEnum), g));
        
        builder
            .Property(p => p.BloodType)
            .HasConversion(b => b.ToString(), b => (BloodTypeEnum)Enum.Parse(typeof(BloodTypeEnum), b));

        builder
            .Property(r => r.RhFactor)
            .HasConversion(r => r.ToString(), r => (RhFactorEnum)Enum.Parse(typeof(RhFactorEnum), r));
    }
}
