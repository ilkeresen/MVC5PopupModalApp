using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    [Authorize(Roles = "A")]
    public class SettingsController : Controller
    {
        // GET: Settings
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult SettingsList()
        {
            var Admins = Context.TBLAdmin.ToList();
            return PartialView(Admins);
        }

        [HttpPost]
        public JsonResult AdminDelete(int id)
        {
            TBLAdmin tBLAdmin = Context.TBLAdmin.Where(x => x.AdminID == id).SingleOrDefault();

            if (tBLAdmin != null)
            {
                Context.TBLAdmin.Remove(tBLAdmin);
                Context.SaveChanges();
                TempData["item"] = tBLAdmin.AdminMail;
                TempData["icon"] = "fa-trash-alt";
                TempData["message"] = "YETKİLİ SİLİNDİ.";
                TempData["alert"] = "danger";
            }

            return Json(tBLAdmin, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AdminGetItem(int id)
        {
            TBLAdmin tBLAdmin = Context.TBLAdmin.Where(x => x.AdminID == id).FirstOrDefault();
            TBLAdmin NewtBLAdmin = new TBLAdmin();

            if (tBLAdmin != null)
            {
                NewtBLAdmin.AdminID = tBLAdmin.AdminID;
                NewtBLAdmin.AdminMail = tBLAdmin.AdminMail;
                NewtBLAdmin.AdminRol = tBLAdmin.AdminRol;
                NewtBLAdmin.AdminPassword = tBLAdmin.AdminPassword;
                NewtBLAdmin.AdminStatus = tBLAdmin.AdminStatus;
            }

            return Json(NewtBLAdmin, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AdminUpdate(TBLAdmin tBLAdmin)
        {
            TBLAdmin GettBLAdmin = Context.TBLAdmin.Find(tBLAdmin.AdminID);

            if (GettBLAdmin != null)
            {
                GettBLAdmin.AdminID = tBLAdmin.AdminID;
                GettBLAdmin.AdminMail = tBLAdmin.AdminMail;
                GettBLAdmin.AdminRol = tBLAdmin.AdminRol;
                GettBLAdmin.AdminPassword = tBLAdmin.AdminPassword;
                GettBLAdmin.AdminPhoto = tBLAdmin.AdminPhoto;
                GettBLAdmin.AdminStatus = tBLAdmin.AdminStatus;
                Context.SaveChanges();
                TempData["item"] = tBLAdmin.AdminMail;
                TempData["icon"] = "fa-edit";
                TempData["message"] = "YETKİLİ GÜNCELLENDİ.";
                TempData["alert"] = "info";
            }

            return Json(GettBLAdmin, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AdminAdd(TBLAdmin tBLAdmin)
        {
            if (tBLAdmin != null)
            {
                tBLAdmin.AdminStatus = true;
                Context.TBLAdmin.Add(tBLAdmin);
                Context.SaveChanges();
                TempData["item"] = tBLAdmin.AdminMail;
                TempData["icon"] = "fa-check";
                TempData["message"] = "YETKİLİ EKLENDİ.";
                TempData["alert"] = "dark";
            }

            return Json(tBLAdmin, JsonRequestBehavior.AllowGet);
        }
    }
}