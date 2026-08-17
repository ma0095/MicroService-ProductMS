using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.DTOs.Products
{
    public class ProductDTO
    {
        public long id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal FaceValue { get; set; }
        public int ActiveStatus { get; set; }
        public long? CreatedUserId { get; set; }
        public long? EditedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
