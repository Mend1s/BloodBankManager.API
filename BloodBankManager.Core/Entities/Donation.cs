using BloodBankManager.Core.Entities;

namespace BloodBankManager.Core.Donor;

public class Donation : BaseEntity
{
    public Donation(int donorId, DateTime donationDate, int quantityMl)
    {
        DonorId = donorId;
        DonationDate = donationDate;
        QuantityMl = quantityMl;
    }

    public int DonorId { get; private set; }
    public DateTime DonationDate { get; private set; }
    public int QuantityMl { get; private set; }
    public Donor Donor { get; private set; }

    public void UpdateDonation(int donorId, int quantityMl)
    {
        DonorId = donorId;
        QuantityMl = quantityMl;
    }
}