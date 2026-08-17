using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductMS.Data.Entities;
using ProductMS.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Data
{
    public partial class ProductMSContext : DbContext
    {
        public ProductMSContext()
        {
        }

        public ProductMSContext(DbContextOptions<ProductMSContext> options): base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263. 
            _ = optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ProductMS;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //for fluent API approach and also we can create a separate mapper for each entity
            #region Mapping
            _ = modelBuilder.ApplyConfiguration(new ProductMap());


            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}