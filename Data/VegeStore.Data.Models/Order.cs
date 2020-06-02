namespace VegeStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VegeStore.Data.Common.Models;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string FullName { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public string Phone { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
