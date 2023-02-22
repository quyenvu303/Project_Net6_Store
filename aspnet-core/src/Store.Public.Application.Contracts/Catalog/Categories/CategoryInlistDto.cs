﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Store.Public.Categories
{
    // class custom số trường trả ra trên Jtable
    public class CategoryInlistDto : EntityDto<Guid>
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? SortOrder { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public bool? IsActive { get; set; }
        public List<CategoryInlistDto> Children { get; set; } = new List<CategoryInlistDto>();
    }
}