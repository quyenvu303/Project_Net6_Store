using Microsoft.AspNetCore.Mvc;
using Store.Public.Categories;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace Store.Public.Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ICategoriesAppService _categoryappService;
        public HeaderViewComponent(ICategoriesAppService categoryappService)
        {
            _categoryappService = categoryappService;
        }
        public async Task<IViewComponentResult> InvokeAsync() {
            var model = await _categoryappService.GetListAllAsync();
            return View(model);
        }
    }
}
