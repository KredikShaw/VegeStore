namespace VegeStore.Web.ViewModels.Administration.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VegeStore.Data.Models;
    using VegeStore.Services.Mapping;

    public class AdminUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public virtual string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public int OrdersCount { get; set; }
    }
}
