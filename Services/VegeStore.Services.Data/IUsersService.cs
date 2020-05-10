namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUsersService
    {
        string GetCartId(string userId);
    }
}
