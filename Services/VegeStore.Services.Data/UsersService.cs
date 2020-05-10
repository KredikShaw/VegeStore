namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using VegeStore.Data.Common.Repositories;
    using VegeStore.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public string GetCartId(string userId)
        {
            var user = this.usersRepository
                .All()
                .FirstOrDefault(u => u.Id == userId);

            var cartId = user.CartId;

            return cartId;
        }
    }
}
