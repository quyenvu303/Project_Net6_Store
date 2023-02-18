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
        public string OrderId { get; set; }
        public DateTime? AppDate { get; set; }
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
        public PaymentMethod PaymentId { get; set; }
    }
}
