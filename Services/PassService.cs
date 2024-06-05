using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorSecurityClearance.Models;

namespace VisitorSecurityClearance.Services
{
    public class PassService : IPassService
    {
        private readonly ICosmosDbService<Pass> _cosmosDbService;

        public PassService(ICosmosDbService<Pass> cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<Pass>> GetPassesAsync(string query)
        {
            return await _cosmosDbService.GetItemsAsync(query);
        }

        public async Task<Pass> GetPassAsync(string id)
        {
            return await _cosmosDbService.GetItemAsync(id);
        }

        public async Task AddPassAsync(Pass pass)
        {
            await _cosmosDbService.AddItemAsync(pass);
        }

        public async Task UpdatePassAsync(string id, Pass pass)
        {
            await _cosmosDbService.UpdateItemAsync(id, pass);
        }

        public async Task DeletePassAsync(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
        }

        internal byte[] GeneratePassPdf(Visitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
