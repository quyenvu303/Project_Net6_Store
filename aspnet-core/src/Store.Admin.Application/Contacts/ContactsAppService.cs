using Store.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Store.Admin.Contacts
{
    public class ContactsAppService : CrudAppService<
        Contact,
        ContactDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateContactDto,
        CreateUpdateContactDto>, IContactAppService
    {
        public ContactsAppService(IRepository<Contact, Guid> repository) : base(repository)
        {
        }

        public async Task DeleteMultileAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<List<ContactInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            //query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Contact>, List<ContactInlistDto>>(data);
        }

        public async Task<PagedResultDto<ContactInlistDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Title.Contains(input.Keyword));
            query = query.Where(x => x.Status == input.Status);
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ContactInlistDto>(totalCount, ObjectMapper.Map<List<Contact>, List<ContactInlistDto>>(data));
        }
    }
}
