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
    /// Obt�m todas as doa��es registradas.
    /// </summary>
    /// <returns>Uma lista de doa��es.</returns>
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
    /// Obt�m uma doa��o pelo ID.
    /// </summary>
    /// <param name="id">O ID da doa��o a ser obtida.</param>
    /// <returns>A doa��o solicitada.</returns>
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
    /// Cria uma nova doa��o.
    /// </summary>
    /// <param name="donationInputModel">Os detalhes da doa��o a ser criada.</param>
    /// <returns>A doa��o criada.</returns>
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