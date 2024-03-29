﻿using Store.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Orders
{
    public class CreateUpdateOrderDto
    {
        public string OrderId { get; set; }
        public DateTime? ApplyDate { get; set; }
        public OrderStatus Status { get; set; }
        public string ShippingName { get; set; }
        public decimal? ShippingFee { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public Guid? CusomerUserId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string Description { get; set; }
        public string Request { get; set; }
        public PaymentMethod PaymentId { get; set; }
    }
}
