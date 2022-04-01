using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult CategoryList()
        {
            List<TBLCategory> tBLCategories = Context.TBLCategory.Where(x => x.CategoryStatus == true).ToList();
            return PartialView(tBLCategories);
        }

        [HttpPost]
        public JsonResult CategoryDelete(int id)
        {
            TBLCategory tBLCategory = Context.TBLCategory.Where(x => x.CategoryID == id).SingleOrDefault();

            if (tBLCategory != null)
            {
                tBLCategory.CategoryStatus = false;
                Context.SaveChanges();
                TempData["item"] = tBLCategory.CategoryName;
                TempData["icon"] = "fa-trash-alt";
                TempData["message"] = "KATEGORİ SİLİNDİ.";
                TempData["alert"] = "danger";
            }

            return Json(tBLCategory, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CategoryGetItem(int id)
        {
            TBLCategory tBLCategory = Context.TBLCategory.Where(x => x.CategoryID == id).FirstOrDefault();
            TBLCategory NewtBLCategory = new TBLCategory();

            if (tBLCategory != null)
            {
                NewtBLCategory.CategoryID = tBLCategory.CategoryID;
                NewtBLCategory.CategoryName = tBLCategory.CategoryName;
                NewtBLCategory.CategoryDetail = tBLCategory.CategoryDetail;
            }

            return Json(NewtBLCategory, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CategoryUpdate(TBLCategory tBLCategory)
        {
            TBLCategory GettBLCategory = Context.TBLCategory.Find(tBLCategory.CategoryID);

            if (GettBLCategory != null)
            {
                GettBLCategory.CategoryID = tBLCategory.CategoryID;
                GettBLCategory.CategoryName = tBLCategory.CategoryName;
                GettBLCategory.CategoryDetail = tBLCategory.CategoryDetail;
                Context.SaveChanges();
                TempData["item"] = tBLCategory.CategoryName;
                TempData["icon"] = "fa-edit";
                TempData["message"] = "KATEGORİ GÜNCELLENDİ.";
                TempData["alert"] = "info";
            }

            return Json(GettBLCategory, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CategoryAdd(TBLCategory tBLCategory)
        {
            if (tBLCategory != null)
            {
                tBLCategory.CategoryStatus = true;
                Context.TBLCategory.Add(tBLCategory);
                Context.SaveChanges();
                TempData["item"] = tBLCategory.CategoryName;
                TempData["icon"] = "fa-check";
                TempData["message"] = "KATEGORİ EKLENDİ.";
                TempData["alert"] = "dark";
            }

            return Json(tBLCategory, JsonRequestBehavior.AllowGet);
        }
    }
}