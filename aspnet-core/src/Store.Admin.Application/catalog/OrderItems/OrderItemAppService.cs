using Store.Admin.Orders;
using Store.Admin.Products;
using Store.Banners;
using Store.Orders;
using Store.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Store.Admin.OrderItems
{
    public class OrderItemAppService : CrudAppService<
         OrderItem,
         OrderItemDto,
         Guid,
         PagedResultRequestDto>, IOrderItemAppService
    {
        private readonly IBlobContainer<ProductImageContainer> _fileContainer;
        public OrderItemAppService(IRepository<OrderItem, Guid> repository,
            IBlobContainer<ProductImageContainer> fileContainer) : base(repository)
        {
            _fileContainer = fileContainer;
        }
        public async Task<List<OrderItemDto>> GetOrderListItemsAsync(string Id)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.OrderId.ToString() == Id);
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<OrderItem>, List<OrderItemDto>>(data);
        }

        public async Task<string> GetImageAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }
            var thumbnailContent = await _fileContainer.GetAllBytesOrNullAsync(fileName);

            if (thumbnailContent is null)
            {
                return null;
            }
            var result = Convert.ToBase64String(thumbnailContent);
            return result;
        }

    }
}
