using BloodBankManager.Application.Services.Interfaces;
using BloodBankManager.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankManager.API.Controllers;

[Route("api/[controller]")]
public class BloodStorageController : ControllerBase
{
    private readonly IBloodStorageService _bloodStorageService;
    public BloodStorageController(
        IBloodStorageService bloodStorageService)
    {
        _bloodStorageService = bloodStorageService;
    }

    /// <summary>
    /// Obtém todos os registros de armazenamento de sangue.
    /// </summary>
    /// <returns>Uma lista de registros de armazenamento de sangue.</returns>
    [HttpGet]
    public async Task<ActionResult<BloodStorageViewModel>> GetAllBloodStorage()
    {
        try
        {
            return Ok(await _bloodStorageService.GetAllBloodStorage());
        }
        catch (Exception ex)
        {
            return BadRequest(error: $"[GetAllBloodStorage] : {ex.Message}");
        }
    }

    /// <summary>
    /// Obtém um registro de armazenamento de sangue pelo ID.
    /// </summary>
    /// <param name="id">O ID do registro de armazenamento de sangue.</param>
    /// <returns>O registro de armazenamento de sangue solicitado.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BloodStorageViewModel>> GetBloodStorageById(int id)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok( await _bloodStorageService.GetBloodStorageById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(error: $"[GetBloodStorageById] : {ex.Message}");
        }
    }
}
