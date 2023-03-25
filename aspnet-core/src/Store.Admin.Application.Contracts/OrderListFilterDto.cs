using Store.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Store.Admin
{
    public class OrderListFilterDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Id { get; set; }
    }
}
