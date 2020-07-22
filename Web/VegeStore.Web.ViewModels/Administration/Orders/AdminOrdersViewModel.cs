namespace VegeStore.Web.ViewModels.Administration.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AdminOrdersViewModel
    {
        public IEnumerable<AdminOrderViewModel> Orders { get; set; }
    }
}
