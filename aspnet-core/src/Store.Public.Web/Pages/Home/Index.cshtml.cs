using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using Store.Public.Categories;
using Store.Public.Products;
using System.Linq;
using System.Threading.Tasks;
using Store.Public.Web.Models;
using Volo.Abp.Caching;
using Store.Public.Banners;
using Store.Public.Blogs;

namespace Store.Public.Web.Pages;

public class IndexModel : PublicPageModel
{
    private readonly IDistributedCache<HomeCacheItem> _distributedCache;
    private readonly ICategoriesAppService _categoriesAppService;
    private readonly IProductsAppService _productsAppService;
    private readonly IBannersAppService _bannerAppService;
    public List<CategoryInlistDto> Categories { get; set; }
    public List<ProductInlistDto> TopSellerProducts { get; set; }
    public List<ProductInlistDto> TopTrendingProducts { get; set; }
    public List<BannerInlistDto> Banner { get; set; }
    public IndexModel(ICategoriesAppService categoriesAppService,
                    IBannersAppService bannerAppService,
                    IProductsAppService productAppService,
                    IDistributedCache<HomeCacheItem> distributedCache)
    {
        _categoriesAppService = categoriesAppService;
        _productsAppService = productAppService;
        _bannerAppService = bannerAppService;
        _distributedCache = distributedCache;
    }
    public async Task OnGetAsync()
    {

        //var allBanner = await _bannerAppService.GetListTopBannerAsync(4);
        //var allCategories = await _categoriesAppService.GetListAllAsync();
        //var rootCategories = allCategories.Where(x => x.ParentId == null).ToList();
        //foreach (var category in rootCategories)
        //{
        //    category.Children = rootCategories.Where(x => x.ParentId == category.Id).ToList();
        //}

        //var topSellerProducts = await _productsAppService.GetListTopSellersAsync(5);
        //var topTrendingProducts = await _productsAppService.GetListTrendingAsync(5);

        //TopSellerProducts = topSellerProducts;
        //TopTrendingProducts = topTrendingProducts;
        //Categories = rootCategories;
        //Banner = allBanner;

        var cacheItem = await _distributedCache.GetOrAddAsync(StorePublicConsts.CacheKeys.HomeData, async () =>
        {
            var allBanner = await _bannerAppService.GetListTopBannerAsync(4);
            var allCategories = await _categoriesAppService.GetListAllAsync();
            var rootCategories = allCategories.Where(x => x.ParentId == null).ToList();
            foreach (var category in rootCategories)
            {
                category.Children = rootCategories.Where(x => x.ParentId == category.Id).ToList();
            }

            var topSellerProducts = await _productsAppService.GetListTopSellersAsync(5);
            var topTrendingProducts = await _productsAppService.GetListTrendingAsync(5);
            return new HomeCacheItem()
            {
                TopSellerProducts = topSellerProducts,
                TopTrendingProducts = topTrendingProducts,
                Categories = rootCategories,
                Banner = allBanner,
            };

        },
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(0)
        });

        TopSellerProducts = cacheItem.TopSellerProducts;
        TopTrendingProducts = cacheItem.TopTrendingProducts;
        Categories = cacheItem.Categories;
        Banner = cacheItem.Banner;
    }

}
