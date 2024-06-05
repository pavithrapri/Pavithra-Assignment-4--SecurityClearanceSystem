using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorSecurityClearance.Models;

namespace VisitorSecurityClearance.Services
{
    public interface IVisitorService
    {
        Task<IEnumerable<Visitor>> GetVisitorsAsync(string query);
        Task<Visitor> GetVisitorAsync(string id);
        Task AddVisitorAsync(Visitor visitor);
        Task UpdateVisitorAsync(string id, Visitor visitor);
        Task DeleteVisitorAsync(string id);
    }
}
