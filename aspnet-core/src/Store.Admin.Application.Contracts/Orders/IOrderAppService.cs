using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Admin.Orders
{
    public interface IOrderAppService : ICrudAppService<
       OrderDto,
       Guid,
       PagedResultRequestDto,
       CreateUpdateOrderDto,
       CreateUpdateOrderDto>
    {
        Task<PagedResultDto<OrderInlistDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<OrderInlistDto>> GetListAllAsync();
    }
}
