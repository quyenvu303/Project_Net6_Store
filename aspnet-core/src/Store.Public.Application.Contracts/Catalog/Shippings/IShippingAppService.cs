using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Public.Shippings
{
 
    public interface IShippingAppService : IReadOnlyAppService<
         ShippingDto,
         Guid,
         PagedResultRequestDto>
    {
        Task<List<ShippingInlistDto>> GetListAllAsync();
        Task<ShippingInlistDto> GetShipingFreeAsync();
    }
}
