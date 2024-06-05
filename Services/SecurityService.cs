using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorSecurityClearance.Models;

namespace VisitorSecurityClearance.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ICosmosDbService<Security> _cosmosDbService;

        public SecurityService(ICosmosDbService<Security> cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<Security>> GetSecuritiesAsync(string query)
        {
            return await _cosmosDbService.GetItemsAsync(query);
        }

        public async Task<Security?> GetSecurityAsync(string id)
        {
            return await _cosmosDbService.GetItemAsync(id);
        }

        public async Task AddSecurityAsync(Security security)
        {
            await _cosmosDbService.AddItemAsync(security);
        }

        public async Task UpdateSecurityAsync(string id, Security security)
        {
            await _cosmosDbService.UpdateItemAsync(id, security);
        }

        public async Task DeleteSecurityAsync(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
        }
    }
}
