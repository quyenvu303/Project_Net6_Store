using Microsoft.AspNetCore.Mvc;
using Store.Public.Blogs;
using System.Collections.Generic;

namespace Store.Public.Web.Models
{
    public class FooterCacheItem
    {
        public List<BlogInlistDto> TopBlog { get; set; }
    }
}
