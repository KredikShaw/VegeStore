namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICartItemsService
    {
        Task CreateCartItemAsync(string userId, int itemId);
    }
}
