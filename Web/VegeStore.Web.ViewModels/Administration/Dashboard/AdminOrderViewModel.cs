namespace VegeStore.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VegeStore.Data.Models;
    using VegeStore.Services.Mapping;

    public class AdminOrderViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public string Phone { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
