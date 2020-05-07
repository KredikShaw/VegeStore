namespace VegeStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class StoreController : Controller
    {
        public IActionResult Shop()
        {
            return this.View();
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
    }
}
