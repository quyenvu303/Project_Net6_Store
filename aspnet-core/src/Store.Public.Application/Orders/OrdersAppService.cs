using AutoMapper.Internal.Mappers;
using Store.Orders;
using Store.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

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
        public OrdersAppService(IRepository<Order, Guid> repository,
            OrderCodeGenerator orderCodeGenerator,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Product, Guid> productRepository)
            : base(repository)
        {
            _orderItemRepository = orderItemRepository;
            _orderCodeGenerator = orderCodeGenerator;
            _productRepository = productRepository;
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


    }
}
