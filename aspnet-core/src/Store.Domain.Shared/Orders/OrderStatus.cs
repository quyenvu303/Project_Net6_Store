using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Store.Orders
{
    public enum OrderStatus
    {
        [Display(Name = "Chờ xác nhận")]
        New = 1,
        [Display(Name = "Đã xác nhận")]
        Confirmed = 2,
        [Display(Name = "Đang xử lý")]
        Processing = 3,
        [Display(Name = "Đang giao hàng")]
        Shipping = 4,
        [Display(Name = "Giao hàng thành công")]
        Finished = 5,
        [Display(Name = "Giao hàng không thành công")]
        Canceled = 6,
        [Display(Name = "Yêu cầu hủy đơn")]
        RequestCancel = 7
    }
}
