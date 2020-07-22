namespace VegeStore.Web.ViewModels.Administration.Items
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VegeStore.Data.Models;
    using VegeStore.Services.Mapping;

    public class AdminItemViewModel : IMapFrom<Item>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Sold { get; set; }

        public double Available { get; set; }

        public string ThumbnailUrl { get; set; }

        public int CartsCount { get; set; }
    }
}
