namespace VegeStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VegeStore.Data.Common.Models;

    public class Item : BaseDeletableModel<int>
    {
        public Item()
        {
            this.Carts = new HashSet<CartItem>();
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public double Rating { get; set; }

        public int RatedAmount { get; set; }

        public int Sold { get; set; }

        public double Available { get; set; }

        public string ThumbnailUrl { get; set; }

        public virtual ICollection<CartItem> Carts { get; set; }
    }
}
