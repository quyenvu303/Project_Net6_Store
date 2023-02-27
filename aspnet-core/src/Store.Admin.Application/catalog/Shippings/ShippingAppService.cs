using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Store.Admin.Shippings;
using Store.Admin.Permissions;
using Store.Shippings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Store.Admin.Shippings
{
    public class ShippingAppService : CrudAppService<
         Shipping,
         ShippingDto,
         Guid,
         PagedResultRequestDto,
         CreateUpdateShippingDto,
         CreateUpdateShippingDto>, IShippingAppService
    {
        public ShippingAppService(IRepository<Shipping, Guid> repository) : base(repository)
        {
        }

        public async Task DeleteMultileAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<List<ShippingInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            //query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Shipping>, List<ShippingInlistDto>>(data);
        }

        public async Task<PagedResultDto<ShippingInlistDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.ShippingName.Contains(input.Keyword));
            query = query.Where(x => x.Status == input.Status);
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ShippingInlistDto>(totalCount, ObjectMapper.Map<List<Shipping>, List<ShippingInlistDto>>(data));
        }
    }
}
