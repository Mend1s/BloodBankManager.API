using BloodBankManager.Application.ViewModels;
using BloodBankManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManager.API.Controllers;

[Route("api/[controller]")]
public class BloodStorageController : ControllerBase
{
    private readonly BloodManagementDbContext _dbContext;
    public BloodStorageController(BloodManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<BloodStorageViewModel>> GetAllBloodStorage()
    {
        var bloodStorage = await _dbContext.BloodStorage.ToListAsync();

        if (bloodStorage == null)
        {
            return NotFound();
        }

        var bloodStorageViewModel = bloodStorage.Select(bloodStorage => new BloodStorageViewModel
        {
            Id = bloodStorage.Id,
            BloodType = bloodStorage.BloodType,
            RhFactor = bloodStorage.RhFactor,
            QuantityMl = bloodStorage.QuantityMl
        }).ToList();

        return Ok(bloodStorageViewModel);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BloodStorageViewModel>> GetBloodStorageById(int id)
    {
        var bloodStorage = await _dbContext.BloodStorage.SingleOrDefaultAsync(b => b.Id == id);

        if (bloodStorage == null)
        {
            return NotFound();
        }

        var bloodStorageViewModel = new BloodStorageViewModel
        {
            Id = bloodStorage.Id,
            BloodType = bloodStorage.BloodType,
            RhFactor = bloodStorage.RhFactor,
            QuantityMl = bloodStorage.QuantityMl
        };

        return Ok(bloodStorageViewModel);
    }
}
