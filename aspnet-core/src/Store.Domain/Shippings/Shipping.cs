using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Store.Shippings
{
    public class Shipping : Entity<Guid>
    {
        public string ShippingName { get; set; }
        public decimal? ShippingFee { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
    }
}
