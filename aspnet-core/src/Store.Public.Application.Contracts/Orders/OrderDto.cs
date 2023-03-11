using Store.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Store.Public.Orders
{
    public class OrderDto : EntityDto<Guid>
    {
        public string OrderId { get; set; }
        public DateTime? AppDate { get; set; }
        //public OrderStatus Status { get; set; }
        public int? Status { get; set; }
        public string ShippingName { get; set; }
        public decimal? ShippingFee { get; set; }
        public double Total { get; set; }
        public double Subtotal { get; set; }
        public double Discount { get; set; }
        public double GrandTotal { get; set; }
        public Guid? CusomerUserId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string Description { get; set; }
        public PaymentMethod PaymentId { get; set; }
    }
}
