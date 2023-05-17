using Common.Services.Interfaces;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var medicines = await _medicineService.GetAllAsync(cancellationToken);

            return Ok(medicines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var medicine = await _medicineService.GetByIdAsync(id, cancellationToken);

            return Ok(medicine);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(MedicineDto medicineDto, CancellationToken cancellationToken)
        {
            await _medicineService.CreateAsync(medicineDto, cancellationToken);

            return Ok("Medicine has been created =)");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(MedicineDto medicineDto, CancellationToken cancellationToken)
        {
            await _medicineService.UpdateAsync(medicineDto, cancellationToken);

            return Ok("Medicine has been updated =)");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            await _medicineService.DeleteByIdAsync(id, cancellationToken);

            return Ok("Medicine has been deleted =(");
        }
    }
}
