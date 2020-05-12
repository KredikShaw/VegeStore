namespace VegeStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using VegeStore.Data.Models;
    using VegeStore.Services.Data;
    using VegeStore.Web.ViewModels.Cart;
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

        [Authorize]
        public IActionResult Shop()
        {
            var viewModel = new ShopItemsViewModel
            {
                Items = this.itemsService.GetAllItems<ShopItemViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Wishlist()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> Cart()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var cartId = user.CartId;
            var itemIds = this.cartItemsService.GetItemIds(cartId);
            var viewModel = new CartItemsViewModel
            {
                Items = this.itemsService.GetAllCartItems<CartItemViewModel>(itemIds),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Item(int id)
        {
            var viewModel = new ItemViewModel
            {
                Item = this.itemsService.GetItem(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int itemId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.cartItemsService.CreateCartItemAsync(userId, itemId);
            return this.RedirectToAction("Shop");
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.cartItemsService.RemoveCartItemAsync(userId, itemId);
            return this.RedirectToAction("Cart");
        }
    }
}
