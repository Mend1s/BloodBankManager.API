using BloodBankManager.Application.ViewModels;

namespace BloodBankManager.Application.Services.Interfaces;

public interface IBloodStorageService
{
    Task<List<BloodStorageViewModel>> GetAllBloodStorage();
    Task<BloodStorageViewModel> GetBloodStorageById(int id);
    Task AddBloodStorage(int donorId, int quantityML);
}
