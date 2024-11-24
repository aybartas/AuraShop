using AuraShop.Cargo.Business.Abstract;
using AuraShop.Cargo.Dto.Cargo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Cargo.WebAPI.Controller;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CargosController : ControllerBase
{
    private readonly IGenericService<Entity.Concrete.Cargo> _cargoService;

    public CargosController(IGenericService<Entity.Concrete.Cargo> cargoService)
    {
        _cargoService = cargoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var values = await _cargoService.GetAll();
        return Ok(values);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCargoDto dto)
    {
        var newCargo = new Entity.Concrete.Cargo
        {
            Id = 0,
            OrderNumber = dto.OrderNumber,
            TrackingNumber = dto.TrackingNumber,
            CargoCompanyId = dto.CargoCompanyId,
            Status = dto.Status,
            ShippedDate = dto.ShippedDate,
            EstimatedDeliveryDate = dto.EstimatedDeliveryDate,
            DeliveredDate = dto.DeliveredDate
        };

        var result = await _cargoService.Create(newCargo);

        return CreatedAtAction(nameof(GetById), new { id = result.Id });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        var entity = await _cargoService.GetById(id);

        if (entity == null)
            return BadRequest("Invalid ID provided");

        await _cargoService.Delete(id);

        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var value = _cargoService.GetById(id);
        if (value == null)
            return NotFound();

        return Ok(value);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCargoDto dto)
    {
        var entity = await _cargoService.GetById(id);
        if (entity is null)
            return BadRequest("Invalid ID provided");

        entity.OrderNumber = dto.OrderNumber;
        entity.TrackingNumber = dto.TrackingNumber;
        entity.CargoCompanyId = dto.CargoCompanyId;
        entity.ShippedDate = dto.ShippedDate;
        entity.EstimatedDeliveryDate = dto.EstimatedDeliveryDate;
        entity.DeliveredDate = dto.DeliveredDate;

        _cargoService.Update(entity);
        return NoContent();
    }
}