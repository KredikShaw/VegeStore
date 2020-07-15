namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<string> CreateOrder(string name, string address, decimal price, string phone, string userId);

        Task<IEnumerable<T>> GetOrdersAsync<T>();
    }
}
