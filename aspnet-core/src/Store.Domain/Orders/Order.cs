using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Store.Orders
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        public Order()
        {

        }
        public Order(Guid id)
        {
            Id = id;
        }
        public string OrderId { get; set; }
        public DateTime? ApplyDate { get; set; }
        public OrderStatus Status { get; set; }
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
        public string Request { get; set; }
        public PaymentMethod PaymentId { get; set; }
    }
}
