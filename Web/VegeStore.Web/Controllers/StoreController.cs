namespace VegeStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using VegeStore.Data.Models;
    using VegeStore.Services.Data;
    using VegeStore.Web.ViewModels.Shop;

    public class StoreController : Controller
    {
        private readonly IItemsService itemsService;
        private readonly ICartItemsService cartItemsService;
        private readonly UserManager<ApplicationUser> userManager;

        public StoreController(
            IItemsService itemsService,
            ICartItemsService cartItemsService,
            UserManager<ApplicationUser> userManager)
        {
            this.itemsService = itemsService;
            this.cartItemsService = cartItemsService;
            this.userManager = userManager;
        }

        public IActionResult Shop()
        {
            var viewModel = new ShopItemsViewModel
            {
                Items = this.itemsService.GetAllItems<ShopItemViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Wishlist()
        {
            return this.View();
        }

        public IActionResult Cart()
        {
            return this.View();
        }

        public IActionResult Checkout()
        {
            return this.View();
        }

        public IActionResult Item(int id)
        {
            var viewModel = new ItemViewModel
            {
                Item = this.itemsService.GetItem(id),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> AddToCart(int itemId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.cartItemsService.CreateCartItemAsync(userId, itemId);
            return this.RedirectToAction("Shop");
        }
    }
}
