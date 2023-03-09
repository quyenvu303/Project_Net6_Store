using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Banners
{
    public class CreateUpdateBannerDto
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string ImageContent { get; set; }
        public bool? Status { get; set; }
    }
}
