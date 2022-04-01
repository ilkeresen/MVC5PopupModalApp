using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult EmployeeList()
        {
            List<TBLEmployee> tBLEmployees = Context.TBLEmployee.Where(x => x.EmployeeStatus == true).ToList();
            return PartialView(tBLEmployees);
        }

        [HttpPost]
        public JsonResult EmployeeDelete(int id)
        {
            TBLEmployee tBLEmployee = Context.TBLEmployee.Where(x => x.EmployeeID == id).SingleOrDefault();

            if (tBLEmployee != null)
            {
                tBLEmployee.EmployeeStatus = false;
                Context.SaveChanges();
                TempData["item"] = tBLEmployee.EmployeeName + " " + tBLEmployee.EmployeeSurname;
                TempData["icon"] = "fa-trash-alt";
                TempData["message"] = "PERSONEL SİLİNDİ.";
                TempData["alert"] = "danger";
            }

            return Json(tBLEmployee, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EmployeeGetItem(int id)
        {
            TBLEmployee tBLEmployee = Context.TBLEmployee.Where(x => x.EmployeeID == id).FirstOrDefault();
            TBLEmployee NewtBLEmployee = new TBLEmployee();

            if (tBLEmployee != null)
            {
                NewtBLEmployee.EmployeeID = tBLEmployee.EmployeeID;
                NewtBLEmployee.EmployeeName = tBLEmployee.EmployeeName;
                NewtBLEmployee.EmployeeSurname = tBLEmployee.EmployeeSurname;
            }

            return Json(NewtBLEmployee, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EmployeeUpdate(TBLEmployee tBLEmployee)
        {
            TBLEmployee GettBLEmployee = Context.TBLEmployee.Find(tBLEmployee.EmployeeID);

            if (GettBLEmployee != null)
            {
                GettBLEmployee.EmployeeID = tBLEmployee.EmployeeID;
                GettBLEmployee.EmployeeName = tBLEmployee.EmployeeName;
                GettBLEmployee.EmployeeSurname = tBLEmployee.EmployeeSurname;
                Context.SaveChanges();
                TempData["item"] = tBLEmployee.EmployeeName + " " + tBLEmployee.EmployeeSurname;
                TempData["icon"] = "fa-edit";
                TempData["message"] = "PERSONEL GÜNCELLENDİ.";
                TempData["alert"] = "info";
            }

            return Json(GettBLEmployee, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EmployeeAdd(TBLEmployee tBLEmployee)
        {
            if (tBLEmployee != null)
            {
                tBLEmployee.EmployeeStatus = true;
                Context.TBLEmployee.Add(tBLEmployee);
                Context.SaveChanges();
                TempData["item"] = tBLEmployee.EmployeeName + " " + tBLEmployee.EmployeeSurname;
                TempData["icon"] = "fa-check";
                TempData["message"] = "PERSONEL EKLENDİ.";
                TempData["alert"] = "dark";
            }

            return Json(tBLEmployee, JsonRequestBehavior.AllowGet);
        }
    }
}