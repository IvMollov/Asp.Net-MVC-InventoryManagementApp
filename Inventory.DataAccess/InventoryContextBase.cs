using System;
using Inventory.DataAccess.Entities;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Inventory.DataAccess
{
    public class InventoryContextBase : DbContext
    {
        public InventoryContextBase()
            : base()
        {
        }


        public ObjectContext ObjectContext
        {
            get { return (this as IObjectContextAdapter).ObjectContext; }
        }

        public DbSet<InventoriesManagement> Inventories { get; set; }
    }
}
