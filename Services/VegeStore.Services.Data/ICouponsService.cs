namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using VegeStore.Data.Models;

    public interface ICouponsService
    {
        Coupon GetCoupon(string code);

        Task DeleteCouponAsync(Coupon coupon);
    }
}
