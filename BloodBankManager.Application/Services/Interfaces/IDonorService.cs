using BloodBankManager.Application.InputModels;
using BloodBankManager.Application.ViewModels;
using BloodBankManager.Core.Donor;

namespace BloodBankManager.Application.Services.Interfaces;

public interface IDonorService
{
    Task<List<DonorViewModel>> GetAllDonors();
    Task<DonorByIdViewModel> GetDonorById(int id);
    Task<Donor> CreateDonor(CreateDonorInputModel donorInputModel);
    Task<Donor> UpdateDonor(int id, UpdateDonorInputModel donorInputModel);
    Task<DonorViewModel> DisableDonor(int id);
}
