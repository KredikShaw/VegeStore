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
        private readonly IUploadService uploadService;

        public ItemsController(IItemsService itemsService, IUploadService uploadService)
        {
            this.itemsService = itemsService;
            this.uploadService = uploadService;
        }

        public IActionResult Items()
        {
            var viewModel = new AdminItemsViewModel
            {
                Items = this.itemsService.GetAllItems<AdminItemViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult CreateItem()
        {
            var viewModel = new ItemsInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemsInputModel inputModel)
        {
            var url = this.uploadService.UploadImageToCloudinary(inputModel.Thumbnail.OpenReadStream());

            await this.itemsService.CreateItemAsync(inputModel.Name, inputModel.Type, inputModel.Description, inputModel.Price, inputModel.Available, url);

            return this.RedirectToAction("Items");
        }

        public IActionResult EditItem(int itemId)
        {
            var viewModel = this.itemsService.GetItem<ItemsInputModel>(itemId);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(ItemsInputModel inputModel)
        {
            string thumbnailUrl = null;

            if (inputModel.Thumbnail != null)
            {
                thumbnailUrl = this.uploadService.UploadImageToCloudinary(inputModel.Thumbnail.OpenReadStream());
            }

            await this.itemsService.UpdateItemAsync(inputModel.Id, inputModel.Name, inputModel.Type, inputModel.Description, inputModel.Price, inputModel.Available, thumbnailUrl);

            return this.RedirectToAction("Items");
        }

        public async Task<IActionResult> DeleteItem(int itemId)
        {
            await this.itemsService.DeleteItemAsync(itemId);

            return this.RedirectToAction("Items"); // TODO: Add modal popup to confirm delete
        }

        public async Task<IActionResult> DeletedItems()
        {
            var viewModel = new AdminItemsViewModel
            {
                Items = await this.itemsService.GetDeletedItemsAsync<AdminItemViewModel>(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> UndeleteItem(int itemId)
        {
            await this.itemsService.UndeleteItemAsync(itemId);

            return this.RedirectToAction("DeletedItems");
        }
    }
}
