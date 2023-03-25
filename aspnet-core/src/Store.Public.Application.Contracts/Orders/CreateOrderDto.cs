using Store.Public.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Public.Orders
{
    public class CreateOrderDto
    {
        public Guid? CustomerUserId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string Description { get; set; }
        public string Request { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
