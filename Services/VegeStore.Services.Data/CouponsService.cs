namespace VegeStore.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using VegeStore.Data.Common.Repositories;
    using VegeStore.Data.Models;

    public class CouponsService : ICouponsService
    {
        private readonly IRepository<Coupon> couponsRepository;

        public CouponsService(IRepository<Coupon> couponsRepository)
        {
            this.couponsRepository = couponsRepository;
        }

        public async Task DeleteCouponAsync(Coupon coupon)
        {
            this.couponsRepository.Delete(coupon);
            await this.couponsRepository.SaveChangesAsync();
        }

        public Coupon GetCoupon(string code)
        {
            var coupon = this.couponsRepository.All()
                .FirstOrDefault(c => c.Code == code);

            return coupon;
        }
    }
}
