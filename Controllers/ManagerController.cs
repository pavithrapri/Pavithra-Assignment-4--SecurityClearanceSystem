using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearance.Models;
using VisitorSecurityClearance.Services;
using System.Threading.Tasks;

namespace VisitorSecurityClearance.Controllers
{
    [Route("api/[[action]/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateManager(Manager manager)
        {
            await _managerService.AddManagerAsync(manager);
            return Ok(manager);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManager(string id)
        {
            var manager = await _managerService.GetManagerAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            return Ok(manager);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManager(string id, Manager manager)
        {
            var existingManager = await _managerService.GetManagerAsync(id);
            if (existingManager == null)
            {
                return NotFound();
            }

            await _managerService.UpdateManagerAsync(id, manager);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(string id)
        {
            var existingManager = await _managerService.GetManagerAsync(id);
            if (existingManager == null)
            {
                return NotFound();
            }

            await _managerService.DeleteManagerAsync(id);
            return NoContent();
        }
    }
}
