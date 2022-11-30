var tabladata;
var tabladata2;

$(document).ready(function () {
    /*Interactua con la vista y trae datos*/
    ////validamos el formulario
    $("#formNivel").validate({
        rules: {
            RefEje: "required",
            Nivel: "required",
            Nombre: "required",
            IdPerfil: "required",
            IdGeografia: "required"
        },
        messages: {
            RefEje: "(*)",
            Nivel: "(*)",
            Nombre: "(*)",
            IdPerfil: "(*)",
            IdGeografia: "(*)"
        },
        errorElement: 'span'
    });

    tabladata = $('#tbdata').DataTable({ /*genera un objeto tabledata con el id indicado*/
        "ajax": { //ajax es una interfaz que transforma los datos en formato json en este caso
            "url": $.MisUrls.url.Url_ObtenerEjePrincipal, //le damos la url que va ir al controlador
            "type": "GET",
            "datatype": "json"//le decimos el tipo
        },
        "columns": [ //le decimos qué columnas queremos mostrar
            { "data": "RefEje" },//lo que tenemos en ddbb
            { "data": "Nivel" },
            { "data": "Nombre" },
            { "data": "IdEjePadre" },
            { "data": "IdPerfil" },
            { "data": "IdGeografia" },
            {
                "data": "IdEje", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>" +
                        "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i></button>" +
                        "<button class='btn btn-primary btn-sm ml-2' type='button' onclick='muestraHijos(" + data + ")'><i class='fas fa-forward'></i></button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "90px"
            },
        ],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        }
    });
});

function abrirPopUpForm(json) {

    $("#txtid").val(0);

    if (json != null) {

        $("#txtid").val(json.IdEje);
        $("#txtrefeje").val(json.RefEje);
        $("#txtnivel").val(json.Nivel);
        $("#txtnombre").val(json.Nombre);
        $("#txtidejepadre").val(json.IdEjePadre);
        $("#txtidperfil").val(json.IdPerfil);
        $("#txtidgeografia").val(json.IdGeografia);

    } else {
        $("#txtrefeje").val("");
        $("#txtnivel").val("");
        $("#txtnombre").val("");
        $("#txtidejepadre").val("");
        $("#txtidperfil").val("");
        $("#txtidgeografia").val("");
    }

    $('#FormModal').modal('show');

}

function Guardar() {

    if ($("#formNivel").valid()) {

        var request = {
            objeto: {
                IdEje: $("#txtid").val(),
                RefEje: $("#txtrefeje").val(),
                Nivel: $("#txtnivel").val(),
                Nombre: $("#txtnombre").val(),
                IdEjePadre: $("#txtidejepadre").val(),
                IdPerfil: $("#txtidperfil").val(),
                IdGeografia: $("#txtidgeografia").val()
            }
        }

        jQuery.ajax({
            url: $.MisUrls.url.Url_GuardarEjePrincipal,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.resultado) {
                    tabladata.ajax.reload();
                    $('#FormModal').modal('hide');
                } else {

                    swal("Mensaje", "No se pudo guardar los cambios", "warning")
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {

            },
        });

    }
}



function eliminar($id) {
    swal({
        title: "Mensaje",
        text: "¿Desea eliminar el RefEje seleccionado?",
        type: "warning",
        showCancelButton: true,

        confirmButtonText: "Si",
        confirmButtonColor: "#DD6B55",

        cancelButtonText: "No",

        closeOnConfirm: true
    },

        function () {
            jQuery.ajax({
                url: $.MisUrls.url.Url_EliminarEjePrincipal + "?id=" + $id,
                type: "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    if (data.resultado) {
                        tabladata.ajax.reload();
                    } else {
                        swal("Mensaje", "No se pudo eliminar la EjePrincipal", "warning")
                    }
                },
                error: function (error) {
                    console.log(error)
                },
                beforeSend: function () {

                },
            });
        });
}

function muestraHijos($id) {

    tabladata2 = $('#tbdatahijos').DataTable({
        "ajax": {
            "url": $.MisUrls.url.Url_ObtenerHijosEjePrincipal + "?id=" + $id,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "RefEje" },
            { "data": "Nivel" },
            { "data": "Nombre" },
            { "data": "IdEjePadre" },
            { "data": "IdPerfil" },
            { "data": "IdGeografia" },
            {
                "data": "IdEje", "render": function (data, type, row, meta) {
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
}