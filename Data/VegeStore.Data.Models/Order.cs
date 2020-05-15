namespace VegeStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VegeStore.Data.Common.Models;

    public class Order : BaseDeletableModel<string>
    {
        public string Address { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
