namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using VegeStore.Data.Common.Repositories;
    using VegeStore.Data.Models;

    public class OrderItemsService : IOrderItemsService
    {
        private readonly IRepository<OrderItem> orderItemsRepository;
        private readonly IItemsService itemsService;

        public OrderItemsService(IRepository<OrderItem> orderItemsRepository, IItemsService itemsService)
        {
            this.orderItemsRepository = orderItemsRepository;
            this.itemsService = itemsService;
        }

        public async Task CreateOrderItemAsync(string orderId, int itemId, int amount)
        {
            if (this.itemsService.CheckAvailability(itemId, amount))
            {
                var orderItem = new OrderItem
                {
                    OrderId = orderId,
                    ItemId = itemId,
                    Amount = amount,
                };

                await this.orderItemsRepository.AddAsync(orderItem);
                await this.orderItemsRepository.SaveChangesAsync();
            }
        }
    }
}
