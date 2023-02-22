using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Admin.Blogs
{
    public interface IBlogAppService : ICrudAppService<
        BlogDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateBlogDto,
        CreateUpdateBlogDto>
    {
        Task<PagedResultDto<BlogInlistDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<BlogInlistDto>> GetListAllAsync();
        Task DeleteMultileAsync(IEnumerable<Guid> ids);
    }
}
