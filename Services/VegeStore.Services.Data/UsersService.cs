namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using VegeStore.Data.Common.Repositories;
    using VegeStore.Data.Models;
    using VegeStore.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task BanUser(string userId)
        {
            var user = this.usersRepository.All()
                .FirstOrDefault(x => x.Id == userId);

            this.usersRepository.Delete(user);
            await this.usersRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetBannedUsers<T>()
        {
            var users = this.usersRepository
                .AllWithDeleted()
                .Where(x => x.IsDeleted == true)
                .To<T>()
                .ToList();

            return users;
        }

        public string GetCartId(string userId)
        {
            var user = this.usersRepository
                .All()
                .FirstOrDefault(u => u.Id == userId);

            var cartId = user.CartId;

            return cartId;
        }

        public IEnumerable<T> GetUsers<T>()
        {
            var users = this.usersRepository
                .All()
                .To<T>()
                .ToList();

            return users;
        }

        public async Task UnbanUser(string userId)
        {
            var user = this.usersRepository
                .AllWithDeleted()
                .FirstOrDefault(x => x.Id == userId);

            this.usersRepository.Undelete(user);
            await this.usersRepository.SaveChangesAsync();
        }
    }
}
