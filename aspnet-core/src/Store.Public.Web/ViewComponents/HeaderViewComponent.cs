using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Store.Public.Banners;
using Store.Public.Blogs;
using Store.Public.Categories;
using Store.Public.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace Store.Public.Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IDistributedCache<HeaderCacheItem> _distributedCache;
        private readonly ICategoriesAppService _categoryappService;
        public List<CategoryInlistDto> Categories { get; set; }
        public List<BlogInlistDto> TopBlog { get; set; }
        public HeaderViewComponent(ICategoriesAppService categoryappService,
        IDistributedCache<HeaderCacheItem> distributedCache)
        {
            _categoryappService = categoryappService;
            _distributedCache = distributedCache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var cacheItem = await _distributedCache.GetOrAddAsync(StorePublicConsts.CacheKeys.HeaderData, async () =>
            {
                var allCategories = await _categoryappService.GetListAllAsync();
                var roootCategory = allCategories.Where(x => x.ParentId == null).ToList();
                foreach (var category in roootCategory)
                {
                    category.Children = allCategories.Where(x => x.ParentId == category.Id).ToList();
                }
                return new HeaderCacheItem()
                {
                    Categories = roootCategory,
                };
            },
            () => new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(0)
            });
            return View(cacheItem.Categories);

        }
    }
}
