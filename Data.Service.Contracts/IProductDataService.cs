using ProductMS.Data.Contracts;
using ProductMS.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Data.Service.Contracts
{
    public interface IProductDataService
    {
        Task<ActionStatus<IProduct>> CreateProduct(IProduct result);
        Task<ActionStatus<IProduct>> EditProduct(IProduct result);
        Task<ActionStatus<IProduct>> GetProductById(long id);
    }
}
