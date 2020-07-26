namespace VegeStore.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VegeStore.Services.Data;
    using VegeStore.Web.ViewModels.Administration.Orders;

    public class OrdersController : AdministrationController
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public async Task<IActionResult> Orders()
        {
            var viewModel = new AdminOrdersViewModel
            {
                Orders = await this.ordersService.GetOrdersAsync<AdminOrderViewModel>(),
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> CompleteOrder(string orderId)
        {
            await this.ordersService.MarkOrderAsCompletedAsync(orderId);
            return this.RedirectToAction("Orders");
        }

        public async Task<IActionResult> CompletedOrders()
        {
            var viewModel = new AdminOrdersViewModel
            {
                Orders = await this.ordersService.GetCompletedOrdersAsync<AdminOrderViewModel>(),
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> UncompleteOrder(string orderId)
        {
            await this.ordersService.MarkOrderAsUncompletedAsync(orderId);
            return this.RedirectToAction("CompletedOrders");
        }
    }
}
