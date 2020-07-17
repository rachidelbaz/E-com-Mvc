using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECom.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + file.FileName;
                var path = Path.Combine(Server.MapPath("~/content/images/"), fileName);
                file.SaveAs(path);
                result.Data = new { Success = true, ImageURL = string.Format("/content/images/{0}", fileName) };
            }
            catch (Exception ex)
            {

                result.Data = new { Success = false, Message = ex.Message };
            }

            return result;
        }
    }
}