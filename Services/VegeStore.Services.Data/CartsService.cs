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

        public CartsService(IDeletableEntityRepository<Cart> cartsRepository)
        {
            this.cartsRepository = cartsRepository;
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
