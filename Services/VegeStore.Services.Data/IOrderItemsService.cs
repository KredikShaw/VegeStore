namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOrderItemsService
    {
        Task CreateOrderItemAsync(string orderId, int itemId, int amount);
    }
}
