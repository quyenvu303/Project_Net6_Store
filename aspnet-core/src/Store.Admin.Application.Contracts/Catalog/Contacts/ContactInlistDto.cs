﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Store.Admin.Contacts
{
    public class ContactInlistDto : EntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
    }
}
