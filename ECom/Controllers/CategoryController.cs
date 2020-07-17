using ECom.Entities;
using ECom.Services.cs;
using ECom.ViewModels;
using System.Web.Mvc;

namespace ECom.Controllers
{
    public class CategoryController : Controller
    {
        //CategoriesService categoryService = new CategoriesService();
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid) {
                CategoriesService.Instence.SaveCategory(category);
                return PartialView("Index");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_Get(int ID)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();
            var category = CategoriesService.Instence.GetCategory(ID);
            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.isFeatured = category.isFeatured;
            return PartialView(model);
        }
        public ActionResult CategoryTable(string search, int? pageNo)
        {

            CategorySearchViewModel model = new CategorySearchViewModel();
            model.pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SearchTerm = search;
            var TotalItems = CategoriesService.Instence.GetCategoriesCount(search);
            model.categories = CategoriesService.Instence.GetCategories(model.SearchTerm, model.pageNo);
            if (model.categories != null)
            {
                model.pager = new Pager(TotalItems, pageNo, 2);
                return PartialView("CategoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }

        }
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(EditCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var existedCategory = CategoriesService.Instence.GetCategory(category.ID);
                existedCategory.ID = category.ID;
                existedCategory.Name = category.Name;
                existedCategory.Description = category.Description;
                existedCategory.isFeatured = category.isFeatured;
                if (category.ImageURL != null)
                {
                    existedCategory.ImageURL = category.ImageURL;
                }
                CategoriesService.Instence.UpdateCategory(existedCategory);
                return RedirectToAction("CategoryTable"); 
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var caregory = CategoriesService.Instence.GetCategory(ID);
            return View(caregory);
        }
        [HttpPost]
        public ActionResult Delete(ECom.Entities.Category category)
        {
            CategoriesService.Instence.DeleteCategory(category.ID);
            return PartialView("Index");
        }
    }
}