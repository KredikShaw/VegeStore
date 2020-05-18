namespace VegeStore.Data.Models
{
    using System;

    public class Coupon
    {
        public Coupon()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ExpirationDate = DateTime.UtcNow.AddDays(14);
        }

        public string Id { get; set; }

        public string Code { get; set; }

        public decimal DicountAmount { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
