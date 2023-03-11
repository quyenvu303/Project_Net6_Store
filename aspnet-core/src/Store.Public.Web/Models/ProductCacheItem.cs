using Microsoft.AspNetCore.Mvc;
using Store.Public.Categories;
using Store.Public.Products;
using System.Collections.Generic;

namespace Store.Public.Web.Models
{
    public class ProductCacheItem
    {
        public CategoryDto Category { set; get; }
        public List<CategoryInlistDto> Categories { set; get; }
        public List<CategoryInlistDto> ItemByCategories { set; get; }
        public List<ProductInlistDto> Product { set; get; }
        public PagedResult<ProductInlistDto> ProductData { set; get; }

    }
}
