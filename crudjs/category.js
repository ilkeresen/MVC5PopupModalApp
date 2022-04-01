///////////////////////////////////////KATEGORİLER//////////////////////////////////////////////
//KATEGORİ SİLME
$(document).on("click", ".CategoryDelete", function () {
  var id = $(this).attr("data-id");
  $("#CategoryID").val(id);
});

$("#CategoryDelete").click(function () {
  var id = $("#CategoryID").val();
  $.ajax({
    type: "POST",
    url: "/Category/CategoryDelete/",
    data: { "id": id },
    dataType: "json",
    success: function () {
      window.location.href = "/Category/Index/";
    }
  });
});
//KATEGORİ GÜNCELLEME
$(document).on("click", ".CategoryUpdate", function () {
  var id = $(this).attr("data-id");
  $("#CategoryID").val(id);
  $.ajax({
    type: "POST",
    url: "/Category/CategoryGetItem/" + id,
    dataType: "json",
    success: function (data) {
      $("#UpdateCategoryID").val(data.CategoryID);
      $("#UpdateCategoryName").val(data.CategoryName);
      $("#UpdateCategoryDetail").val(data.CategoryDetail);
    }
  });
});
$("#CategoryUpdate").click(function () {
  var updatedata = $("#CategoryUpdateForm").serialize();
  $.ajax({
    type: "POST",
    url: "/Category/CategoryUpdate/",
    data: updatedata,
    dataType: "json",
    success: function () {
      window.location.href = "/Category/Index/";
    }
  });
});
//KATEGORİ EKLEME
$("#CategoryAdd").click(function () {
  var updatedata = $("#CategoryAddForm").serialize();
  $.ajax({
    type: "POST",
    url: "/Category/CategoryAdd/",
    data: updatedata,
    dataType: "json",
    success: function () {
      window.location.href = "/Category/Index/";
    }
  });
});
//KATEGORİ DETAY
$(document).on("click", ".CategoryDetail", function () {
  var id = $(this).attr("data-id");
  $.ajax({
    type: "POST",
    url: "/Category/CategoryGetItem/" + id,
    dataType: "json",
    success: function (data) {
      $("#DetailCategoryID").val(data.CategoryID);
      $("#DetailCategoryName").val(data.CategoryName);
      $("#DetailCategoryDetail").val(data.CategoryDetail);
    }
  });
});
