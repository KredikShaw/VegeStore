namespace VegeStore.Web.ViewModels.Checkout
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CheckoutViewModel
    {
        public decimal TotalCost { get; set; }

        public decimal Discount { get; set; }

        public CheckoutInputModel InputModel { get; set; }
    }
}
