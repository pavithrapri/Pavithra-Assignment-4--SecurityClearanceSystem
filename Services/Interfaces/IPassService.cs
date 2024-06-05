using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorSecurityClearance.Models;

namespace VisitorSecurityClearance.Services
{
    public interface IPassService
    {
        Task<IEnumerable<Pass>> GetPassesAsync(string query);
        Task<Pass> GetPassAsync(string id);
        Task AddPassAsync(Pass pass);
        Task UpdatePassAsync(string id, Pass pass);
        Task DeletePassAsync(string id);
    }
}
