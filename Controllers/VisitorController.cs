using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearance.Models;
using VisitorSecurityClearance.Services;
using System;
using System.Threading.Tasks;

namespace VisitorSecurityClearance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorService _visitorService;
        private readonly EmailService _emailService;
        private readonly PassService _passService;

        public VisitorController(IVisitorService visitorService, EmailService emailService, PassService passService)
        {
            _visitorService = visitorService;
            _emailService = emailService;
            _passService = passService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisitor([FromBody] Visitor visitor)
        {
            try
            {
                var existingVisitor = await _visitorService.GetVisitorAsync(visitor.Email);
                if (existingVisitor != null)
                {
                    return BadRequest("Visitor already exists.");
                }

                await _visitorService.AddVisitorAsync(visitor);
                var pdfBytes = _passService.GeneratePassPdf(visitor);

                // Send email with PDF
                var pdfBase64 = Convert.ToBase64String(pdfBytes);
                await _emailService.SendEmailAsync(visitor.Email, "Visitor Pass", "Here is your visitor pass.", pdfBase64);

                return Ok(visitor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitor(string id)
        {
            try
            {
                var visitor = await _visitorService.GetVisitorAsync(id);
                if (visitor == null)
                {
                    return NotFound();
                }
                return Ok(visitor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVisitor(string id, [FromBody] Visitor visitor)
        {
            try
            {
                var existingVisitor = await _visitorService.GetVisitorAsync(id);
                if (existingVisitor == null)
                {
                    return NotFound();
                }

                await _visitorService.UpdateVisitorAsync(id, visitor);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(string id)
        {
            try
            {
                var existingVisitor = await _visitorService.GetVisitorAsync(id);
                if (existingVisitor == null)
                {
                    return NotFound();
                }

                await _visitorService.DeleteVisitorAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetVisitorsByStatus([FromQuery] string status)
        {
            try
            {
                var query = $"SELECT * FROM c WHERE c.Status = '{status}'";
                var visitors = await _visitorService.GetVisitorsAsync(query);
                return Ok(visitors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
