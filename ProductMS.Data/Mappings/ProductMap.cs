using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Data.Mappings
{
    internal class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            _ = builder.ToTable("Products");
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Code).IsRequired().HasMaxLength(20);
            _ = builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            _ = builder.Property(x => x.FaceValue).HasPrecision(15, 3);
            _ = builder.Property(x => x.ActiveStatus).IsRequired();
            _ = builder.Property(x => x.CreatedUserId);
            _ = builder.Property(x => x.EditedUserId);
            _ = builder.Property(x => x.CreatedDate);
            _ = builder.Property(x => x.EditedDate);
        }
    }
}
