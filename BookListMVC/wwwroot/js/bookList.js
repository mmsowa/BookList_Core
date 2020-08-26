let dataTable;
let userId;
let userBooks;

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
                      onclick=deleteBook("/Books/Delete?id=${data}")>
                      Delete
                    </a>
                    ${isBookAssignedToUser(data) ?
                    `<button class='btn btn-secondary text-white' style='cursor:pointer;'
                            onclick="removeBookFromUser('${userId}','${data}')">
                            Remove from List
                          </button>
                        </div>`:
                    `<button class='btn btn-primary text-white' style='cursor:pointer;'
                            onclick="addBookToUser('${userId}','${data}')">
                            Add to List
                          </button>
                        </div>`
            }`;
        }, "width": "30%"
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
  $.post("/Books/AddBookToUser", { appUserId: _appUserId, bookId: _bookId }, location.reload());
}

function removeBookToUser(_appUserId, _bookId) {
  $.post("/Books/RemoveBookFromUser", { appUserId: _appUserId, bookId: _bookId });
}


function isBookAssignedToUser(_bookId) {
  var booksInUser = userBooks.data.filter((b) => b.id === _bookId);
  return booksInUser.length > 0
}

$(document).ready(function () {
  $.ajax({
    type: 'GET',
    url: "Users/GetCurrentUserId",
    dataType: 'json',
    async: false,
    success: function (data) {
      userId = data.userId;
    }
  })

  $.ajax({
    type: 'GET',
    url: "Books/GetBooksForUser",
    data: {
      id: userId
    },
    async: false,
    success: function (response) {
      userBooks = response;
    }
  }).done(function () {
    console.log(userBooks);
    loadDataTable()
  })
});
