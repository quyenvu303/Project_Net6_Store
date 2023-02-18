using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Store.Warehouses
{
    public class WarehouseDetail : Entity<Guid>
    {
        public Guid WarehouseGuid { get; set; }
        public Guid ProductGuid { get; set; }
        public DateTime? ApplyDate { get; set; }
        public decimal Quantity { get; set; }
        public override object[] GetKeys()
        {
            return new object[] { WarehouseGuid, ProductGuid };
        }
    }
}
