using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Store.Contacts
{
    public class Contact : Entity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
    }
}
