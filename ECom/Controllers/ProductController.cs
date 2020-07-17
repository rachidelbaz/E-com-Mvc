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
    public class ProductController : Controller
    {
        CategoriesService categoryService = new CategoriesService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        //ProductsServices ProductService = new ProductsServices();
      
        public ActionResult ProductTable(string search,int? pageNo)
        
        {
            var pageSize = 5;
            ProductSearchViewModel model = new ProductSearchViewModel();

            model.SearchTerm = search /*!=null? search:model.SearchTerm;*/;
            model.pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var TotalItems = ProductsServices.Instence.GetProductsCount(search);
    
           model.products = ProductsServices.Instence.GetProducts(model.SearchTerm, model.pageNo, pageSize);
            model.pager = new Pager(TotalItems, model.pageNo, pageSize);

            return PartialView("ProductTable",model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var Categoreis = categoryService.GetAllCategories();
            return PartialView(Categoreis);
        }
        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                CategoriesService categoriesService = new CategoriesService();

                var NewProduct = new Product();
                NewProduct.Name = model.Name;
                NewProduct.Description = model.Description;
                NewProduct.Price = model.Price;
                NewProduct.CategoryID = model.CategoryID;
                if (model.ImageUrl!=null)
                {
                    NewProduct.ImageURL = model.ImageUrl; 
                }
                NewProduct.category = categoriesService.GetCategory(model.CategoryID);

                ProductsServices.Instence.SaveProduct(NewProduct);
                return RedirectToAction("ProductTable"); 
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();

              var product = ProductsServices.Instence.GetProduct(ID);
            model.ID = product.ID;
            model.Name = product.Name;
            model.Price = product.Price;
            model.ImageURL = product.ImageURL;
            model.Description = product.Description;
            model.CategoryID = product.category.ID; /*!= null ? product.category.ID : 0;*/
            model.AvailableCategories = CategoriesService.Instence.GetAllCategories();
            return PartialView(model);

        }
        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {

            var existingProduct = ProductsServices.Instence.GetProduct(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;

            existingProduct.category = null; //mark it null. Because the referncy key is changed below
            existingProduct.CategoryID= model.CategoryID;

            //dont update imageURL if its empty
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingProduct.ImageURL = model.ImageURL;
            }

            ProductsServices.Instence.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }
      
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsServices.Instence.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }

        public ActionResult Details(int ID)
        {
            ProductDetailViewModel model =new ProductDetailViewModel();
            model.product=ProductsServices.Instence.GetProduct(ID);
            return View(model);
        }
    }
}