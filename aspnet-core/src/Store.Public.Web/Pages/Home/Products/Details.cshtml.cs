using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.Migrations;
using Store.Public.Categories;
using Store.Public.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Public.Web.Pages.Home.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductsAppService _productsAppService;
        private readonly ICategoriesAppService _categoriesAppService;

        public List<ProductInlistDto> ProductBySlug { get; set; }
        public DetailsModel(IProductsAppService productsAppService,
           ICategoriesAppService categoriesAppService)
        {
            _productsAppService = productsAppService;
            _categoriesAppService = categoriesAppService;
        }
        public CategoryDto Category { get; set; }
        public ProductDto Product { get; set; }
        public async Task OnGetAsync(string categorySlug, string slug)
        {
            Category = await _categoriesAppService.GetBySlugAsync(categorySlug);
            Product = await _productsAppService.GetBySlugAsync(slug);
            ProductBySlug = await _productsAppService.GetProductBySlugAsync(slug, categorySlug);
        }
    }
}
