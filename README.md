# MVC5PopupModalApp
<h2>Murat Yücedağ hocama sevgiler :)</h2>
Tema ve Tablolar responsive DataTable ile yapıldı.<br>

  `Güncelleme,Silme,Detay,Ekleme` crud işlemleri için `popup-modal` kullanıldı.<br>
  `Listeleme` için `PartialView` yapısı kullanıldı.<br>
  
Frontend için görüntüleri aşağıdaki gibidir. <p></p>
  
![](https://i.hizliresim.com/r182duw.jpg)
![](https://i.hizliresim.com/r78vs8t.jpg)
![](https://i.hizliresim.com/5rfchmj.jpg)
![](https://i.hizliresim.com/8qgysvv.jpg)
![](https://i.hizliresim.com/fuyxc7o.jpg)
![](https://i.hizliresim.com/ory25xg.jpg) <p>
`author.js` ajax işlemleri ile id değerimizi post methodu ile controller'a iletiyoruz dönen değerleri foreach ile listeliyoruz. `$.each`
 </p>
 
![](https://i.hizliresim.com/a0hqbca.jpg)


 ```javascript
 
 ///////////////////////////////////////YAZARLAR//////////////////////////////////////////////
//YAZAR SİLME
$(document).on("click", ".AuthorDelete", function () {
  var id = $(this).attr("data-id");
  $("#AuthorID").val(id);
});

$("#AuthorDelete").click(function () {
  var id = $("#AuthorID").val();
  $.ajax({
    type: "POST",
    url: "/Author/AuthorDelete/",
    data: { "id": id },
    dataType: "json",
    success: function () {
      window.location.href = "/Author/Index/";
    }
  });
});
//YAZAR GÜNCELLEME
$(document).on("click", ".AuthorUpdate", function () {
  var id = $(this).attr("data-id");
  $("#AuthorID").val(id);
  $.ajax({
    type: "POST",
    url: "/Author/AuthorGetItem/" + id,
    dataType: "json",
    success: function (data) {
      $("#UpdateAuthorID").val(data.AuthorID);
      $("#UpdateAuthorName").val(data.AuthorName);
      $("#UpdateAuthorSurname").val(data.AuthorSurname);
      $("#UpdateAuthorDetail").val(data.AuthorDetail);
    }
  });
});
$("#AuthorUpdate").click(function () {
  var updatedata = $("#AuthorUpdateForm").serialize();
  $.ajax({
    type: "POST",
    url: "/Author/AuthorUpdate/",
    data: updatedata,
    dataType: "json",
    success: function () {
      window.location.href = "/Author/Index/";
    }
  });
});
//YAZAR EKLEME
$("#AuthorAdd").click(function () {
  var updatedata = $("#AuthorAddForm").serialize();
  $.ajax({
    type: "POST",
    url: "/Author/AuthorAdd/",
    data: updatedata,
    dataType: "json",
    success: function () {
      window.location.href = "/Author/Index/";
    }
  });
});
//YAZAR DETAY
$(document).on("click", ".AuthorDetail", function () {
  var id = $(this).attr("data-id");
  $.ajax({
    type: "POST",
    url: "/Author/AuthorGetItem/" + id,
    dataType: "json",
    success: function (data) {
      $("#DetailAuthorID").val(data.AuthorID);
      $("#DetailAuthorName").val(data.AuthorName);
      $("#DetailAuthorSurname").val(data.AuthorSurname);
      $("#DetailAuthorDetail").val(data.AuthorDetail);
    }
  });
});

$(document).on("click", ".Book", function () {
  var id = $(this).attr("data-id");
  $.ajax({
    type: "POST",
    url: "/Author/AuthorGetBook/" + id,
    dataType: "json",
    success: function (data) {
      if (data != 0) {
        $('#AuthorBook').empty();
        var numberid = 0;
        $.each(data, function (i, item) {
          var rows = "<tr>"
            + "<td>" + (++numberid) + "</td>"
            + "<td>" + item.BookName + "</td>"
            + "<td>" + item.BookAuthor + "</td>"
            + "<td>" + item.BookPublisher + "</td>"
            + "</tr>";
          $('#AuthorBook').append(rows);
        });
      }
      else {
        $('#AuthorBook').empty();
        var rows = "<tr>"
          + "<td colspan='4' style='text-align:center;'>" + " Yazara Ait Kitap Bulunamadı. " + "</td>"
          + "</tr>";
        $('#AuthorBook').append(rows);
      }
    }
  });
});

 
 ```
 <br>
View bölümünde stabil bootstrap modal yapımızı oluşturuyoruz ek olarak table body bölümüne  id = "istenilen_isim"  ekliyoruz ve işlemlerimizi bitiriyoruz. Index.cshtml bölümünde tüm kodlar mevcut.
<br>

![](https://i.hizliresim.com/ge6pc4g.jpg)

```javascript
<!-- BOOKS MODAL -->
<div class="modal fade" id="modal-book" style="display: none;" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header bg-dark">
                <h4 class="modal-title"><i class="fas fa-book"></i> Yazar Kitapları</h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                <table class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Kitap Adı</th>
                            <th>Yazar Adı Soyadı</th>
                            <th>Kitap Yayınevi</th>
                        </tr>
                    </thead>
                    <tbody id="AuthorBook">
                    </tbody>
                    <tfoot>
                        <tr>
                            <th>#</th>
                            <th>Kitap Adı</th>
                            <th>Yazar Adı Soyadı</th>
                            <th>Kitap Yayınevi</th>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <div class="modal-footer justify-content-between">
                <button type="button" class="btn btn-default" data-dismiss="modal">Vazgeç</button>
            </div>
        </div>
    </div>
</div>
```

<p>

Controller da ise Yazara ait kitapları getirmemiz için TBLBook tablosunda bulunan BookAuthor stünundaki değer ile bizim yolladığımız id değerini karşılaştırıyoruz eşit olan id'leri listeliyoruz.
</p>
  
![](https://i.hizliresim.com/448nmjv.jpg)


```javascript
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
```
