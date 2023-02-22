using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Public
{
    public class BaseListFilterDto : PagedResultRequestBase
    {
        public string Keyword { get; set; }
    }
}
