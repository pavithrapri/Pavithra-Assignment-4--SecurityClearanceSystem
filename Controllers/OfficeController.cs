using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearance.Models;
using VisitorSecurityClearance.Services;
using System;
using System.Threading.Tasks;

namespace VisitorSecurityClearance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly OfficeService _officeService;

        public OfficeController(OfficeService officeService)
        {
            _officeService = officeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffice(Office office)
        {
            await _officeService.AddOfficeAsync(office);
            return Ok(office);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffice(string id)
        {
            var office = await _officeService.GetOfficeAsync(id);
            if (office == null)
            {
                return NotFound();
            }
            return Ok(office);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffice(string id, Office office)
        {
            var existingOffice = await _officeService.GetOfficeAsync(id);
            if (existingOffice == null)
            {
                return NotFound();
            }

            await _officeService.UpdateOfficeAsync(id, office);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffice(string id)
        {
            var existingOffice = await _officeService.GetOfficeAsync(id);
            if (existingOffice == null)
            {
                return NotFound();
            }

            await _officeService.DeleteOfficeAsync(id);
            return NoContent();
        }
    }
}
