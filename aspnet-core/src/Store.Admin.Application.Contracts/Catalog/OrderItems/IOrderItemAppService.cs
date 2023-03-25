using Store.Admin.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Admin.OrderItems
{
    public interface IOrderItemAppService : ICrudAppService<
       OrderItemDto,
       Guid,
       PagedResultRequestDto>
    {
        Task<List<OrderItemDto>> GetOrderListItemsAsync(string Id);
    }
}
