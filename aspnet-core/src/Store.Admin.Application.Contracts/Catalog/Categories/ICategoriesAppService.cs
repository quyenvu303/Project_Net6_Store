using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Admin.Categories
{
    public interface ICategoriesAppService : ICrudAppService
       <CategoryDto,
       Guid,
       PagedResultRequestDto,
       CreateUpdateCategoryDto,
       CreateUpdateCategoryDto>
    {
        Task<PagedResultDto<CategoryInlistDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<CategoryInlistDto>> GetListAllAsync();
        Task DeleteMultileAsync(IEnumerable<Guid> ids);
    }
}
