using ECom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECom.ViewModels
{
    public class HomeViewModel
    {
        public List<ECom.Entities.Category> Featurecategories { get; set; }
        public List<Product> products { get; set; }
    }
}