using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using Store.Public.Categories;
using Store.Public.Products;
using System.Linq;
using System.Threading.Tasks;
using Store.Public.Web.Models;
using Volo.Abp.Caching;

namespace Store.Public.Web.Pages;

public class IndexModel : PublicPageModel
{
    private readonly IDistributedCache<HomeCacheItem> _distributedCache;
    private readonly ICategoriesAppService _categoriesAppService;
    private readonly IProductsAppService _productsAppService;
    public List<CategoryInlistDto> Categories { get; set; }
    public List<ProductInlistDto> TopSellerProducts { get; set; }
    public IndexModel(ICategoriesAppService categoriesAppService,
        IProductsAppService productAppService, IDistributedCache<HomeCacheItem> distributedCache)
    {
        _categoriesAppService = categoriesAppService;
        _productsAppService = productAppService;
        _distributedCache = distributedCache;
    }
    public async Task OnGetAsync()
    {
        var cacheItem = await _distributedCache.GetOrAddAsync(StorePublicConsts.CacheKeys.HomeData, async () =>
        {
            var allCategories = await _categoriesAppService.GetListAllAsync();
            var rootCategories = allCategories.Where(x => x.ParentId == null).ToList();
            foreach (var category in rootCategories)
            {
                category.Children = rootCategories.Where(x => x.ParentId == category.Id).ToList();
            }

            var topSellerProducts = await _productsAppService.GetListTopSellersAsync(5);
            return new HomeCacheItem()
            {
                TopSellerProducts = topSellerProducts,
                Categories = rootCategories
            };

        },
       () => new DistributedCacheEntryOptions
       {
           AbsoluteExpiration = DateTimeOffset.Now.AddHours(12)
       });

        TopSellerProducts = cacheItem.TopSellerProducts;
        Categories = cacheItem.Categories;
    }

}
