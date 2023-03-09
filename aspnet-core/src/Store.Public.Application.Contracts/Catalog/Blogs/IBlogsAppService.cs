using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Public.Blogs
{
    public interface IBlogsAppService : IReadOnlyAppService<
        BlogDto,
        Guid,
        PagedResultRequestDto>
    {
        Task<PagedResult<BlogInlistDto>> GetListFilterAsync(BaseListFilterDto input);

        Task<List<BlogInlistDto>> GetListTopBlogAsync(int numberOfRecords);
        Task<List<BlogInlistDto>> GetListAllAsync();
        Task<string> GetImageAsync(string fileName);
        Task<BlogDto> GetBlogByIdAsync(Guid? Id, string Title);
    }
}
