using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Admin.Products
{
    public interface IProductAppService : ICrudAppService<
        ProductDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateProductDto,
        CreateUpdateProductDto>
    {
        Task<PagedResultDto<ProductInlistDto>> GetListFilterAsync(ProductFilter input);
        Task<List<ProductInlistDto>> GetListAllAsync();
        Task DeleteMultileAsync(IEnumerable<Guid> ids);

        Task<string> GetImageAsync(string fileName);

        Task<string> GetSuggestNewCodeAsync();
    }
}
