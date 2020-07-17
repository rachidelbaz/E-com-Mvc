 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Entities
{
    public class Product:BaseEntity
    {
        public int CategoryID { get; set; }
        public decimal Price { get; set; }

        public virtual Category category { get; set; }
    }
}
