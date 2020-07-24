namespace VegeStore.Web.ViewModels.Administration.Items
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using VegeStore.Data.Models;
    using VegeStore.Services.Mapping;

    public class ItemsInputModel : IMapFrom<Item>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public double Available { get; set; }

        public IFormFile Thumbnail { get; set; }
    }
}
