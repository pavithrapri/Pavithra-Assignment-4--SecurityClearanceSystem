using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorSecurityClearance.Models;

namespace VisitorSecurityClearance.Services
{
    public interface IManagerService
    {
        Task<IEnumerable<Manager>> GetManagersAsync(string query);
        Task<Manager> GetManagerAsync(string id);
        Task AddManagerAsync(Manager manager);
        Task UpdateManagerAsync(string id, Manager manager);
        Task DeleteManagerAsync(string id);
    }
}
