using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Store.Public.Blogs
{
    public class BlogDto : IEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public Guid Id { get; set; }
    }
}
