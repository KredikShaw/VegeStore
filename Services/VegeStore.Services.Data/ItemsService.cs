namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using VegeStore.Data.Common.Repositories;
    using VegeStore.Data.Models;
    using VegeStore.Services.Mapping;

    public class ItemsService : IItemsService
    {
        private readonly IDeletableEntityRepository<Item> itemsRepository;

        public ItemsService(IDeletableEntityRepository<Item> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        public bool CheckAvailability(int itemId, int amount)
        {
            var item = this.itemsRepository.All().FirstOrDefault(i => i.Id == itemId);
            return amount <= item.Available;
        }

        public async Task CreateItem(string name, string type, string description, decimal price, double available, string thumbnailUrl)
        {
            var item = new Item
            {
                Name = name,
                Type = type,
                Description = description,
                Price = price,
                Available = available,
                ThumbnailUrl = thumbnailUrl,
            };

            await this.itemsRepository.AddAsync(item);
            await this.itemsRepository.SaveChangesAsync();
        }

        public async Task DecreaseAvailability(int itemId, int amount)
        {
            var item = this.itemsRepository.All().FirstOrDefault(i => i.Id == itemId);
            item.Available -= amount;
            item.Sold += amount;
            this.itemsRepository.Update(item);
            await this.itemsRepository.SaveChangesAsync();
        }

        public async Task DeleteItem(int id)
        {
            var item = this.itemsRepository.All()
                .FirstOrDefault(x => x.Id == id);

            this.itemsRepository.Delete(item);
            await this.itemsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllCartItems<T>(IEnumerable<int> ids)
        {
            var items = this.itemsRepository
                .All()
                .Where(i => ids.Contains(i.Id))
                .To<T>()
                .ToList();

            return items;
        }

        public IEnumerable<T> GetAllItems<T>()
        {
            var items = this.itemsRepository
                .All()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToList();

            return items;
        }

        public Item GetItem(int id)
        {
            var item = this.itemsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            return item;
        }

        public T GetItem<T>(int id)
        {
            var item = this.itemsRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return item;
        }

        public async Task UpdateItem(int id, string name, string type, string description, decimal price, double available, string thumbnailUrl)
        {
            var item = this.itemsRepository.All()
                .FirstOrDefault(x => x.Id == id);

            item.Name = name;
            item.Type = type;
            item.Description = description;
            item.Price = price;
            item.Available = available;

            if (thumbnailUrl != null)
            {
                item.ThumbnailUrl = thumbnailUrl;
            }

            this.itemsRepository.Update(item);
            await this.itemsRepository.SaveChangesAsync();
        }
    }
}
