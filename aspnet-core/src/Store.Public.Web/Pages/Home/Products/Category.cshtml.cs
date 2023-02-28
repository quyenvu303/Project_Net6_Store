using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.Public.Categories;
using Store.Public.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Public.Web.Pages.Home.Products
{
    public class CategoryModel : PageModel
    {
        public CategoryDto Category { set; get; }
        public List<CategoryInlistDto> Categories { set; get; }
        public List<ProductInlistDto> Product { set; get; }
        public PagedResult<ProductInlistDto> ProductData { set; get; }


        private readonly ICategoriesAppService _categoriesAppService;
        private readonly IProductsAppService _productsAppService;
        public CategoryModel(ICategoriesAppService categoriesAppService,
           IProductsAppService productsAppService)
        {
            _categoriesAppService = categoriesAppService;
            _productsAppService = productsAppService;
        }

        public async Task OnGetAsync(string CategoryId, int page = 1)
        {
            Category = await _categoriesAppService.GetByCategoryIdAsync(CategoryId);
            Product = await _productsAppService.GetByIdAsync(CategoryId);
            Categories = await _categoriesAppService.GetListAllAsync();
            ProductData = await _productsAppService.GetListFilterAsync(new ProductFilter()
            {
                CurrentPage = page,
                CateId = CategoryId,
            });
        }
    }
}
