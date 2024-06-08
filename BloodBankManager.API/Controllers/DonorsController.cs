using BloodBankManager.Application.InputModels;
using BloodBankManager.Application.Services.Interfaces;
using BloodBankManager.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DonorsController : ControllerBase
{
    private readonly IDonorService _donorService;
    public DonorsController(
        IDonorService donorService)
    {
        _donorService = donorService;
    }

    /// <summary>
    /// Obtém todos os doadores.
    /// </summary>
    /// <returns>Uma lista de doadores.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DonorViewModel>> GetAllDonors()
    {
        try
        {
            return Ok(await _donorService.GetAllDonors());
        }
        catch (Exception ex)
        {
            return BadRequest(error: $"[GetAllDonors] : {ex.Message}");
        }
    }

    /// <summary>
    /// Obtém um doador pelo ID.
    /// </summary>
    /// <param name="id">O ID do doador a ser obtido.</param>
    /// <returns>O doador solicitado.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DonorByIdViewModel>> GetDonorById(int id)
    {
        try
        {
            return Ok(await _donorService.GetDonorById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(error: $"[GetDonorById] : {ex.Message}");
        }
    }

    /// <summary>
    /// Cria um novo doador.
    /// </summary>
    /// <param name="donorInputModel">Os detalhes do doador a ser criado.</param>
    /// <returns>O doador criado.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDonor([FromBody] CreateDonorInputModel donorInputModel)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var donor = await _donorService.CreateDonor(donorInputModel);

            return CreatedAtAction(nameof(GetDonorById), new { id = donor.Id }, donor);
        }
        catch (Exception ex)
        {
            return BadRequest(error: $"[CreateDonor] : {ex.Message}");
        }
    }

    /// <summary>
    /// Atualiza um doador existente.
    /// </summary>
    /// <param name="id">O ID do doador a ser atualizado.</param>
    /// <param name="donorInputModel">Os novos detalhes do doador.</param>
    /// <returns>O doador atualizado.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DonorViewModel>> UpdateDonor(int id, [FromBody] UpdateDonorInputModel donorInputModel)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _donorService.UpdateDonor(id, donorInputModel));
        }
        catch (Exception ex)
        {
            return BadRequest(error: $"[UpdateDonor] : {ex.Message}");
        }
    }

    /// <summary>
    /// Desativa um doador pelo ID.
    /// </summary>
    /// <param name="id">ID do doador a ser desativado.</param>
    /// <returns>O doador desativado.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DonorViewModel>> DeleteDonor(int id)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _donorService.DisableDonor(id));
        }
        catch (Exception ex)
        {
            return BadRequest(error: $"[DeleteDonor] : {ex.Message}");
        }
    }
}
