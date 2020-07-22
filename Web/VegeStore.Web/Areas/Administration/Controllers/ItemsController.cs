namespace VegeStore.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VegeStore.Services.Data;
    using VegeStore.Web.ViewModels.Administration.Items;

    public class ItemsController : AdministrationController
    {
        private readonly IItemsService itemsService;

        public ItemsController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        public IActionResult Items()
        {
            var viewModel = new AdminItemsViewModel
            {
                Items = this.itemsService.GetAllItems<AdminItemViewModel>(),
            };

            return this.View(viewModel); // TODO: Create Item functionality
        }
    }
}
