using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Public.Products
{
    public class ProductFilter : BaseListFilterDto
    {
        public Guid? CategoryId { get; set; }
        public string CateId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? WarehouseGuid { get; set; }
    }
}
