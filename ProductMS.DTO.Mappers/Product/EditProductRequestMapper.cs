using ProductMS.Data.Contracts;
using ProductMS.DTOs.Product;
using ProductMS.DTOs.Products;
using ProductMS.Framework.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.DTO.Mappers.Product
{
    internal class EditProductRequestMapper : APIDataMapper<IProduct, EditProductRequestDTO>
    {
        public EditProductRequestMapper(IServiceProvider Services) : base(Services)
        {
        }

        public override IProduct ToEntity(EditProductRequestDTO value)
        {
            IProduct entity = this.CreateEntity();
            entity.Id = value.id;
            entity.Code = value.Code;
            entity.Name = value.Name;
            entity.FaceValue = value.FaceValue;
            entity.ActiveStatus = value.ActiveStatus;
            entity.EditedUserId = value.EditedUserId;
            return entity;
        }

        public override EditProductRequestDTO ToObject(IProduct? entity)
        {
            EditProductRequestDTO value = new EditProductRequestDTO();
            value.id = entity.Id;
            value.Code = entity.Code;
            value.Name = entity.Name;
            value.FaceValue = entity.FaceValue;
            value.ActiveStatus = entity.ActiveStatus;
            value.EditedUserId = entity.EditedUserId;
            return value;
        }
    }
}
