using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorSecurityClearance.Models;

namespace VisitorSecurityClearance.Services
{
    public interface IOfficeService
    {
        Task<IEnumerable<Office>> GetOfficesAsync(string query);
        Task<Office> GetOfficeAsync(string id);
        Task AddOfficeAsync(Office office);
        Task UpdateOfficeAsync(string id, Office office);
        Task DeleteOfficeAsync(string id);
    }
}
