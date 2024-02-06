
var dataTable;
$(document).ready(function () {
    loadDataTable();

})
function loadDataTable() {
    dataTable = $('#tbldata').dataTable({
        "ajax": {
            "url": "/Admin/CoverType/GetAll"
        },
        "columns": [
            { "data": "name", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                 <a href="/Admin/CoverType/Upsert/${data}" class="btn btn-info">
                                    <i class="fas fa-edit"></i>
                                 </a>
                                 <a class="btn btn-danger" onClick=Delete("/Admin/CoverType/Delete/${data}")>
                                    <i class="fas fa-trash-alt"></i>
                                 </a>
                            </div>`;
                }
            }
        ]
    })
}
function Delete(url) {
    swal({
        title: "Want to delete?",
        text: "Delete Information",
        button: true,
        icon: "warning",
        dangerMode: true // Corrected 'dangerModel' to 'dangerMode'
    }).then((willDelete) => {
        if (willDelete) { // Corrected 'WillDelete' to 'willDelete'
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}