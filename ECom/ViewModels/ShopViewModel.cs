using ECom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECom.ViewModels
{
    public class ShopViewModel
    {
        public string SearchTerm { get; set; }
        public List<Product> products { get; set; }
        public List<ECom.Entities.Category> FeaturedCategories { get; set; }
        public int? MaximumPrice { get; set; }
        //public int? MunimumPrice { get; set; }
        public int? categoryId { get; set; }
        public int? sortBy { get; set; }
        public int? pageNo { get; set; }
        public Pager pager { get; set; }
    }
    public class FilterProdutcdViewModel
    {
       
        public List<Product> products { get; set; }
        public Pager pager { get; set; }
        public int? sortBy { get; set; }
        public int? categoryId { get; set; }
        public string SearchTerm { get; set; }
        public int? pageNo { get; set; }
        


    }
}