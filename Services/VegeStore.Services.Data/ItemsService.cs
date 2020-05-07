namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
