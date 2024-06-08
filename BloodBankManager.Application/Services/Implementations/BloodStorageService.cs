using BloodBankManager.Application.Services.Interfaces;
using BloodBankManager.Application.ViewModels;
using BloodBankManager.Core.Enums;
using BloodBankManager.Infrastructure.EmailConfig;
using BloodBankManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManager.Application.Services.Implementations;

public class BloodStorageService : IBloodStorageService
{
    private readonly BloodManagementDbContext _dbContext;
    private readonly IEmailService _emailService;
    public BloodStorageService(
        BloodManagementDbContext dbContext,
        IEmailService emailService)
    {
        _emailService = emailService;
        _dbContext = dbContext;
    }

    public async Task AddBloodStorage(int donorId, int quantityML)
    {
        var donor = await _dbContext.Donors.SingleOrDefaultAsync(d => d.Id == donorId);

        if (donor == null) throw new Exception("Donor não encontrado, tente novamente!");

        var storage = await _dbContext.BloodStorage
            .SingleOrDefaultAsync
            (s => s.BloodType == donor.BloodType && s.RhFactor == donor.RhFactor);

        storage.UpdateQuantityStorage(quantityML);

        _dbContext.BloodStorage.Update(storage);

        _dbContext.SaveChanges();
    }

    public async Task<List<BloodStorageViewModel>> GetAllBloodStorage()
    {
        var bloodStorage = await _dbContext.BloodStorage.ToListAsync();

        if (bloodStorage == null) throw new Exception("Estoques não encontrados.");

        var bloodStorageViewModel = bloodStorage.Select(bloodStorage => new BloodStorageViewModel
        {
            Id = bloodStorage.Id,
            BloodType = bloodStorage.BloodType,
            RhFactor = bloodStorage.RhFactor,
            QuantityMl = bloodStorage.QuantityMl
        }).ToList();

        return bloodStorageViewModel;
    }

    public async Task<BloodStorageViewModel> GetBloodStorageById(int id)
    {
        var bloodStorage = await _dbContext.BloodStorage.SingleOrDefaultAsync(b => b.Id == id);

        await CheckAndNotifyLowStock(bloodStorage.BloodType, bloodStorage.QuantityMl);

        if (bloodStorage == null) throw new Exception("Estoque não encontrado.");

        var bloodStorageViewModel = new BloodStorageViewModel
        {
            Id = bloodStorage.Id,
            BloodType = bloodStorage.BloodType,
            RhFactor = bloodStorage.RhFactor,
            QuantityMl = bloodStorage.QuantityMl
        };
        return bloodStorageViewModel;
    }

    public async Task CheckAndNotifyLowStock(BloodTypeEnum bloodType, int currentQuantity)
    {
        if (currentQuantity < GetMinimumQuantity(bloodType))
        {
            string subject = $"Aviso: Estoque Baixo de Sangue {bloodType}";
            string body = $"O estoque de sangue do tipo {bloodType} está baixo. Restam apenas {currentQuantity} unidades.";

            _emailService.SendEmailAsync("", subject, body);
        }
    }

    private int GetMinimumQuantity(BloodTypeEnum bloodType)
    {
        return 100;
    }
}
