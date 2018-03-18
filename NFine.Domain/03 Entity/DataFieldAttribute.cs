using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity
{
    public class DataFieldAttribute : Attribute
    {
        public string DescName { get; set; }
    }
}
