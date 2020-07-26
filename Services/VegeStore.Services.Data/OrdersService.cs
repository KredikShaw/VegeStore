namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VegeStore.Data.Common.Repositories;
    using VegeStore.Data.Models;
    using VegeStore.Services.Mapping;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public OrdersService(IDeletableEntityRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<string> CreateOrderAsync(string name, string address, decimal price, string phone, string userId)
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

        public async Task<IEnumerable<T>> GetCompletedOrdersAsync<T>()
        {
            var orders = await this.ordersRepository
                .AllWithDeleted()
                .Where(x => x.IsDeleted == true)
                .To<T>()
                .ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<T>> GetOrdersAsync<T>()
        {
            var orders = await this.ordersRepository
                .All()
                .To<T>()
                .ToListAsync();

            return orders;
        }

        public async Task MarkOrderAsCompletedAsync(string orderId)
        {
            var order = await this.ordersRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == orderId);

            this.ordersRepository.Delete(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public async Task MarkOrderAsUncompletedAsync(string orderId)
        {
            var order = await this.ordersRepository
                   .AllWithDeleted()
                   .FirstOrDefaultAsync(x => x.Id == orderId);

            this.ordersRepository.Undelete(order);
            await this.ordersRepository.SaveChangesAsync();
        }
    }
}
