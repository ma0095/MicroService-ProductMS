using Microsoft.EntityFrameworkCore;
using ProductMS.Data.Contracts;
using ProductMS.Data.Service.Contracts;
using ProductMS.Framework.Data;
using ProductMS.Framework.Data.Services;
using ProductMS.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Data.Service
{
    public class ProductDataService : BaseDataService, IProductDataService
    {
        private readonly IRepository<IProduct> _productRepo;

        public ProductDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _productRepo = unitOfWork.Repository<IProduct>();

        }

        public async Task<ActionStatus<IProduct>> CreateProduct(IProduct requestdata)
        {
            try
            {
                IProduct data = _productRepo.Add(requestdata);
                int count = await UnitOfWork.CommitAsync();
                if (count > 0)
                {
                    return new ActionStatus<IProduct>(true, data);
                }
                return new ActionStatus<IProduct>(new ResponseVM("DPC0001"));
            }
            catch (Exception ex)
            {
                return new ActionStatus<IProduct>("DPC-CreatePoduct", ex);
            }
        }
        public async Task<ActionStatus<IProduct>> GetProductById(long id)
        {
            try
            {
                IProduct data = await _productRepo.Entities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (data != null)
                {
                    return new ActionStatus<IProduct>(true, data);
                }
                return new ActionStatus<IProduct>(new ResponseVM("DPG0002"));
            }
            catch (Exception ex)
            {
                return new ActionStatus<IProduct>("DPC-GetProductById", ex);
            }
        }
        public async Task<ActionStatus<IProduct>> EditProduct(IProduct entity)
        {
            try
            {
                IProduct data = await _productRepo.Entities.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (data != null)
                {
                    data.Code = entity.Code;
                    data.Name = entity.Name;
                    data.FaceValue = entity.FaceValue;
                    data.ActiveStatus = entity.ActiveStatus;
                    data.EditedUserId = entity.EditedUserId;
                    _productRepo.Update(data);
                    int count = await UnitOfWork.CommitAsync();
                    if (count > 0)
                    {
                        return new ActionStatus<IProduct>(true, data);
                    }
                    return new ActionStatus<IProduct>(new ResponseVM("DPE0001"));
                }
                return new ActionStatus<IProduct>((ActionStatus)data);
            }
            catch (Exception ex)
            {
                return new ActionStatus<IProduct>("DPC-EditProduct", ex);
            }
        }
    }
}
