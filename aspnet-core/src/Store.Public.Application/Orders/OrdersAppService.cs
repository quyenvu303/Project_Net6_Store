using AutoMapper.Internal.Mappers;
using Store.Orders;
using Store.Products;
using Store.Public.Orders;
using Store.Public.Orders;
using Store.Public.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Store.Public.Orders
{
    public class OrdersAppService : CrudAppService<
       Order,
       OrderDto,
       Guid,
       PagedResultRequestDto,
       CreateOrderDto,
       CreateOrderDto>, IOrdersAppService
    {
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly OrderCodeGenerator _orderCodeGenerator;
        private readonly IRepository<Product, Guid> _productRepository;

        private readonly IRepository<Order> _orderRepository;
        public OrdersAppService(IRepository<Order, Guid> repository,
            OrderCodeGenerator orderCodeGenerator,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Order> orderRepository,
            IRepository<Product, Guid> productRepository)
            : base(repository)
        {
            _orderItemRepository = orderItemRepository;
            _orderCodeGenerator = orderCodeGenerator;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public override async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            var subTotal = input.Items.Sum(x => x.Quantity * x.Price);

            var orderId = Guid.NewGuid();
            var order = new Order(orderId)
            {
                OrderId = await _orderCodeGenerator.GenerateAsync(),
                AppDate = DateTime.Now,
                Status = OrderStatus.New,
                ShippingName = "",
                ShippingFee = 0,
                Total = subTotal,
                Subtotal = subTotal,
                Discount = 0,
                GrandTotal = subTotal,
                CusomerUserId = input.CustomerUserId,
                CustomerName = input.CustomerName,
                CustomerPhoneNumber = input.CustomerPhoneNumber,
                CustomerEmail = input.CustomerEmail,
                CustomerAddress = input.CustomerAddress,
                Description = input.Description,
                PaymentId = PaymentMethod.COD,
            };
            var items = new List<OrderItem>();
            foreach (var item in input.Items)
            {
                var product = await _productRepository.GetAsync(item.ProductId);

                items.Add(new OrderItem()
                {
                    OrderId = orderId,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Total = item.Price * item.Quantity,
                });
            }
            await _orderItemRepository.InsertManyAsync(items);
            var result = await Repository.InsertAsync(order);
            return ObjectMapper.Map<Order, OrderDto>(result);
        }

        public async Task<PagedResult<OrderDto>> GetOrderItemsAsync(ProductFilter input)
        {
            var query = await Repository.GetQueryableAsync();

            var order = await _orderRepository.GetListAsync(x => x.CusomerUserId == input.UserId);
           
            query = query.Where(x => x.CusomerUserId == input.UserId);

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter
               .ToListAsync(
                  query.Skip((input.CurrentPage - 1) * input.PageSize)
               .Take(input.PageSize));

            return new PagedResult<OrderDto>(
                ObjectMapper.Map<List<Order>, List<OrderDto>>(data),
                totalCount,
                input.CurrentPage,
                input.PageSize
            );
        }
    }
}
