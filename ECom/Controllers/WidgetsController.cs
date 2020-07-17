using ECom.Services.cs;
using ECom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECom.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isLatestProduct, int? CategoryID = 0)
        {
            ProductsWidgetViewModel model = new ProductsWidgetViewModel();
            model.isLatestProduct = isLatestProduct;
            if (isLatestProduct)
            {
                model.products = ProductsServices.Instence.GetLatestProducts(4);
            }else if(CategoryID.HasValue && CategoryID > 0)
            {
                model.products = ProductsServices.Instence.GetRelatedProducts(CategoryID, 4);
            }
            else
            {
                model.products = ProductsServices.Instence.GetProducts(1,8);
            }
            return PartialView(model);
        }
    }
}