using Store.Public.Banners;
using Store.Public.Blogs;
using Store.Public.Categories;
using System.Collections.Generic;

namespace Store.Public.Web.Models
{
    public class HeaderCacheItem
    {
        public List<CategoryInlistDto> Categories { set; get; }
        public List<CartItem> CartItems { get; set; }
    }
}
