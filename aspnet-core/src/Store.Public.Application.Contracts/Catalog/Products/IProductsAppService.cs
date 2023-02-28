using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Public.Products
{
    public interface IProductsAppService : IReadOnlyAppService<
        ProductDto,
        Guid,
        PagedResultRequestDto>
    {
        Task<PagedResult<ProductInlistDto>> GetListFilterAsync(ProductFilter input);
        Task<List<ProductInlistDto>> GetListAllAsync();
        Task<List<ProductInlistDto>> GetListTopSellersAsync(int numberOfRecords);
        Task<string> GetImageAsync(string fileName);

        Task<List<ProductInlistDto>> GetByIdAsync(string CategoryId);

    }
}
