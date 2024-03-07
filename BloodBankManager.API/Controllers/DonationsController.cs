using BloodBankManager.Application.InputModels;
using BloodBankManager.Application.ViewModels;
using BloodBankManager.Core.Donor;
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
    public DonationsController(BloodManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<DonationViewModel>> GetAllDonations()
    {
        var donations = await _dbContext.Donations.ToListAsync();

        if (donations == null)
        {
            return NotFound();
        }

        var donationsViewModel = donations.Select(donation => new DonationViewModel
        {
            Id = donation.Id,
            DonorId = donation.DonorId,
            DonationDate = donation.DonationDate,
            QuantityMl = donation.QuantityMl,
            Donor = donation.Donor
        }).ToList();

        return Ok(donationsViewModel);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DonationViewModel>> GetDonationById(int id)
    {
        var donation = await _dbContext.Donations.SingleOrDefaultAsync(d => d.Id == id);

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
    public async Task<ActionResult<DonationViewModel>> CreateDonation([FromBody] CreateDonationInputModel donationInputModel)
    {
        var donation = new Donation(
                       donationInputModel.DonorId,
                       donationInputModel.DonationDate,
                       donationInputModel.QuantityMl);

        _dbContext.Donations.Add(donation);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDonationById), new { id = donation.Id }, donation);
    }
}