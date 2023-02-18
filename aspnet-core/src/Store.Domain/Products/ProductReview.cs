using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Store.Products
{
    public class ProductReview : CreationAuditedEntity<Guid>
    {
        public string Title { get; set; }
        public Guid ProductId { get; set; }
        public Guid ParentId { get; set; }
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
