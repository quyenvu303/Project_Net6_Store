using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Admin.Contacts
{
    public interface IContactAppService : ICrudAppService<
        ContactDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateContactDto,
        CreateUpdateContactDto>
    {
        Task<PagedResultDto<ContactInlistDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<ContactInlistDto>> GetListAllAsync();
        Task DeleteMultileAsync(IEnumerable<Guid> ids);
    }
}
