///////////////////////////////////////YETKİLİLER//////////////////////////////////////////////
//YETKİLİ SİLME
$(document).on("click", ".SettingsDelete", function () {
  var id = $(this).attr("data-id");
  $("#AdminID").val(id);
});

$("#AdminDelete").click(function () {
  var id = $("#AdminID").val();
  $.ajax({
    type: "POST",
    url: "/Settings/AdminDelete/" + id,
    dataType: "json",
    success: function () {
      window.location.href = "/Settings/Index/";
    }
  });
});
//YETKİLİ GÜNCELLEME
$(document).on("click", ".SettingsEdit", function () {
  var id = $(this).attr("data-id");
  $("#AdminID").val(id);
  $.ajax({
    type: "POST",
    url: "/Settings/AdminGetItem/" + id,
    dataType: "json",
    success: function (data) {
      $("#UpdateAdminID").val(data.AdminID);
      $("#UpdateAdminMail").val(data.AdminMail);
      $("#UpdateAdminRol").val(data.AdminRol);
      $("#UpdateAdminPassword").val(data.AdminPassword);
      $("#UpdateAdminStatus").val(data.AdminStatus);
    }
  });
});
$("#AdminUpdate").click(function () {
  var updatedata = $("#AdminUpdateForm").serialize();
  $.ajax({
    type: "POST",
    url: "/Settings/AdminUpdate/",
    data: updatedata,
    dataType: "json",
    success: function () {
      window.location.href = "/Settings/Index/";
    }
  });
});
//YETKİLİ EKLEME
$("#AdminAdd").click(function () {
  var updatedata = $("#AdminAddForm").serialize();
  $.ajax({
    type: "POST",
    url: "/Settings/AdminAdd/",
    data: updatedata,
    dataType: "json",
    success: function () {
      window.location.href = "/Settings/Index/";
    }
  });
});
