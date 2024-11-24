using AuraShop.Cargo.Business.Abstract;
using AuraShop.Cargo.Dto.CargoCompany;
using AuraShop.Cargo.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Cargo.WebAPI.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly IGenericService<CargoCompany> _cargoCompanyService;

        public CargoCompaniesController(IGenericService<CargoCompany> cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _cargoCompanyService.GetAll();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCargoCompanyDto createCargoCompanyDto)
        {
            var newCompany = new CargoCompany
            {
                Name = createCargoCompanyDto.Name
            };

            var result = await _cargoCompanyService.Create(newCompany);

            return CreatedAtAction(nameof(GetById), new { id = result.Id });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _cargoCompanyService.GetById(id);

            if (entity == null)
                return BadRequest("Invalid ID provided");

            await _cargoCompanyService.Delete(id);

            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = _cargoCompanyService.GetById(id);
            if (value == null)
                return NotFound();

            return Ok(value);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,UpdateCargoCompanyDto updataCargoCompanyDto)
        {
            var entity = await _cargoCompanyService.GetById(id);
            if (entity is null)
                return BadRequest("Invalid ID provided");

            entity.Name = updataCargoCompanyDto.Name;

            _cargoCompanyService.Update(entity);
            return NoContent();
        }
    }
}
