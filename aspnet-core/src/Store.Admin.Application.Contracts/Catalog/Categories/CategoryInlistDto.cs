using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Store.Admin.Categories
{
    // class custom số trường trả ra trên Jtable
    public class CategoryInlistDto : EntityDto<Guid>
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public int? SortOrder { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public Guid? ParentId { get; set; }
        public bool? IsActive { get; set; }
    }
}
