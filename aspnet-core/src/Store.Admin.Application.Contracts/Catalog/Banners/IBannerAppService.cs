
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Admin.Banners
{
    public interface IBannerAppService : ICrudAppService<
        BannerDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateBannerDto,
        CreateUpdateBannerDto>
    {
        Task<PagedResultDto<BannerInlistDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<BannerInlistDto>> GetListAllAsync();
        Task DeleteMultileAsync(IEnumerable<Guid> ids);
        Task<string> GetImageAsync(string fileName);
    }
}
