using ECom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECom.ViewModels
{
    public class ProductsWidgetViewModel
    {
        public List<Product> products { get; set; }
        public bool isLatestProduct { get; set; }
    }
}