using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Blogs
{
    public class CreateUpdateBlogDto
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string ImageContent { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
    }
}
