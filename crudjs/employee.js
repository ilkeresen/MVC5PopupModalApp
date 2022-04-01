///////////////////////////////////////PERSONELLER//////////////////////////////////////////////
//PERSONEL SİLME
$(document).on("click", ".EmployeeDelete", function () {
  var id = $(this).attr("data-id");
  $("#EmployeeID").val(id);
});

$("#EmployeeDelete").click(function () {
  var id = $("#EmployeeID").val();
  $.ajax({
    type: "POST",
    url: "/Employee/EmployeeDelete/",
    data: { "id": id },
    dataType: "json",
    success: function () {
      window.location.href = "/Employee/Index/";
    }
  });
});
//PERSONEL GÜNCELLEME
$(document).on("click", ".EmployeeUpdate", function () {
  var id = $(this).attr("data-id");
  $("#EmployeeID").val(id);
  $.ajax({
    type: "POST",
    url: "/Employee/EmployeeGetItem/" + id,
    dataType: "json",
    success: function (data) {
      $("#UpdateEmployeeID").val(data.EmployeeID);
      $("#UpdateEmployeeName").val(data.EmployeeName);
      $("#UpdateEmployeeSurname").val(data.EmployeeSurname);
    }
  });
});
$("#EmployeeUpdate").click(function () {
  var updatedata = $("#EmployeeUpdateForm").serialize();
  $.ajax({
    type: "POST",
    url: "/Employee/EmployeeUpdate/",
    data: updatedata,
    dataType: "json",
    success: function () {
      window.location.href = "/Employee/Index/";
    }
  });
});
//PERSONEL EKLEME
$("#EmployeeAdd").click(function () {
  var updatedata = $("#EmployeeAddForm").serialize();
  $.ajax({
    type: "POST",
    url: "/Employee/EmployeeAdd/",
    data: updatedata,
    dataType: "json",
    success: function () {
      window.location.href = "/Employee/Index/";
    }
  });
});
