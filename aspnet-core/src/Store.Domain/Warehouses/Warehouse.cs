using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Store.Warehouses
{
    public class Warehouse : CreationAuditedAggregateRoot<Guid>
    {
        public Warehouse() { }

        public Warehouse(Guid id, string warehouseId, string title, bool? status)
        {
            WarehouseId = warehouseId;
            Title = title;
            Status = status;
        }

        public string WarehouseId { get; set; }
        public string Title { get; set; }
        public bool? Status { get; set; }
    }
}
