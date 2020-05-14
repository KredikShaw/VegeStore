namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICartItemsService
    {
        Task CreateCartItemAsync(string userId, int itemId, int amount);

        IEnumerable<int> GetItemIds(string cartId);

        int GetItemsCount(ClaimsPrincipal user);

        Task RemoveCartItemAsync(string userId, int itemId);

        int GetAmount(string cartId, int itemId);

        Task ChangeAmountAsync(string cartId, int itemId, int amount);
    }
}
