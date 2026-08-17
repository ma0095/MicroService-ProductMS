using ProductMS.Data.Contracts;
using ProductMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Data.Entities
{
    public class Product : BaseEntity, IProduct
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal FaceValue { get; set; }
        public int ActiveStatus { get; set; }
        public long? CreatedUserId { get; set; }
        public long? EditedUserId { get; set; }
    }
}
