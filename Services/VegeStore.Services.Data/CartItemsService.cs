namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using VegeStore.Data.Common.Repositories;
    using VegeStore.Data.Models;

    public class CartItemsService : ICartItemsService
    {
        private readonly IRepository<CartItem> cartItemsRepository;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartItemsService(
            IRepository<CartItem> cartItemsRepository,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.cartItemsRepository = cartItemsRepository;
            this.usersService = usersService;
            this.userManager = userManager;
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

        public IEnumerable<int> GetItemIds(string cartId)
        {
            var ids = this.cartItemsRepository
                .All()
                .Where(ci => ci.CartId == cartId)
                .Select(ci => ci.ItemId)
                .ToList();

            return ids;
        }

        public int GetItemsCount(ClaimsPrincipal user)
        {
            var userId = this.userManager.GetUserId(user);
            var cartId = this.usersService.GetCartId(userId);
            var count = this.cartItemsRepository
                .All()
                .Where(ci => ci.CartId == cartId)
                .Select(ci => ci.ItemId)
                .Count();

            return count;
        }

        public async Task RemoveCartItemAsync(string userId, int itemId)
        {
            var cartId = this.usersService.GetCartId(userId);

            var cartItem = this.cartItemsRepository
                .All()
                .FirstOrDefault(ci => ci.CartId == cartId && ci.ItemId == itemId);

            this.cartItemsRepository.Delete(cartItem);
            await this.cartItemsRepository.SaveChangesAsync();
        }
    }
}
