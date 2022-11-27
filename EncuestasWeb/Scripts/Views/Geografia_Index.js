var tabladata;

$(document).ready(function () {
    ////validamos el formulario
    $("#formNivel").validate({
        rules: {
            Pais: "required",
            CoordenadasX: "required",
            CoordenadasY: "required"
        },
        messages: {
            Pais: "(*)",
            CoordenadasX: "(*)",
            CoordenadasY: "(*)"
        },
        errorElement: 'span'
    });

    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": $.MisUrls.url.Url_ObtenerGeografia,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Pais" },
            { "data": "CoordenadasX" },
            { "data": "CoordenadasY" },
            {
                "data": "IdGeografia", "render": function (data, type, row, meta) {
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

function abrirPopUpForm(json) {

    $("#txtid").val(0);

    if (json != null) {

        $("#txtid").val(json.IdGeografia);
        $("#txtpais").val(json.Pais);
        $("#txtcoordenadasx").val(json.CoordenadasX);
        $("#txtcoordenadasy").val(json.CoordenadasY);

    } else {
        $("#txtpais").val("");
        $("#txtcoordenadasx").val("");
        $("#txtcoordenadasy").val("");
    }

    $('#FormModal').modal('show');

}

function Guardar() {

    if ($("#formNivel").valid()) {

        var request = {
            objeto: {
                IdGeografia: $("#txtid").val(),
                Pais: $("#txtpais").val(),
                CoordenadasX: $("#txtcoordenadasx").val(),
                CoordenadasY: $("#txtcoordenadasy").val()
            }
        }

        jQuery.ajax({
            url: $.MisUrls.url.Url_GuardarGeografia,
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
        text: "¿Desea eliminar el pais seleccionado?",
        type: "warning",
        showCancelButton: true,

        confirmButtonText: "Si",
        confirmButtonColor: "#DD6B55",

        cancelButtonText: "No",

        closeOnConfirm: true
    },

        function () {
            jQuery.ajax({
                url: $.MisUrls.url.Url_EliminarGeografia + "?id=" + $id,
                type: "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    if (data.resultado) {
                        tabladata.ajax.reload();
                    } else {
                        swal("Mensaje", "No se pudo eliminar la geografia", "warning")
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