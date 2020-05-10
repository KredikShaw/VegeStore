namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using VegeStore.Data.Common.Repositories;
    using VegeStore.Data.Models;

    public class CartItemsService : ICartItemsService
    {
        private readonly IRepository<CartItem> cartItemsRepository;
        private readonly IUsersService usersService;

        public CartItemsService(
            IRepository<CartItem> cartItemsRepository,
            IUsersService usersService)
        {
            this.cartItemsRepository = cartItemsRepository;
            this.usersService = usersService;
        }

        public async Task CreateCartItemAsync(string userId, int itemId)
        {
            var cartId = this.usersService.GetCartId(userId);

            var cartItem = new CartItem
            {
                CartId = cartId,
                ItemId = itemId,
            };

            await this.cartItemsRepository.AddAsync(cartItem);
            await this.cartItemsRepository.SaveChangesAsync();
        }
    }
}
