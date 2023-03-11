using Store.Public.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Public.Orders
{
    public interface IOrdersAppService : ICrudAppService
        <OrderDto,
        Guid,
        PagedResultRequestDto, CreateOrderDto, CreateOrderDto>
    {
        Task<PagedResult<OrderDto>> GetOrderItemsAsync(ProductFilter input);
    }
}
