using ECom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECom.ViewModels
{
    public class ChekOutViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> ProductsID { get; set; }
        public User user { get; set; }
    }
}