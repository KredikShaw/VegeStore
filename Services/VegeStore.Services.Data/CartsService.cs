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

    public class CartsService : ICartsService
    {
        private readonly IDeletableEntityRepository<Cart> cartsRepository;
        private readonly ICartItemsService cartItemsService;
        private readonly IItemsService itemsService;
        private readonly ICouponsService couponsService;

        public CartsService(
            IDeletableEntityRepository<Cart> cartsRepository,
            ICartItemsService cartItemsService,
            IItemsService itemsService,
            ICouponsService couponsService)
        {
            this.cartsRepository = cartsRepository;
            this.cartItemsService = cartItemsService;
            this.itemsService = itemsService;
            this.couponsService = couponsService;
        }

        public async Task ApplyCouponAsync(string code, string cartId)
        {
            var coupon = this.couponsService.GetCoupon(code);
            if (coupon != null && coupon.ExpirationDate.Ticks > DateTime.UtcNow.Ticks)
            {
                var cart = this.cartsRepository.All().FirstOrDefault(c => c.Id == cartId);
                var discount = cart.TotalCost * (coupon.DicountAmount / 100);
                cart.Discount = coupon.DicountAmount;
                this.cartsRepository.Update(cart);
                await this.cartsRepository.SaveChangesAsync();

                await this.couponsService.DeleteCouponAsync(coupon);
            }
        }

        public async Task<decimal> CalculateTotalCostAsync(string cartId)
        {
            var cartItems = this.cartItemsService.GetAllCartItems(cartId);
            foreach (var cartItem in cartItems)
            {
                cartItem.Item = this.itemsService.GetItem(cartItem.ItemId);
            }

            var totalCost = cartItems.Sum(ci => ci.Item.Price * ci.Amount);
            if (totalCost < 50 && cartItems.Count() > 0)
            {
                totalCost += 5;
            }

            var cart = this.cartsRepository.All().FirstOrDefault(c => c.Id == cartId);
            cart.TotalCost = totalCost;
            this.cartsRepository.Update(cart);
            await this.cartsRepository.SaveChangesAsync();

            return totalCost;
        }

        public async Task<string> CreateCartAsync(string userId)
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

        public decimal CalculateDiscount(string cartId)
        {
            var cart = this.cartsRepository.All()
                .FirstOrDefault(c => c.Id == cartId);

            var discount = cart.TotalCost * (cart.Discount / 100);
            return discount;
        }

        public async Task RemoveDiscountAsync(string cartId)
        {
            var cart = await this.cartsRepository.All().FirstOrDefaultAsync(c => c.Id == cartId);
            cart.Discount = 0;
            this.cartsRepository.Update(cart);
            await this.cartsRepository.SaveChangesAsync();
        }

        public int GetItemsCount(string cartId)
        {
            var cart = this.cartsRepository.All()
                .FirstOrDefault(c => c.Id == cartId);

            return cart.Items.Count();
        }
    }
}
