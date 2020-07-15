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
    using VegeStore.Web.ViewModels.Checkout;
    using VegeStore.Web.ViewModels.Shop;

    public class StoreController : Controller
    {
        private readonly IItemsService itemsService;
        private readonly ICartItemsService cartItemsService;
        private readonly ICartsService cartsService;
        private readonly IOrdersService ordersService;
        private readonly IOrderItemsService orderItemsService;
        private readonly UserManager<ApplicationUser> userManager;

        public StoreController(
            IItemsService itemsService,
            ICartItemsService cartItemsService,
            ICartsService cartsService,
            IOrdersService ordersService,
            IOrderItemsService orderItemsService,
            UserManager<ApplicationUser> userManager)
        {
            this.itemsService = itemsService;
            this.cartItemsService = cartItemsService;
            this.cartsService = cartsService;
            this.ordersService = ordersService;
            this.orderItemsService = orderItemsService;
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
                TotalCost = await this.cartsService.CalculateTotalCostAsync(cartId),
                Discount = this.cartsService.CalculateDiscount(cartId),
            };

            foreach (var item in viewModel.Items)
            {
                item.Amount = this.cartItemsService.GetAmount(cartId, item.Id);
            }

            return this.View(viewModel);
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
        [HttpPost]
        public async Task<IActionResult> AddToCart(int itemId, int amount) // TODO: fix adding to cart from shop page
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.cartItemsService.CreateCartItemAsync(userId, itemId, amount);
            return this.RedirectToAction("Shop");
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.cartItemsService.RemoveCartItemAsync(userId, itemId);
            return this.RedirectToAction("Cart");
        }

        [Authorize]
        public async Task<IActionResult> ChangeAmount(int amount, int itemId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var cartId = user.CartId;
            await this.cartItemsService.ChangeAmountAsync(cartId, itemId, amount);
            return this.RedirectToAction("Cart");
        }

        [Authorize]
        public async Task<IActionResult> ApplyCoupon(string code)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var cartId = user.CartId;
            await this.cartsService.ApplyCouponAsync(code, cartId);
            return this.RedirectToAction("Cart");
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var cartId = user.CartId;

            var viewModel = new CheckoutViewModel
            {
                TotalCost = await this.cartsService.CalculateTotalCostAsync(cartId),
                Discount = this.cartsService.CalculateDiscount(cartId),
                ItemsCount = this.cartsService.GetItemsCount(cartId),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInputModel inputModel)
        {
            var name = inputModel.FirstName + " " + inputModel.LastName;
            var address = inputModel.Neighbourhood + ", " + inputModel.Street + ", " + inputModel.Zip + ", " + inputModel.Appartment;
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var cartId = user.CartId;
            var price = this.cartsService.GetCart(cartId).TotalCost;

            var orderId = await this.ordersService.CreateOrder(name, address, price, inputModel.Phone, userId);

            var cartItems = this.cartItemsService.GetAllCartItems(cartId);

            foreach (var cartItem in cartItems)
            {
                await this.orderItemsService.CreateOrderItemAsync(orderId, cartItem.ItemId, cartItem.Amount);
                await this.itemsService.DecreaseAvailability(cartItem.ItemId, cartItem.Amount);
            }

            await this.cartItemsService.EmptyCart(cartId);
            await this.cartsService.RemoveDiscount(cartId);

            return this.RedirectToAction("ThankYou");
        }

        [Authorize]
        public IActionResult ThankYou() // TODO: Add administration
        {// TODO: fix footer info, fix homepage info
            return this.View(); // TODO: Add pagination where necessary
        }// TODO: Refactor code (put everything in a new controller/service, currently breaking S from SOLID)
    }
}
