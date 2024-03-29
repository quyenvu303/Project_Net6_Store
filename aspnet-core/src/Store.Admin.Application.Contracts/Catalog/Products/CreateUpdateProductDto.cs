﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Products
{
    public class CreateUpdateProductDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Slug { get; set; }
        public Guid CategoryId { get; set; }
        public Guid WarehouseGuid { get; set; }
        public string Origin { get; set; }
        public string ImageName { get; set; }
        public string ImageContent { get; set; }
        public int? TotalQuantity { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceSale { get; set; }
        public string Parameter { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? Status { get; set; }
        public bool? BestSellers { get; set; }
        public bool? Trending { get; set; }
    }
}
