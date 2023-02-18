using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Categories
{
    public class CreateUpdateCategoryDto
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? SortOrder { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public bool? IsActive { get; set; }
    }
}
