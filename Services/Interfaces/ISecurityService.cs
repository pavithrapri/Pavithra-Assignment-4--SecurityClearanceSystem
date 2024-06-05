using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorSecurityClearance.Models;

namespace VisitorSecurityClearance.Services
{
    public interface ISecurityService
    {
        Task<IEnumerable<Security>> GetSecuritiesAsync(string query);
        Task<Security> GetSecurityAsync(string id);
        Task AddSecurityAsync(Security security);
        Task UpdateSecurityAsync(string id, Security security);
        Task DeleteSecurityAsync(string id);
    }
}
