namespace BloodBankManager.Core.Services;

public interface IBloodStorageService
{
    Task AddBloodStorage(int donorId, int quantityML);
}
