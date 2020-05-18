namespace VegeStore.Web.ViewModels.Cart
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VegeStore.Data.Models;
    using VegeStore.Services.Mapping;

    public class CartItemsViewModel
    {
        public IEnumerable<CartItemViewModel> Items { get; set; }

        public decimal TotalCost { get; set; }

        public decimal Discount { get; set; }
    }
}
