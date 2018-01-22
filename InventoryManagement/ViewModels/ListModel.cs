using PagedList;
using PagedList.Mvc;
using System.Collections.Generic;

namespace InventoryManagement.ViewModels
{
    public class ListModel<T>
    {
        public IList<T> Items { get; set; }
    }
}