using Store.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Store.Admin.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class StoreController : AbpControllerBase
{
    protected StoreController()
    {
        LocalizationResource = typeof(StoreResource);
    }
}
