using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Store.Admin.Permissions;
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
    [Authorize(StorePermissions.Order.Default)]
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
            GetPolicyName = StorePermissions.Order.Default;
            GetListPolicyName = StorePermissions.Order.Default;
            CreatePolicyName = StorePermissions.Order.Create;
            UpdatePolicyName = StorePermissions.Order.Update;
            DeletePolicyName = StorePermissions.Order.Delete;
        }

        [Authorize(StorePermissions.Order.Default)]
        public async Task<List<OrderInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            //query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Order>, List<OrderInlistDto>>(data);
        }
        [Authorize(StorePermissions.Order.Default)]
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
