using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Store.Public.Categories;
using Store.Public.Products;
using Store.Public.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace Store.Public.Web.Pages.Home.Products
{
    public class CategoryModel : PageModel
    {
        private readonly IDistributedCache<ProductCacheItem> _distributedCache;
        public CategoryDto Category { set; get; }
        public List<CategoryInlistDto> Categories { set; get; }
        public List<ProductInlistDto> Product { set; get; }
        public PagedResult<ProductInlistDto> ProductData { set; get; }
        private readonly ICategoriesAppService _categoriesAppService;
        private readonly IProductsAppService _productsAppService;
        public CategoryModel(ICategoriesAppService categoriesAppService,
           IProductsAppService productsAppService,
           IDistributedCache<ProductCacheItem> distributedCache)
        {
            _categoriesAppService = categoriesAppService;
            _productsAppService = productsAppService;
            _distributedCache = distributedCache;
        }
        public async Task OnGetAsync(string CategoryId, int page = 1)
        {

            var cacheItem = await _distributedCache.GetOrAddAsync(StorePublicConsts.CacheKeys.ProductData, async () => 
            {
                var Category = await _categoriesAppService.GetByCategoryIdAsync(CategoryId);
                var Product = await _productsAppService.GetByIdAsync(CategoryId);

                var allCategories = await _categoriesAppService.GetListAllAsync();
                var rootCategories = allCategories.Where(x => x.ParentId == null).ToList();

                var ProductData = await _productsAppService.GetListFilterAsync(new ProductFilter()
                {
                    CurrentPage = page,
                    CateId = CategoryId,
                });
                return new ProductCacheItem()
                {
                    Category = Category,
                    Product = Product,
                    Categories = rootCategories,
                    ProductData = ProductData,
                };
            },
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(0)
            });
            Category = cacheItem.Category;
            Product = cacheItem.Product;
            Categories = cacheItem.Categories;
            ProductData = cacheItem.ProductData;
        }
    }
}
