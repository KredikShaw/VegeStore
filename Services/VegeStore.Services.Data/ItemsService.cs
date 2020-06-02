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

        public async Task DecreaseAvailability(int itemId, int amount)
        {
            var item = this.itemsRepository.All().FirstOrDefault(i => i.Id == itemId);
            item.Available -= amount;
            this.itemsRepository.Update(item);
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
    }
}
