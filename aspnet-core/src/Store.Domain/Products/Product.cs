using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Store.Products
{
    public class Product : CreationAuditedAggregateRoot<Guid>
    {
        public Product() { }

        public Product(Guid id, string productId, string productName, string slug,
            Guid categoryId, Guid warehouseGuid, string origin, 
            string image, int? quantity, decimal? price, decimal? priceSale, 
            string parameter, string description, bool? isActive, bool? status,
            string categoryName, string warehouseName, string categorySlug)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
            Slug = slug;
            CategoryId = categoryId;
            WarehouseGuid = warehouseGuid;
            Origin = origin;
            Image = image;
            Quantity = quantity;
            Price = price;
            PriceSale = priceSale;
            Parameter = parameter;
            Description = description;
            IsActive = isActive;
            Status = status;
            CategoryName = categoryName;
            WarehouseName = warehouseName;
            CategorySlug = categorySlug;
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Slug { get; set; }
        public Guid CategoryId { get; set; }
        public Guid WarehouseGuid { get; set; }
        public string Origin { get; set; }
        public string Image { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceSale { get; set; }
        public string Parameter { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? Status { get; set; }
        public string CategoryName { get; set; }
        public string WarehouseName { get; set; }
        public string CategorySlug { get; set; }
    }
}
