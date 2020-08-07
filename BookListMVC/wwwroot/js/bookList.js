var dataTable;

$(document).ready(function () {
  loadDataTable();
});

let userId;
$.ajax({
  url: "Users/GetCurrentUserId",
  type: 'get',
  dataType: 'html',
  success: function (data) {
    userId = data;
  }
}).done(() => { console.log(userId) });


function loadDataTable() {
  dataTable = $('#DT_load').DataTable({
    "ajax": {
      "url": "/books/getall/",
      "type": "GET",
      "datatype": "json"
    },
    "columns": [
      { "data": "name", "width": "20%" },
      { "data": "author", "width": "20%" },
      { "data": "isbn", "width": "20%" },
      {
        "data": "id",
        "render": function (data) {
          return `<div class="text-center">
                    <a href="/Books/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                        Edit
                    </a>
                    &nbsp;
                    <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                      onclick=deleteBook('/books/Delete?id='+${data})>
                      Delete
                    </a>
                    <a class='btn btn-primary text-white' style='cursor:pointer;' onclick=addBookToUser()>
                      Add to List
                  </div>`;
        }, "width": "22%"
      }
    ],
    "language": {
      "emptyTable": "no data found"
    },
    "width": "100%"
  });
}

function deleteBook (url) {
  swal({
    title: "Are you sure?",
    text: "Once deleted, you will not be able to recover",
    icon: "warning",
    buttons: true,
    dangerMode: true
  }).then((willDelete) => {
    if (willDelete) {
      $.ajax({
        type: "DELETE",
        url: url,
        success: function (data) {
          if (data.success) {
            toastr.success(data.message);
            dataTable.ajax.reload();
          }
          else {
            toastr.error(data.message);
          }
        }
      });
    }
  });
}

function addBookToUser(_appUserId, _bookId) {
  $.post("/Books/AddBookToUser", { appUserId: _appUserId, bookId: _bookId });
}