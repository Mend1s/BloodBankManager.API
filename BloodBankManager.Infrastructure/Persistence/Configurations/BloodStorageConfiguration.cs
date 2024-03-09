using BloodBankManager.Core.Entities;
using BloodBankManager.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManager.Infrastructure.Persistence.Configurations;

public class BloodStorageConfiguration : IEntityTypeConfiguration<BloodStorage>
{
    public void Configure(EntityTypeBuilder<BloodStorage> builder)
    {
        builder
            .HasKey(s => s.Id);

        builder
            .Property(p => p.BloodType)
            .HasConversion(b => b.ToString(), b => (BloodTypeEnum)Enum.Parse(typeof(BloodTypeEnum), b));

        builder
            .Property(r => r.RhFactor)
            .HasConversion(r => r.ToString(), r => (RhFactorEnum)Enum.Parse(typeof(RhFactorEnum), r));
    }
}
