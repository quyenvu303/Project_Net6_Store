using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Store.Public.Shippings
{
    public class ShippingInlistDto : EntityDto<Guid>
    {
        public string ShippingName { get; set; }
        public double ShippingFee { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
    }
}
