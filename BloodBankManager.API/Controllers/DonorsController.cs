using BloodBankManager.Application.InputModels;
using BloodBankManager.Application.ViewModels;
using BloodBankManager.Core.Donor;
using BloodBankManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManager.API.Controllers;

[Route("api/[controller]")]
public class DonorsController : ControllerBase
{
    //private readonly IMediator _mediator;
    private readonly BloodManagementDbContext _dbContext;
    public DonorsController(BloodManagementDbContext dbContext)
    {
        _dbContext = dbContext;
        //_mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<DonorViewModel>> GetAllDonors()
    {
        var donors = await _dbContext.Donors.ToListAsync();

        if (donors == null)
        {
            return NotFound();
        }

        var donorsViewModel = donors.Select(donor => new DonorViewModel
        {
            Id = donor.Id,
            FullName = donor.FullName,
            Gender = donor.Gender,
            Weight = donor.Weight,
            BloodType = donor.BloodType,
            RhFactor = donor.RhFactor,
            Active = donor.Active
        }).ToList();

        return Ok(donorsViewModel);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DonorByIdViewModel>> GetDonorById(int id)
    {
        var donor = await _dbContext.Donors
            .Include(d => d.Donations)
            . SingleOrDefaultAsync(d => d.Id == id);

        if (donor == null)
        {
            return NotFound();
        }

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
            Donations = donor.Donations,
            Address = donor.Address,
            Active = donor.Active
        };

        return donorByIdViewModel;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDonor([FromBody] CreateDonorInputModel donorInputModel)
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

        return CreatedAtAction(nameof(GetDonorById), new { id = donor.Id }, donor);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DonorViewModel>> UpdateDonor(int id, [FromBody] UpdateDonorInputModel donorInputModel)
    {
        var donor = await _dbContext.Donors.SingleOrDefaultAsync(d => d.Id == donorInputModel.Id);

        if (donor == null)
        {
            return NotFound();
        }

        donor.Update(donorInputModel.FullName, donorInputModel.Gender, donorInputModel.Weight, donorInputModel.Address);

        _dbContext.Donors.Update(donor);

        await _dbContext.SaveChangesAsync();

        return Ok(donor);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DonorViewModel>> DeleteDonor(int id)
    {
        var donor = await _dbContext.Donors.SingleOrDefaultAsync(d => d.Id == id);

        if (donor == null)
        {
            return NotFound();
        }

        donor.DeactivateDonor();

        _dbContext.Donors.Update(donor);

        await _dbContext.SaveChangesAsync();

        return Ok(donor);
    }
}
