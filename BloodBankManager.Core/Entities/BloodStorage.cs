using BloodBankManager.Core.Enums;

namespace BloodBankManager.Core.Entities;

public class BloodStorage : BaseEntity
{
    public BloodStorage() { }
    public BloodStorage(BloodTypeEnum bloodType, RhFactorEnum rhFactor, int quantityMl)
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        QuantityMl = quantityMl;
    }

    public BloodTypeEnum BloodType{ get; private set; }
    public RhFactorEnum RhFactor { get; private set; }
    public int QuantityMl { get; private set; }

    public void UpdateQuantityStorage(int quantityMl)
    {
        QuantityMl += quantityMl;
    }
}
