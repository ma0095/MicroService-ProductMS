using ProductMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Data.Contracts
{
    public interface IProduct : IAuditable, IEntity
    {
        string Code { get; set; }
        string Name { get; set; }
        decimal FaceValue { get; set; }
        int ActiveStatus { get; set; }
    }
}
