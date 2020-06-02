namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using VegeStore.Data.Common.Repositories;
    using VegeStore.Data.Models;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public OrdersService(IDeletableEntityRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<string> CreateOrder(string name, string address, decimal price, string phone, string userId)
        {
            var order = new Order
            {
                FullName = name,
                Address = address,
                Price = price,
                Phone = phone,
                UserId = userId,
            };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();

            return order.Id;
        }
    }
}
