using BloodBankManager.Application.InputModels;
using BloodBankManager.Application.Services.Interfaces;
using BloodBankManager.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankManager.API.Controllers;

[Route("api/[controller]")]
public class DonationsController : ControllerBase
{
    private readonly IDonationService _donationService;
    public DonationsController(
        IDonationService donationService)
    {
        _donationService = donationService;
    }


    /// <summary>
    /// Obtém todas as doações registradas.
    /// </summary>
    /// <returns>Uma lista de doações.</returns>
    [HttpGet]
    public async Task<ActionResult<DonationViewModel>> GetAllDonations()
    {
        try
        {
            return Ok(await _donationService.GetAllDonations());
        }
        catch (Exception ex)
        {
            return BadRequest(error: $"[GetAllDonations] : {ex.Message}");
        }
    }

    /// <summary>
    /// Obtém uma doação pelo ID.
    /// </summary>
    /// <param name="id">O ID da doação a ser obtida.</param>
    /// <returns>A doação solicitada.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<DonationViewModel>> GetDonationById(int id)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _donationService.GetDonationById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(error: $"[GetDonationById] : {ex.Message}");
        }
    }

    /// <summary>
    /// Cria uma nova doação.
    /// </summary>
    /// <param name="donationInputModel">Os detalhes da doação a ser criada.</param>
    /// <returns>A doação criada.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateDonation([FromBody] CreateDonationInputModel donationInputModel)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var donation = await _donationService.CreateDonation(donationInputModel);

            var donationViewModel = new DonationViewModel
            {
                Id = donation.Id,
                DonorId = donation.DonorId,
                DonationDate = donation.DonationDate,
                QuantityMl = donation.QuantityMl,
            };

            return CreatedAtAction(nameof(GetDonationById), new { id = donationViewModel.Id}, donationViewModel);
        }
        catch (Exception ex)
        {
            return BadRequest(error: $"[CreateDonation] : {ex.Message}");
        }

    }
}