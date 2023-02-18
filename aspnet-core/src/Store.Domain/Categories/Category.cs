using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Store.Categories
{
    public class Category : CreationAuditedAggregateRoot<Guid>
    {
        public Category() { }
        public Category(Guid id, string categoryId, string categoryName, int? sortOrder, string description, Guid? parentId, bool? isActive)
        {
            Id = id;
            CategoryId = categoryId;
            CategoryName = categoryName;
            SortOrder = sortOrder;
            Description = description;
            ParentId = parentId;
            IsActive = isActive;
        }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? SortOrder { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public bool? IsActive { get; set; }

    }
}
