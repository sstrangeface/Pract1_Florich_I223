using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yagnov_I212
{
    public interface IProduct
    {
        string Name { get; set; }
        decimal Price { get; set; }
    }
}