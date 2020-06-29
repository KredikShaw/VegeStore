namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using VegeStore.Data.Models;

    public interface ICartsService
    {
        Cart GetCart(string cartId);

        Task<string> CreateCart(string userId);

        Task<decimal> CalculateTotalCostAsync(string cartId);

        Task ApplyCouponAsync(string code, string cartId);

        decimal CalculateDiscount(string cartId);

        Task RemoveDiscount(string cartId);

        int GetItemsCount(string cartId);
    }
}
