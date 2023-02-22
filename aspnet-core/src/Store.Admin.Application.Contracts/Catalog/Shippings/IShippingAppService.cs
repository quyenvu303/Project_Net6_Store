using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Admin.Shippings
{
    public interface IShippingAppService : ICrudAppService<
       ShippingDto,
       Guid,
       PagedResultRequestDto,
       CreateUpdateShippingDto,
       CreateUpdateShippingDto>
    {
        Task<PagedResultDto<ShippingInlistDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<ShippingInlistDto>> GetListAllAsync();
        Task DeleteMultileAsync(IEnumerable<Guid> ids);
    }
}
