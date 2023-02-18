using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Admin.Warehouses
{
    public interface IWarehouseAppService : ICrudAppService<
        WarehouseDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateWarehouseDto,
        CreateUpdateWarehouseDto>
    {
        Task<PagedResultDto<WarehouseInlistDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<WarehouseInlistDto>> GetListAllAsync();
        Task DeleteMultileAsync(IEnumerable<Guid> ids);
    }
}
