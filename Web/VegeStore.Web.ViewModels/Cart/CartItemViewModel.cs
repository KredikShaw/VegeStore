namespace VegeStore.Web.ViewModels.Cart
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VegeStore.Data.Models;
    using VegeStore.Services.Mapping;

    public class CartItemViewModel : IMapFrom<Item>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ThumbnailUrl { get; set; }

        public int Amount { get; set; }
    }
}
