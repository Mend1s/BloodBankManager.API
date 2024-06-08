using BloodBankManager.Application.InputModels;
using BloodBankManager.Application.Services.Interfaces;
using BloodBankManager.Application.ViewModels;
using BloodBankManager.Core.Donor;
using BloodBankManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManager.Application.Services.Implementations;

public class DonationService : IDonationService
{
    private readonly IBloodStorageService _storageService;
    private readonly BloodManagementDbContext _dbContext;
    public DonationService(
        BloodManagementDbContext dbContext,
        IBloodStorageService storageService)
    {
        _dbContext = dbContext;
        _storageService = storageService;
    }
    public async Task<Donation> CreateDonation(CreateDonationInputModel donationInputModel)
    {
        var donor = await _dbContext.Donors.SingleOrDefaultAsync(d => d.Id == donationInputModel.DonorId);

        if (donor == null) throw new Exception("Doador não encontrado.");

        if (!donor.Active) throw new Exception("Doador com cadastro inativo, por favor reativar.");

        var donorCheck = donor.CheckDonor(donor);

        if (!donorCheck) throw new Exception("Doador não pode doar sangue.");

        var donation = new Donation(
                       donationInputModel.DonorId,
                       donationInputModel.DonationDate,
                       donationInputModel.QuantityMl);

        var donationCheck = donation.CheckMilimiterToDonation(donationInputModel.QuantityMl);

        if (!donationCheck) throw new Exception("Quantidade de sangue doada não permitida.");

        _dbContext.Donations.Add(donation);

        await _storageService.AddBloodStorage(donationInputModel.DonorId, donationInputModel.QuantityMl);

        await _dbContext.SaveChangesAsync();

        return donation;
    }

    public async Task<List<DonationViewModel>> GetAllDonations()
    {
        var donations = await _dbContext.Donations.Include(d => d.Donor).ToListAsync();

        if (donations == null) throw new Exception("Doações não encontradas.");

        var donationsViewModel = donations?.Select(donation => new DonationViewModel
        {
            Id = donation.Id,
            DonorId = donation.DonorId,
            DonationDate = donation.DonationDate,
            QuantityMl = donation.QuantityMl,
        }).ToList();

        return donationsViewModel;
    }

    public async Task<DonationViewModel> GetDonationById(int id)
    {
        var donation = await _dbContext.Donations
                    .Include(d => d.Donor)
                    .SingleOrDefaultAsync(d => d.Id == id);

        if (donation == null) throw new Exception("Doação não encontrada.");

        var donationViewModel = new DonationViewModel
        {
            Id = donation.Id,
            DonorId = donation.DonorId,
            DonationDate = donation.DonationDate,
            QuantityMl = donation.QuantityMl,
        };

        return donationViewModel;
    }
}
