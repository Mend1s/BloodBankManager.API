using BloodBankManager.Application.InputModels;
using BloodBankManager.Application.Services.Interfaces;
using BloodBankManager.Application.ViewModels;
using BloodBankManager.Core.Donor;
using BloodBankManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManager.Application.Services.Implementations;

public class DonorService : IDonorService
{
    private readonly BloodManagementDbContext _dbContext;
    public DonorService(BloodManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Donor> CreateDonor(CreateDonorInputModel donorInputModel)
    {
        var donor = new Donor(
            donorInputModel.FullName,
            donorInputModel.Email,
            donorInputModel.BirthDate,
            donorInputModel.Gender,
            donorInputModel.Weight,
            donorInputModel.BloodType,
            donorInputModel.RhFactor,
            donorInputModel.Address);

        await _dbContext.Donors.AddAsync(donor);

        await _dbContext.SaveChangesAsync();

        return donor;
    }

    public async Task<DonorViewModel> DisableDonor(int id)
    {
        var donor = await _dbContext.Donors.SingleOrDefaultAsync(d => d.Id == id);

        if (donor == null) throw new Exception("Doador não encontrado.");

        donor.DeactivateDonor();

        //_dbContext.Donors.Update(donor);

        await _dbContext.SaveChangesAsync();

        return new DonorViewModel
        {
            Id = donor.Id,
            FullName = donor.FullName,
            Gender = donor.Gender,
            Weight = donor.Weight,
            BloodType = donor.BloodType,
            RhFactor = donor.RhFactor,
            Active = donor.Active
        };
    }

    public async Task<List<DonorViewModel>> GetAllDonors()
    {
        var donors = await _dbContext.Donors.ToListAsync();

        if (donors == null) throw new Exception("Doadores não encontrado.");

        var donorsViewModel = donors?.Select(donor => new DonorViewModel
        {
            Id = donor.Id,
            FullName = donor.FullName,
            Gender = donor.Gender,
            Weight = donor.Weight,
            BloodType = donor.BloodType,
            RhFactor = donor.RhFactor,
            Active = donor.Active
        }).ToList();

        return donorsViewModel;
    }

    public async Task<DonorByIdViewModel> GetDonorById(int id)
    {
        var donor = await _dbContext.Donors
                    .Include(d => d.Donations)
                    .SingleOrDefaultAsync(d => d.Id == id);

        if (donor == null) throw new Exception("Doador não encontrado.");

        var donorByIdViewModel = new DonorByIdViewModel
        {
            Id = donor.Id,
            FullName = donor.FullName,
            Email = donor.Email,
            BirthDate = donor.BirthDate,
            Gender = donor.Gender,
            Weight = donor.Weight,
            BloodType = donor.BloodType,
            RhFactor = donor.RhFactor,
            Donations = donor.Donations.Select(donation => new DonationViewModel
            {
                Id = donation.Id,
                DonorId = donation.DonorId,
                DonationDate = donation.DonationDate,
                QuantityMl = donation.QuantityMl
            }).ToList(),
            Address = donor.Address,
            Active = donor.Active
        };

        return donorByIdViewModel;
    }

    public async Task<Donor> UpdateDonor(int id, UpdateDonorInputModel donorInputModel)
    {
        var donor = await _dbContext.Donors.SingleOrDefaultAsync(d => d.Id == donorInputModel.Id);

        if (donor == null) throw new Exception("Doador não encontrado.");

        donor.Update(donorInputModel.FullName, donorInputModel.Gender, donorInputModel.Weight, donorInputModel.Address);

        _dbContext.Donors.Update(donor);

        await _dbContext.SaveChangesAsync();

        return donor;
    }
}
