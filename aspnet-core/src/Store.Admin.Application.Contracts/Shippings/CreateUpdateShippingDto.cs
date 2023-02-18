using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Shippings
{
    public class CreateUpdateShippingDto
    {
        public string ShippingName { get; set; }
        public decimal? ShippingFee { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
    }
}
