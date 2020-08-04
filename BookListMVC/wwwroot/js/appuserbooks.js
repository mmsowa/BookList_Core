var dataTable;

$(document).ready(function () {
  loadDataTable();
});

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
                    <a class='btn btn-danger text-white' style='cursor:pointer;'>
                      Remove
                    </a>
                  </div>`;
        }, "width": "10%"
      }
    ],
    "language": {
      "emptyTable": "no data found"
    },
    "width": "100%"
  });
}