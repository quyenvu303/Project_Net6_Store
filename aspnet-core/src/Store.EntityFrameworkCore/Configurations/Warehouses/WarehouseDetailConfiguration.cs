using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Store.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Warehouses
{
    public class WarehouseDetailConfiguration : IEntityTypeConfiguration<WarehouseDetail>
    {
        public void Configure(EntityTypeBuilder<WarehouseDetail> builder)
        {
            builder.ToTable(StoreConsts.DbTablePrefix + "WarehouseDetails");
            builder.HasKey(x => new { x.WarehouseGuid, x.ProductGuid });

        }
    }
}
