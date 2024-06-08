using BloodBankManager.Core.Donor;

namespace BloodBankManager.Application.ViewModels;

public class DonationViewModel
{
    public int Id { get; set; }
    public int DonorId { get; set; }
    public DateTime DonationDate { get; set; }
    public int QuantityMl { get; set; }
}
