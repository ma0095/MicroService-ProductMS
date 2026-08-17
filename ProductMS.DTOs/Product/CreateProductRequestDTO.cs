using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.DTOs.Products
{
    public class CreateProductRequestDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal FaceValue { get; set; }
        public int ActiveStatus { get; set; }
        public long? CreatedUserId { get; set; }
    }
}
