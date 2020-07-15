namespace VegeStore.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using VegeStore.Services.Data;
    using VegeStore.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly IOrdersService ordersService;

        public DashboardController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Orders()
        {
            var viewModel = new AdminOrdersViewModel
            {
                Orders = await this.ordersService.GetOrdersAsync<AdminOrderViewModel>(),
            };
            return this.View(viewModel); // TODO: Mark orders as complete
        }
    }
}
