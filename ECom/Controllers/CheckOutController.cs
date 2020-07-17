using ECom.Entities;
using ECom.Services.cs;
using ECom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECom.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        //ProductsServices productsservices = new ProductsServices();
        public ActionResult Index()
        {
            ChekOutViewModel model = new ChekOutViewModel();
            var ProductsCart = Request.Cookies["CartProducts"];
            if(ProductsCart !=null && !string.IsNullOrEmpty(ProductsCart.Value))
            {
                var ProductAddedToCart = ProductsCart.Value.Split('-');
                //convert list of sting to list of int ids products
                model.ProductsID = ProductAddedToCart.Select(x => int.Parse(x)).ToList();

                model.CartProducts= ProductsServices.Instence.GetProducts(model.ProductsID);
                if(Session["User"] != null)
                {
                    //model.user = TempData["User"] as User;
                    model.user = Session["User"] as User;
                }
                else
                {
                    model.user = new User();
                }
                
                
            }
            return View("Index", model);
        }
    }
}