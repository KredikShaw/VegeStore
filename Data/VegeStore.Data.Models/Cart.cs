namespace VegeStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VegeStore.Data.Common.Models;

    public class Cart : BaseDeletableModel<string>
    {
        public Cart()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Items = new HashSet<CartItem>();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CartItem> Items { get; set; }

        public decimal TotalCost { get; set; }

        public bool CouponApplied { get; set; }
    }
}
