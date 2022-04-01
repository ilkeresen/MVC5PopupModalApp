using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult AuthorList()
        {
            List<TBLAuthor> tBLAuthor = Context.TBLAuthor.Where(x => x.AuthorStatus == true).ToList();
            return PartialView(tBLAuthor);
        }

        [HttpPost]
        public JsonResult AuthorGetBook(int id)
        {
            var areas = Context.TBLBook
            .Where(x => x.BookAuthor == id)
            .Select(y => new
            {
                BookID = y.BookID.ToString(),
                BookName = y.BookName,
                BookAuthor = y.TBLAuthor.AuthorName + " " + y.TBLAuthor.AuthorSurname,
                BookPublisher = y.BookPublisher
            });
            //var liste = Context.TBLBook.Where(x => x.BookAuthor == id).Count();
            return Json(areas, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AuthorDelete(int id)
        {
            TBLAuthor tBLAuthor = Context.TBLAuthor.Where(x => x.AuthorID == id).SingleOrDefault();

            if (tBLAuthor != null)
            {
                tBLAuthor.AuthorStatus = false;
                Context.SaveChanges();
                TempData["item"] = tBLAuthor.AuthorName;
                TempData["icon"] = "fa-trash-alt";
                TempData["message"] = "YAZAR SİLİNDİ.";
                TempData["alert"] = "danger";
            }

            return Json(tBLAuthor, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AuthorGetItem(int id)
        {
            TBLAuthor tBLAuthor = Context.TBLAuthor.Where(x => x.AuthorID == id).FirstOrDefault();
            TBLAuthor NewtBLAuthor = new TBLAuthor();

            if (tBLAuthor != null)
            {
                NewtBLAuthor.AuthorID = tBLAuthor.AuthorID;
                NewtBLAuthor.AuthorName = tBLAuthor.AuthorName;
                NewtBLAuthor.AuthorSurname = tBLAuthor.AuthorSurname;
                NewtBLAuthor.AuthorDetail = tBLAuthor.AuthorDetail;
            }

            return Json(NewtBLAuthor, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AuthorUpdate(TBLAuthor tBLAuthor)
        {
            TBLAuthor GettBLAuthor = Context.TBLAuthor.Find(tBLAuthor.AuthorID);

            if (GettBLAuthor != null)
            {
                GettBLAuthor.AuthorID = tBLAuthor.AuthorID;
                GettBLAuthor.AuthorName = tBLAuthor.AuthorName;
                GettBLAuthor.AuthorSurname = tBLAuthor.AuthorSurname;
                GettBLAuthor.AuthorDetail = tBLAuthor.AuthorDetail;
                Context.SaveChanges();
                TempData["item"] = tBLAuthor.AuthorName;
                TempData["icon"] = "fa-edit";
                TempData["message"] = "KATEGORİ GÜNCELLENDİ.";
                TempData["alert"] = "info";
            }

            return Json(GettBLAuthor, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AuthorAdd(TBLAuthor tBLAuthor)
        {
            if (tBLAuthor != null)
            {
                tBLAuthor.AuthorStatus = true;
                Context.TBLAuthor.Add(tBLAuthor);
                Context.SaveChanges();
                TempData["item"] = tBLAuthor.AuthorName + " " + tBLAuthor.AuthorSurname;
                TempData["icon"] = "fa-check";
                TempData["message"] = "YAZAR EKLENDİ.";
                TempData["alert"] = "dark";
            }

            return Json(tBLAuthor, JsonRequestBehavior.AllowGet);
        }
    }
}