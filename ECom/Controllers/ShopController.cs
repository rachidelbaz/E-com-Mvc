using ECom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECom.Services.cs;
using ECom.Entities;


namespace ECom.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index(string SearchTerm, int? MaximumPrice, int? MunimumPrice, int? CategoryID, int? SortBy, int? pageNo)
        {
            ShopViewModel model = new ShopViewModel();
            model.MaximumPrice = ProductsServices.Instence.GetMaximunPrice();
            model.SearchTerm = SearchTerm;
            model.categoryId = CategoryID;
            model.sortBy = SortBy;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.pageNo = pageNo;
            int TotalProducts = ProductsServices.Instence.GetProductsCount(SearchTerm, MaximumPrice, MunimumPrice, CategoryID, SortBy);

            model.FeaturedCategories = CategoriesService.Instence.GetFeaturedCategories();
            model.products = ProductsServices.Instence.GetProducts(SearchTerm, MaximumPrice, MunimumPrice, CategoryID, SortBy, pageNo.Value, 10);

            model.pager = new Pager(TotalProducts, pageNo);

            return View(model);
        }
        public ActionResult FilterProducts(string SearchTerm, int? MaximumPrice, int? MunimumPrice, int? CategoryID, int? SortBy, int? pageNo)
        {
            FilterProdutcdViewModel model = new FilterProdutcdViewModel();
            model.SearchTerm = SearchTerm;
            model.categoryId = CategoryID;
            model.sortBy = SortBy;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.pageNo = pageNo;
            int TotalProducts = ProductsServices.Instence.GetProductsCount(SearchTerm, MaximumPrice, MunimumPrice, CategoryID, SortBy);

            model.products = ProductsServices.Instence.GetProducts(SearchTerm, MaximumPrice, MunimumPrice, CategoryID, SortBy, pageNo.Value, 10);

            model.pager = new Pager(TotalProducts, pageNo);

            return PartialView(model);
        }
        public JsonResult Placeorder(string productsId)
        {
            Session["UserID"] = "1";
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(productsId) && Session["UserID"] != null)
            {
                //dsiplay like 4-4-5-2-2
                List<int> PsId = productsId.Split('-').Select(x => int.Parse(x)).ToList();
                List<Product> productsBought = ProductsServices.Instence.GetAllProduct(PsId.Distinct().ToList());

                Order newOrder = new Order();
                newOrder.OrderedAt = DateTime.Now;
                newOrder.Status = "pending";

                newOrder.UserId = int.Parse(Session["UserID"].ToString());


                newOrder.TotalAmount = productsBought.Sum(x => x.Price * PsId.Where(y => y == x.ID).Count());

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(productsBought.Select(x => new OrderItem() { ProductID = x.ID, Quantity = PsId.Where(y => y == x.ID).Count() }));

                int Roweffected = ShopServices.Instence.SaveOrder(newOrder);
                result.Data = new { Success = true, rows = Roweffected };
            }
            else
            {
                result.Data = new { Success = false };
            }

            return result;
        }
    }
}