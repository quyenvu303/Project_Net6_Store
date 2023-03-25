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
using Volo.Abp;
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

        // Create Order 
        public override async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            var subTotal = input.Items.Sum(x => x.Quantity * x.Price);

            var orderId = Guid.NewGuid();
            var order = new Order(orderId)
            {
                OrderId = await _orderCodeGenerator.GenerateAsync(),
                ApplyDate = DateTime.Now,
                Status = OrderStatus.New,
                ShippingName = "",
                ShippingFee = 30,
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
                Request = "0",
                PaymentId = PaymentMethod.COD,
            };
            var items = new List<OrderItem>();
            foreach (var item in input.Items)
            {
                var product = await _productRepository.GetAsync(item.ProductId);

                items.Add(new OrderItem()
                {
                    Id = orderId,
                    OrderId = orderId,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    ProductName = product.ProductName,
                    ProductImage = product.Image,
                    Quantity = item.Quantity,
                    Total = item.Price * item.Quantity,
                });
            }
            await _orderItemRepository.InsertManyAsync(items);
            var result = await Repository.InsertAsync(order);
            return ObjectMapper.Map<Order, OrderDto>(result);
        }

        // yeu cau huy don hang
        public async Task<OrderDto> UpdateCancelItemAsync(Guid Id)
        {
            var order = await Repository.GetAsync(Id);
            order.Status = OrderStatus.RequestCancel;
            await Repository.UpdateAsync(order);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        public async Task<OrderDto> UpdateRequestCancelAsync(Guid Id)
        {
            var order = await Repository.GetAsync(Id);
            order.Status = OrderStatus.New;
            await Repository.UpdateAsync(order);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        // Get List All Order
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

        // Get OrderItem By Id
        public async Task<List<OrderItemDto>> GetOrderItemByIdAsync(string Id)
        {
            var query = await _orderItemRepository.GetQueryableAsync();
            query = query.Where(x => x.OrderId.ToString() == Id);

            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<OrderItem>, List<OrderItemDto>>(data);
        }

        public async Task<OrderDto> GetOrderByIdAsync(string Id)
        {
            var order = await _orderRepository.GetAsync(x => x.Id.ToString() == Id);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }
    }
}
