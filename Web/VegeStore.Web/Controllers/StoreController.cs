namespace VegeStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VegeStore.Services.Data;
    using VegeStore.Web.ViewModels.Shop;

    public class StoreController : Controller
    {
        private readonly IItemsService itemsService;

        public StoreController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
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
    }
}
