namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<string> CreateOrderAsync(string name, string address, decimal price, string phone, string userId);

        Task<IEnumerable<T>> GetOrdersAsync<T>();

        Task MarkOrderAsCompletedAsync(string orderId);

        Task MarkOrderAsUncompletedAsync(string orderId);

        Task<IEnumerable<T>> GetCompletedOrdersAsync<T>();
    }
}
