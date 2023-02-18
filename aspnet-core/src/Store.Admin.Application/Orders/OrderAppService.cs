using AutoMapper.Internal.Mappers;
using Store.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Store.Admin.Orders
{
    public class OrderAppService : CrudAppService<
         Order,
         OrderDto,
         Guid,
         PagedResultRequestDto,
         CreateUpdateOrderDto,
         CreateUpdateOrderDto>, IOrderAppService
    {
        public OrderAppService(IRepository<Order, Guid> repository) : base(repository)
        {
        }

        public async Task<List<OrderInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            //query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Order>, List<OrderInlistDto>>(data);
        }

        public async Task<PagedResultDto<OrderInlistDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.OrderId.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<OrderInlistDto>(totalCount, ObjectMapper.Map<List<Order>, List<OrderInlistDto>>(data));
        }
    }
}
