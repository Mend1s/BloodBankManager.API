using BloodBankManager.Core.Enums;

namespace BloodBankManager.Application.InputModels;

public class BloodStorageInputModel
{
    public BloodTypeEnum BloodType { get; set; }
    public RhFactorEnum RhFactor { get; set; }
    public int QuantityMl { get; set; }
}
