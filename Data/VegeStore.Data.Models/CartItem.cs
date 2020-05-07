namespace VegeStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VegeStore.Data.Common.Models;

    public class CartItem
    {
        public string CartId { get; set; }

        public virtual Cart Cart { get; set; }

        public int ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
