using ECom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECom.ViewModels
{
    public class ProductSearchViewModel
    {   public int pageNo { get; set; }
        public List<Product> products { get; set; }
        public string SearchTerm { get; set; }
        public Pager pager { get; set; }
    }
    public class EditProductViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string ImageURL { get; set; }

        public List<ECom.Entities.Category> AvailableCategories { get; set; }
    }
}