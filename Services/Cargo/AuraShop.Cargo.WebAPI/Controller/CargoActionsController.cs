using AuraShop.Cargo.Business.Abstract;
using AuraShop.Cargo.Dto.CargoAction;
using AuraShop.Cargo.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.Cargo.WebAPI.Controller;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CargoActionsController : ControllerBase
{
    private readonly IGenericService<CargoAction> _cargoActionService;

    public CargoActionsController(IGenericService<CargoAction> cargoActionService)
    {
        _cargoActionService = cargoActionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var values = await _cargoActionService.GetAll();
        return Ok(values);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCargoActionDto dto)
    {
        var newCompany = new CargoAction
        {
            CargoId = dto.CargoId,
            ActionDate = dto.ActionDate,
            Message = dto.Message
        };

        var result = await _cargoActionService.Create(newCompany);

        return CreatedAtAction(nameof(GetById), new { id = result.Id });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        var entity = await _cargoActionService.GetById(id);

        if (entity == null)
            return BadRequest("Invalid ID provided");

        await _cargoActionService.Delete(id);

        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var value = _cargoActionService.GetById(id);
        if (value == null)
            return NotFound();

        return Ok(value);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCargoActionDto dto)
    {
        var entity = await _cargoActionService.GetById(id);
        if (entity is null)
            return BadRequest("Invalid ID provided");

        entity.CargoId = dto.CargoId;
        entity.ActionDate = dto.ActionDate;
        entity.Message = dto.Message;
        _cargoActionService.Update(entity);
        return NoContent();
    }
}