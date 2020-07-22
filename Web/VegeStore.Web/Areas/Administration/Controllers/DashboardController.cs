namespace VegeStore.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        } // TODO: Admin Items, take orders into a different controller and use different ones for items and users (leave this only with index method)
    }
}
