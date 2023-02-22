using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Contacts
{
    public class CreateUpdateContactDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
    }
}
