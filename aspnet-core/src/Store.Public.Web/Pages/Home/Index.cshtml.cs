using Store.Public.Categories;
using Store.Public.Products;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Public.Web.Pages;

public class IndexModel : PublicPageModel
{
    private readonly ICategoriesAppService _categoriesAppService;
    public List<CategoryInlistDto> Categories { get; set; }
    public IndexModel(ICategoriesAppService categoriesAppService)
    {
        _categoriesAppService = categoriesAppService;
    }
    public async Task OnGetAsync()
    {
        var allCategories = await _categoriesAppService.GetListAllAsync();
        var roootCategory = allCategories.Where(x => x.ParentId == null).ToList();
        foreach (var category in roootCategory)
        {
            category.Children = allCategories.Where(x => x.ParentId == category.Id).ToList();
        }
        Categories = roootCategory;
    }
}
