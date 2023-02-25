using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Public.Products
{
    public interface IProductAppService : IReadOnlyAppService<
        ProductDto,
        Guid,
        PagedResultRequestDto>
    {
        Task<PagedResult<ProductInlistDto>> GetListFilterAsync(ProductFilter input);
        Task<List<ProductInlistDto>> GetListAllAsync();
        Task<string> GetImageAsync(string fileName);

    }
}
