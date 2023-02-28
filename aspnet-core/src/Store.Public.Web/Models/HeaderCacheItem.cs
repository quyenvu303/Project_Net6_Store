using Store.Public.Categories;
using System.Collections.Generic;

namespace Store.Public.Web.Models
{
    public class HeaderCacheItem
    {
        public List<CategoryInlistDto> Categories { set; get; }
    }
}
