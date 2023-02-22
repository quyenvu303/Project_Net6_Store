using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Warehouses
{
    public class CreateUpdateWarehouseDto
    {
        public string WarehouseId { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
    }
}
