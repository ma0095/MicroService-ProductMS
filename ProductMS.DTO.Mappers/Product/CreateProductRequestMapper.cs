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
    public class CreateProductRequestMapper : APIDataMapper<IProduct, CreateProductRequestDTO>
    {
        public CreateProductRequestMapper(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        public override IProduct ToEntity(CreateProductRequestDTO value)
        {
            IProduct entity = this.CreateEntity();
            entity.Code = value.Code;
            entity.Name = value.Name;
            entity.FaceValue = value.FaceValue;
            entity.ActiveStatus = value.ActiveStatus;
            entity.CreatedUserId = value.CreatedUserId;
            return entity;
        }

        public override CreateProductRequestDTO ToObject(IProduct? entity)
        {
            CreateProductRequestDTO value = new CreateProductRequestDTO();
            value.Code = entity.Code;
            value.Name = entity.Name;
            value.FaceValue = entity.FaceValue;
            value.ActiveStatus = entity.ActiveStatus;
            value.CreatedUserId = entity.CreatedUserId;
            return value;
        }
    }
}
