using BloodBankManager.Core.Enums;
using BloodBankManager.Core.Services;
using BloodBankManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManager.Application.Services;

public class BloodStorageService : IBloodStorageService
{
    private readonly BloodManagementDbContext _dbContext;
    public BloodStorageService(BloodManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddBloodStorage(int donorId, int quantityML)
    {
        var donor = await _dbContext.Donors.SingleOrDefaultAsync(d => d.Id == donorId);

        if (donor == null)
        {
            throw new Exception("Donor não encontrado, tente novamente!");
        }

        var storage = await _dbContext.BloodStorage
            .SingleOrDefaultAsync
            (s => s.BloodType == donor.BloodType && s.RhFactor == donor.RhFactor);

        storage.UpdateQuantityStorage(quantityML);

        _dbContext.BloodStorage.Update(storage);

        _dbContext.SaveChanges();
    }
}
