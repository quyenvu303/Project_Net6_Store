using Microsoft.AspNetCore.Mvc;
using Store.Public.Categories;
using Store.Public.Orders;
using Store.Public.Products;
using Store.Public.Products;
using System.Collections.Generic;

namespace Store.Public.Web.Models
{
    public class ProfileCacheItem
    {
        public PagedResult<OrderDto> OrderData { set; get; }
        public PagedResult<OrderItemDto> OrderItems { get; set; }

        public bool isOrderDetailReady { get; set; }

    }
}
