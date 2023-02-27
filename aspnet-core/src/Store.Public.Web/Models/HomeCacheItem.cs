using Store.Public.Categories;
using Store.Public.Products;
using System.Collections.Generic;

namespace Store.Public.Web.Models
{
    public class HomeCacheItem
    {
        public List<CategoryInlistDto> Categories { set; get; }
        public List<ProductInlistDto> TopSellerProducts { set; get; }
    }
}
