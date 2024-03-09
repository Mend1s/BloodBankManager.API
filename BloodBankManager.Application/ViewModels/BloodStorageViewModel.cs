using BloodBankManager.Core.Enums;

namespace BloodBankManager.Application.ViewModels;

public class BloodStorageViewModel
{
    public int Id { get; set; }
    public BloodTypeEnum BloodType { get; set; }
    public RhFactorEnum RhFactor { get; set; }
    public int QuantityMl { get; set; }
}
