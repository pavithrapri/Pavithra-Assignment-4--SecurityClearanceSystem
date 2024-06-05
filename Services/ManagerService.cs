using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorSecurityClearance.Models;

namespace VisitorSecurityClearance.Services
{
    public class ManagerService : IManagerService
    {
        private readonly ICosmosDbService<Manager> _cosmosDbService;

        public ManagerService(ICosmosDbService<Manager> cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<Manager>> GetManagersAsync(string query)
        {
            return await _cosmosDbService.GetItemsAsync(query);
        }

        public async Task<Manager> GetManagerAsync(string id)
        {
            return await _cosmosDbService.GetItemAsync(id);
        }

        public async Task AddManagerAsync(Manager manager)
        {
            await _cosmosDbService.AddItemAsync(manager);
        }

        public async Task UpdateManagerAsync(string id, Manager manager)
        {
            await _cosmosDbService.UpdateItemAsync(id, manager);
        }

        public async Task DeleteManagerAsync(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
        }
    }
}
