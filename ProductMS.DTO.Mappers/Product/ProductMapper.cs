using ProductMS.Data.Contracts;
using ProductMS.DTOs.Products;
using ProductMS.Framework.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.DTO.Mappers.Product
{
    public class ProductMapper : APIDataMapper<IProduct, ProductDTO>
    {
        public ProductMapper(IServiceProvider Services) : base(Services)
        {
        }

        public override IProduct ToEntity(ProductDTO value)
        {
            IProduct entity = this.CreateEntity();
            entity.Id = value.id;
            entity.Code = value.Code;
            entity.Name = value.Name;
            entity.FaceValue = value.FaceValue;
            entity.ActiveStatus = value.ActiveStatus;
            entity.CreatedUserId = value.CreatedUserId;
            entity.EditedUserId = value.EditedUserId;
            entity.CreatedDate = value.CreatedDate;
            entity.EditedDate = value.EditedDate;
            return entity;
        }

        public override ProductDTO ToObject(IProduct? entity)
        {
            ProductDTO value = new ProductDTO();
            value.id = entity.Id;
            value.Code = entity.Code;
            value.Name = entity.Name;
            value.FaceValue = entity.FaceValue;
            value.ActiveStatus = entity.ActiveStatus;
            value.CreatedUserId = entity.CreatedUserId;
            value.EditedUserId = entity.EditedUserId;
            value.CreatedDate = entity.CreatedDate;
            value.EditedDate = entity.EditedDate;
            return value;
        }
    }
}
