let dataTable;
let userId;
let userBooks;

$(document).ready(function () {
  $.ajax({
    type: 'GET',
    url: location.origin + "/Users/GetCurrentUserId",
    dataType: 'json',
    success: function (data) {
      userId = data.id;
      $.ajax({
        type: 'GET',
        url: location.origin + "/Books/GetBooksForUser",
        data: {
          id: userId
        },
        success: function (response) {
          userBooks = response;
          if (window.location.pathname === "/Books/MyBooks") {
            loadMyBooksTable();
          } else {
            loadDataTable();
          }
        }
      })
    }
  })
});

function loadDataTable() {
  dataTable = $('#DtBooks').DataTable({
    "ajax": {
      "url": "/Books/GetAll/",
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
          return `<div class="text-left">
                    <a href="/Books/Upsert?id=${data}" class='btn btn-success text-white elevation-2dp' style='cursor:pointer; width:70px;'>
                        Edit
                    </a>
                    &nbsp;
                    <a class='btn btn-danger text-white elevation-2dp' style='cursor:pointer; width:70px;'
                      onclick=deleteBook("/Books/Delete?id=${data}")>
                      Delete
                    </a>
                    &nbsp;
                    ${isBookAssignedToUser(data) ?
              `<button class='btn btn-secondary text-white elevation-2dp' style='cursor:pointer; width:170px;'
                      onclick="removeBookFromUser('${userId}','${data}')">
                      Remove from List
                    </button>`
              :
              `<button class='btn btn-primary text-white elevation-2dp' style='cursor:pointer; width:170px;'
                      onclick="addBookToUser('${userId}','${data}')">
                      Add to List
                    </button>`}
                </div>`;
        },
        "width": "33%",
        "orderable": false,
      }
    ],
    "language": {
      "emptyTable": "no data found"
    },
    "width": "100%",
    "pagingType": "simple",
    "bFilter": false,
    "initComplete": (() => {
      $(".dataTables_length select").selectpicker({
        width: '70px'
      })
    }),
  });
}

function loadMyBooksTable() {
  dataTable = $('#DtBooks').DataTable({
    "ajax": {
      "url": "/Books/GetBooksForUser/",
      "data": userId,
      "type": "GET",
      "datatype": "json"
    },
    "columns": [
      { "data": "name", "width": "25%" },
      { "data": "author", "width": "25%" },
      { "data": "isbn", "width": "25%" },
      {
        "data": "id",
        "render": function (data) {
          return `<div class="text-left">
                    <button class='btn btn-secondary text-white' style = 'cursor:pointer; width:170px;' onclick = "removeBookFromUser('${userId}','${data}')" >
                    Remove from List
                    </button>
                  </div>`
        }, "width": "25%"
      }
    ],
    "language": {
      "emptyTable": "no data found"
    },
    "width": "100%"
  });
}

function deleteBook(url) {
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
  $.post("/Books/AddBookToUser", { appUserId: _appUserId, bookId: _bookId }, function () {
    location.reload(true)
  });
}

function removeBookFromUser(_appUserId, _bookId) {
  $.ajax({
    url: "/Books/RemoveBookFromUser",
    type: 'DELETE',
    data: { appUserId: _appUserId, bookId: _bookId },
    success: function () {
      location.reload(true)
    }
  });
}

function isBookAssignedToUser(_bookId) {
  var booksInUser = userBooks.data.filter((b) => b.id === _bookId);
  return booksInUser.length > 0
}

function testButton() {
  return $.get("/Books/Test", "", (data) => console.log(data));
}