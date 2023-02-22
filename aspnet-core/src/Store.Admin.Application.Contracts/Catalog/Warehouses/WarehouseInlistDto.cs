using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Store.Admin.Warehouses
{
    public class WarehouseInlistDto : EntityDto<Guid>
    {
        public string WarehouseId { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
    }
}
