namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using VegeStore.Data.Common.Repositories;
    using VegeStore.Data.Models;

    public class CartsService : ICartsService
    {
        private readonly IDeletableEntityRepository<Cart> cartsRepository;
        private readonly ICartItemsService cartItemsService;
        private readonly IItemsService itemsService;

        public CartsService(
            IDeletableEntityRepository<Cart> cartsRepository,
            ICartItemsService cartItemsService,
            IItemsService itemsService)
        {
            this.cartsRepository = cartsRepository;
            this.cartItemsService = cartItemsService;
            this.itemsService = itemsService;
        }

        public async Task<decimal> CalculateTotalCostAsync(string cartId)
        {
            var cartItems = this.cartItemsService.GetAllCartItems(cartId);
            foreach (var cartItem in cartItems)
            {
                cartItem.Item = this.itemsService.GetItem(cartItem.ItemId);
            }

            var totalCost = cartItems.Sum(ci => ci.Item.Price * ci.Amount);
            if (totalCost < 50)
            {
                totalCost += 5;
            }

            var cart = this.cartsRepository.All().FirstOrDefault(c => c.Id == cartId);
            cart.TotalCost = totalCost;
            this.cartsRepository.Update(cart);
            await this.cartsRepository.SaveChangesAsync();

            return totalCost;
        }

        public async Task<string> CreateCart(string userId)
        {
            var cart = new Cart
            {
                UserId = userId,
            };

            await this.cartsRepository.AddAsync(cart);
            return cart.Id;
        }

        public Cart GetCart(string cartId)
        {
            var cart = this.cartsRepository
                .All()
                .FirstOrDefault(c => c.Id == cartId);

            return cart;
        }
    }
}
