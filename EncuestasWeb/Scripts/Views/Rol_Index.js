var tabladata;

$(document).ready(function () {
    ////activarMenu("roles");

    ////validamos el formulario
    //$("#formNivel").validate({
    //    rules: {
    //        nombre: "required",
    //        descripcion: "required"

    //    },
    //    messages: {
    //        nombre: "(*)",
    //        descripcion: "(*)"
    //    },
    //    errorElement: 'span'
    //});


    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": $.MisUrls.url.Url_ObtenerRoles,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Nombre" },
            { "data": "Descripcion" },
            {
                "data": "IdRol", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>" +
                        "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i></button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "90px"
            }
        ],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        }
    });

});