using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Public.Categories
{
    public interface ICategoriesAppService : IReadOnlyAppService
       <CategoryDto,
       Guid,
       PagedResultRequestDto>
    {
        Task<PagedResult<CategoryInlistDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<CategoryInlistDto>> GetListAllAsync();
        Task<string> GetImageAsync(string fileName);

        Task<CategoryDto> GetByCategoryIdAsync(string CategoryId);

        Task<CategoryDto> GetBySlugAsync(string slug);
    }
}
