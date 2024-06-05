using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorSecurityClearance.Models;

namespace VisitorSecurityClearance.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly ICosmosDbService<Office> _cosmosDbService;

        public OfficeService(ICosmosDbService<Office> cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<Office>> GetOfficesAsync(string query)
        {
            return await _cosmosDbService.GetItemsAsync(query);
        }

        public async Task<Office> GetOfficeAsync(string id)
        {
            return await _cosmosDbService.GetItemAsync(id);
        }

        public async Task AddOfficeAsync(Office office)
        {
            await _cosmosDbService.AddItemAsync(office);
        }

        public async Task UpdateOfficeAsync(string id, Office office)
        {
            await _cosmosDbService.UpdateItemAsync(id, office);
        }

        public async Task DeleteOfficeAsync(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
        }
    }
}
