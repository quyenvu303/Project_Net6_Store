using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Store.Blogs
{
    public class Blog : Entity<Guid>
    {
        public Blog() { }
        public Blog(Guid id, string title, string image, string description, bool? status)
        {
            Id = id;
            Title = title;
            Image = image;
            Description = description;
            Status = status;
        }

        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
    }
}
