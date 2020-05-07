namespace VegeStore.Web.ViewModels.Shop
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VegeStore.Data.Models;
    using VegeStore.Services.Mapping;

    public class ShopItemViewModel : IMapFrom<Item>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ThumbnailUrl { get; set; }
    }
}
