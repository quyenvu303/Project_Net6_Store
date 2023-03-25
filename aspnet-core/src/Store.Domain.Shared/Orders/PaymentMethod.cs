using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Store.Orders
{
    public enum PaymentMethod
    {
        [Display(Name = "Thanh toán khi nhận hàng")]
        COD = 1,
        [Display(Name = "Thanh toán Online")]
        OnlinePayment = 2,
    }
}
