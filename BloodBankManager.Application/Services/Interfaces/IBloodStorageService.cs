using BloodBankManager.Application.ViewModels;
using BloodBankManager.Core.Enums;

namespace BloodBankManager.Application.Services.Interfaces;

public interface IBloodStorageService
{
    Task<List<BloodStorageViewModel>> GetAllBloodStorage();
    Task<BloodStorageViewModel> GetBloodStorageById(int id);
    Task AddBloodStorage(int donorId, int quantityML);
    Task CheckAndNotifyLowStock(BloodTypeEnum bloodType, int currentQuantity);
}
