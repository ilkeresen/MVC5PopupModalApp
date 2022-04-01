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
