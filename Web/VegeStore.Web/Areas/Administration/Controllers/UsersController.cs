namespace VegeStore.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VegeStore.Services.Data;
    using VegeStore.Web.ViewModels.Administration.Users;

    public class UsersController : AdministrationController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Users()
        {
            var viewModel = new AdminUsersViewModel
            {
                Users = this.usersService.GetUsers<AdminUserViewModel>(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> BanUser(string userId)
        {
            await this.usersService.BanUser(userId);

            return this.RedirectToAction("Users");
        }

        public IActionResult BannedUsers()
        {
            var viewModel = new AdminUsersViewModel
            {
                Users = this.usersService.GetBannedUsers<AdminUserViewModel>(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> UnbanUser(string userId)
        {
            await this.usersService.UnbanUser(userId);

            return this.RedirectToAction("BannedUsers"); // TODO: Commit
        }
    }
}
