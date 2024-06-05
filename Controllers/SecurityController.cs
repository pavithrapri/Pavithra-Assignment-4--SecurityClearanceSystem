using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearance.Models;
using VisitorSecurityClearance.Services;
using System.Threading.Tasks;

namespace VisitorSecurityClearance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly SecurityService _securityService;

        public SecurityController(SecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSecurity(Security security)
        {
            await _securityService.AddSecurityAsync(security);
            return Ok(security);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSecurity(string id)
        {
            var security = await _securityService.GetSecurityAsync(id);
            if (security == null)
            {
                return NotFound();
            }
            return Ok(security);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSecurity(string id, Security security)
        {
            var existingSecurity = await _securityService.GetSecurityAsync(id);
            if (existingSecurity == null)
            {
                return NotFound();
            }

            await _securityService.UpdateSecurityAsync(id, security);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurity(string id)
        {
            var existingSecurity = await _securityService.GetSecurityAsync(id);
            if (existingSecurity == null)
            {
                return NotFound();
            }

            await _securityService.DeleteSecurityAsync(id);
            return NoContent();
        }
    }
}
