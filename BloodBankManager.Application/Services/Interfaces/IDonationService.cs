using BloodBankManager.Application.InputModels;
using BloodBankManager.Application.ViewModels;
using BloodBankManager.Core.Donor;

namespace BloodBankManager.Application.Services.Interfaces;

public interface IDonationService
{
    Task<List<DonationViewModel>> GetAllDonations();
    Task<DonationViewModel> GetDonationById(int id);
    Task<Donation> CreateDonation(CreateDonationInputModel donationInputModel);
}
