using System.Threading.Tasks;

namespace VisitorSecurityClearance.Services
{
    public interface ICosmosDbService<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(string query);

        Task AddItemAsync(T item);
        Task<T> GetItemAsync(string id);
        Task UpdateItemAsync(string id, T item);
        Task DeleteItemAsync(string id);
    }
}
