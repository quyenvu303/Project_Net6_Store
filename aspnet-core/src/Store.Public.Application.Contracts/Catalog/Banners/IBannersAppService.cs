using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Public.Banners
{
    public interface IBannersAppService : IReadOnlyAppService<
         BannerDto,
         Guid,
         PagedResultRequestDto>
    {
        Task<PagedResult<BannerInlistDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<BannerInlistDto>> GetListTopBannerAsync(int numberOfRecords);
        Task<List<BannerInlistDto>> GetListAllAsync();
        Task<string> GetImageAsync(string fileName);
    }
}
