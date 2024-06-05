using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorSecurityClearance.Models;

namespace VisitorSecurityClearance.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly ICosmosDbService<Visitor> _cosmosDbService;
        public VisitorService(ICosmosDbService<Visitor> cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<Visitor>> GetVisitorsAsync(string query)
        {
            return await _cosmosDbService.GetItemsAsync(query);
        }

        public async Task<Visitor> GetVisitorAsync(string id)
        {
            return await _cosmosDbService.GetItemAsync(id);
        }

        public async Task AddVisitorAsync(Visitor visitor)
        {
            await _cosmosDbService.AddItemAsync(visitor);
        }

        public async Task UpdateVisitorAsync(string id, Visitor visitor)
        {
            await _cosmosDbService.UpdateItemAsync(id, visitor);
        }

        public async Task DeleteVisitorAsync(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
        }
    }
}
