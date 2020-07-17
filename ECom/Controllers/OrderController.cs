
using ECom.Services.cs;
using ECom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECom.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderTable(string UserName,int? PageNo)    
        {
            int PageSize = 5;
            OrderSearchViewModel model = new OrderSearchViewModel();
            model.UserName = UserName;
            model.PageNo = PageNo.HasValue? PageNo.Value>0? PageNo .Value: 1 :1;
            model.Users =OrderServices.Instance.GetUsers(UserName);
            model.Orders = OrderServices.Instance.SearchOrders(model.UserName, model.PageNo.Value,PageSize);
            var TotalOrders = OrderServices.Instance.GetOrdersCout(model.UserName);
            model.pager = new Pager(TotalOrders, model.PageNo.Value, PageSize);
            return PartialView("OrderTable", model);
        }
        public ActionResult Detail(int Id)
        {
            OrderDetailViewModel model = new OrderDetailViewModel();
            model.Order = OrderServices.Instance.GetOrderById(Id);
            model.User = OrderServices.Instance.GetUserByOrderId(Id);
            model.AvailableStatus = new List<string>() { "Pending", "In progress", "Delivered" };

            return View(model);
        }
        public JsonResult UpdateStatus(int Id, string Status)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data=new { Success = OrderServices.Instance.UpdateOrderStatus(Id, Status) };

            return result;
        }
         
    }
}