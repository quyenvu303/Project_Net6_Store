using Microsoft.AspNetCore.Mvc;
using Store.Public.Categories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace Store.Public.Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ICategoriesAppService _categoryappService;
        public List<CategoryInlistDto> Categories { get; set; }
        public HeaderViewComponent(ICategoriesAppService categoryappService)
        {
            _categoryappService = categoryappService;
        }
        public async Task<IViewComponentResult> InvokeAsync() {

            var allCategories = await _categoryappService.GetListAllAsync();
            var roootCategory = allCategories.Where(x => x.ParentId == null).ToList();
            foreach (var category in roootCategory)
            {
                category.Children = allCategories.Where(x => x.ParentId == category.Id).ToList();
            }
            Categories = roootCategory;


            return View(Categories);
        }
    }
}
