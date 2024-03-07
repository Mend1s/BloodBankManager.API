namespace BloodBankManager.Application.InputModels;

public class CreateDonationInputModel
{
    public DateTime DonationDate { get; set; }
    public int QuantityMl { get; set; }
    public int DonorId { get; set; }
}
