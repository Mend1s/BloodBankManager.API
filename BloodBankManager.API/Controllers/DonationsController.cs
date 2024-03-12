using BloodBankManager.Application.InputModels;
using BloodBankManager.Application.Services;
using BloodBankManager.Application.ViewModels;
using BloodBankManager.Core.Donor;
using BloodBankManager.Core.Services;
using BloodBankManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManager.API.Controllers;

[Route("api/[controller]")]
public class DonationsController : ControllerBase
{
    // TODO
    // 1. add dbcontext - feito
    // 2. add get all donations - feito
    // 3. add get donation by id - feito
    // 4. add post donation - feito
    // 5. add validation - adicionar depois de finalizar os crud's
    // 6. add last 30 days donations

    private readonly BloodManagementDbContext _dbContext;
    private readonly IBloodStorageService _storageService;
    public DonationsController(
        BloodManagementDbContext dbContext,
        IBloodStorageService storageService)
    {
        _dbContext = dbContext;
        _storageService = storageService;
    }

    [HttpGet]
    public async Task<ActionResult<DonationViewModel>> GetAllDonations()
    {
        var donations = await _dbContext.Donations.Include(d => d.Donor).ToListAsync();
            
        if (donations == null)
        {
            return NotFound();
        }

        //var donationsViewModel = donations.Select(donation => new DonationViewModel
        //{
        //    Id = donation.Id,
        //    DonorId = donation.DonorId,
        //    DonationDate = donation.DonationDate,
        //    QuantityMl = donation.QuantityMl,
        //    Donor = donation.Donor
        //}).ToList();

        return Ok(donations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DonationViewModel>> GetDonationById(int id)
    {
        var donation = await _dbContext.Donations
            .Include(d => d.Donor)
            .SingleOrDefaultAsync(d => d.Id == id);

        if (donation == null)
        {
            return NotFound();
        }

        var donationViewModel = new DonationViewModel
        {
            Id = donation.Id,
            DonorId = donation.DonorId,
            DonationDate = donation.DonationDate,
            QuantityMl = donation.QuantityMl,
            Donor = donation.Donor
        };

        return Ok(donationViewModel);
    }

    [HttpPost]
    public async Task<ActionResult> CreateDonation([FromBody] CreateDonationInputModel donationInputModel)
    {
        var donor = await _dbContext.Donors.SingleOrDefaultAsync(d => d.Id == donationInputModel.DonorId);

        if (donor == null) NotFound();

        var donation = new Donation(
                       donationInputModel.DonorId,
                       donationInputModel.DonationDate,
                       donationInputModel.QuantityMl);

        _dbContext.Donations.Add(donation);

        await _storageService.AddBloodStorage(donationInputModel.DonorId, donationInputModel.QuantityMl);

        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDonationById), new { id = donation.Id }, donation);
    }
}