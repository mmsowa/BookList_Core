let allBooksDataTable;
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
                    loadDataTable();
                }
            })
        }
    })
});

function loadDataTable() {
    allBooksDataTable = $('#DtBooks').DataTable({
        "ajax": {
            "url": "/Books/GetAll/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {"data": "name", "width": "20%"},
            {"data": "author", "width": "15%"},
            {"data": "isbn", "width": "10%"},
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-left">
                        ${isBookAssignedToUser(data) ?
                        `<button class='btn btn-secondary text-white elevation-2dp' style='cursor:pointer; width:185px;' onclick="removeBookFromUser('${userId}','${data}')">
                            Zurückgeben
                        </button>
                        <button class='btn btn-primary text-white elevation-2dp' style='cursor:pointer; width:185px;' onclick="addBookToUser('${userId}','${data}', 'ExtendBook')">
                            Verlängern
                        </button>` :
                        `<button class='btn btn-primary text-white elevation-2dp' style='cursor:pointer; width:185px;' onclick="addBookToUser('${userId}','${data}', 'AddBookToUser')">
                            Ausleihen
                        </button>`
                        }
                    </div>`;
                },
                "width": "30%",
                "orderable": false,
            }
        ],
        "language": {
            "emptyTable": "Buch nicht gefunden"
        },
        "width": "100%",
        "pagingType": "simple",
        "bFilter": true,
        "bLengthChange": false,
        "colReorder": true,
        "stateSave": true
    });
}

function addBookToUser(_appUserId, _bookId, _url) {
    $.post(`/Books/${_url}`, {appUserId: _appUserId, bookId: _bookId}, function () {
        location.reload();
    });
}

function removeBookFromUser(_appUserId, _bookId) {
    $.ajax({
        url: "/Books/RemoveBookFromUser",
        type: 'DELETE',
        data: {appUserId: _appUserId, bookId: _bookId},
        success: function () {
            location.reload();
        }
    });
}

function isBookAssignedToUser(_bookId) {
    const booksInUser = userBooks.data.filter((b) => b.id === _bookId);
    return booksInUser.length > 0
}