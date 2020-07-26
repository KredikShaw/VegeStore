namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using VegeStore.Data.Models;

    public interface IItemsService
    {
        IEnumerable<T> GetAllItems<T>();

        Item GetItem(int id);

        T GetItem<T>(int id);

        IEnumerable<T> GetAllCartItems<T>(IEnumerable<int> ids);

        bool CheckAvailability(int itemId, int amount);

        Task DecreaseAvailabilityAsync(int itemId, int amount);

        Task CreateItemAsync(string name, string type, string description, decimal price, double available, string thumbnailUrl);

        Task UpdateItemAsync(int id, string name, string type, string description, decimal price, double available, string thumbnailUrl);

        Task DeleteItemAsync(int id);

        Task<IEnumerable<T>> GetDeletedItemsAsync<T>();

        Task UndeleteItemAsync(int id);
    }
}
