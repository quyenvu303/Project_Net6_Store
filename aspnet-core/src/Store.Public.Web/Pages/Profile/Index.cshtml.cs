using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Store.Public.Orders;
using Store.Public.Products;
using Store.Public.Web.Extensions;
using Store.Public.Web.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Users;

namespace Store.Public.Web.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IDistributedCache<ProfileCacheItem> _distributedCache;

        public PagedResult<OrderDto> OrderData { set; get; }

        private readonly IOrdersAppService _ordersAppService;
        public IndexModel(IDistributedCache<ProfileCacheItem> distributedCache, IOrdersAppService ordersAppService)
        {
            _ordersAppService= ordersAppService;
            _distributedCache = distributedCache;
        }
        public async Task OnGetAsync(int page = 1)
        {
            var cacheItem = await _distributedCache.GetOrAddAsync(StorePublicConsts.CacheKeys.ProductData, async () =>
            {
                Guid? currentUserId = User.Identity.IsAuthenticated ? User.GetUserId() : null;
                var ProductData = await _ordersAppService.GetOrderItemsAsync(new ProductFilter()
                {
                    CurrentPage = page,
                    UserId = currentUserId,
                });

                return new ProfileCacheItem()
                {
                    OrderData = ProductData,
                };
            },
           () => new DistributedCacheEntryOptions
           {
               AbsoluteExpiration = DateTimeOffset.Now.AddHours(0)
           });

            OrderData = cacheItem.OrderData;
        }
    }
}
