namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VegeStore.Data.Models;

    public interface IItemsService
    {
        IEnumerable<T> GetAllItems<T>();

        Item GetItem(int id);
    }
}
