namespace VegeStore.Web.ViewModels.Cart
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CartItemsViewModel
    {
        public IEnumerable<CartItemViewModel> Items { get; set; }
    }
}
