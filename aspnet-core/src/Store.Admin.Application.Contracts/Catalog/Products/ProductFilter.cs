﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Products
{
    public class ProductFilter : BaseListFilterDto
    {
        public Guid? CategoryId { get; set; }
        public Guid? WarehouseGuid { get; set; }
    }
}
