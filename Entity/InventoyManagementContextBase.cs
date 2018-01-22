using System;
using Entity.Entity;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Entity
{
    public class InventoyManagementContextBase : DbContext
    {
        public InventoyManagementContextBase() : base()
        {
        }
        public ObjectContext ObjectContext
        {
            get { return (this as IObjectContextAdapter).ObjectContext; }
        }
        public DbSet<Inventories> Inventories { get; set; }
    }
}
