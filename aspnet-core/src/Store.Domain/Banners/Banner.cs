using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Store.Banners
{
    public class Banner : Entity<Guid>
    {
        public Banner() { }
        public Banner(Guid id, string title, string image, bool? status)
        {
            Id = id;
            Title = title;
            Image = image;
            Status = status;
        }

        public string Title { get; set; }
        public string Image { get; set; }
        public bool? Status { get; set; }
    }
}
