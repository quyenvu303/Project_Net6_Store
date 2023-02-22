using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Store.Admin.Shippings
{
    public class ShippingDto : IEntityDto<Guid>
    {
        public string ShippingName { get; set; }
        public decimal? ShippingFee { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public Guid Id { get; set; }
    }
}
