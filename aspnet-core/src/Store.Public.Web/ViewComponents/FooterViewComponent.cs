using Microsoft.AspNetCore.Mvc;
using Store.Public.Blogs;
using Store.Public.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace Store.Public.Web.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly IDistributedCache<FooterCacheItem> _distributedCache;
        private readonly IBlogsAppService _blogsAppService;
        public List<BlogInlistDto> TopBlog { get; set; }
        public FooterViewComponent(IBlogsAppService blogsAppService,
                    IDistributedCache<FooterCacheItem> distributedCache)
        {
            _blogsAppService = blogsAppService;
            _distributedCache = distributedCache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var cacheItem = await _distributedCache.GetOrAddAsync(StorePublicConsts.CacheKeys.FooterData, async () =>
            {
                var topBlog = await _blogsAppService.GetListTopBlogAsync(3);
                return new FooterCacheItem()
                {
                    TopBlog = topBlog,
                };
            },
            () => new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(0)
            });
            return View(cacheItem.TopBlog);

        }
    }
}
