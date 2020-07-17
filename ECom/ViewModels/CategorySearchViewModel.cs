using ECom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECom.ViewModels
{
    public class CategorySearchViewModel
    {
        public int pageNo { get; set; }

        public List<ECom.Entities.Category> categories { get; set; }
        public string SearchTerm { get; set; }

        public Pager pager { get; set; }
    }
    public class EditCategoryViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageURL { get; set; }

        public bool isFeatured { get; set; }
    }
}