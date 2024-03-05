using BloodBankManager.Application.InputModels;
using BloodBankManager.Application.ViewModels;
using BloodBankManager.Core.Donor;

namespace BloodBankManager.Application.Services.Interfaces;

public interface IDonorService
{
    Task<Donor> CreateDonorAsync(CreateDonorInputModel inputModel);
    Task<Donor> UpdateDonorAsync(UpdateDonorInputModel inputModel);
    Task<Donor> DeleteDonorAsync(int id);
    Task<Donor> GetDonorByIdAsync(int id);
    Task<IEnumerable<DonorViewModel>> GetAllDonorsAsync();
    //Task<IEnumerable<Donor>> GetDonorsByBloodTypeAsync(BloodTypeEnum bloodType);
    //Task<IEnumerable<Donor>> GetDonorsByRhFactorAsync(RhFactorEnum rhFactor);
    //Task<IEnumerable<Donor>> GetDonorsByGenderAsync(GenderEnum)
}
