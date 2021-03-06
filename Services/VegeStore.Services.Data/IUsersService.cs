﻿namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        string GetCartId(string userId);

        IEnumerable<T> GetUsers<T>();

        Task BanUserAsync(string userId);

        IEnumerable<T> GetBannedUsers<T>();

        Task UnbanUserAsync(string userId);
    }
}
