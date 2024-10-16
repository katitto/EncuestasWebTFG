﻿var tabladata;

$(document).ready(function () {

    ////validamos el formulario
    $("#formNivel").validate({
        rules: {
            Descripcion: "required",
            Fecha_inicio: "required",
            Fecha_final: "required"
        },
        messages: {
            Descripcion: "(*)",
            Fecha_inicio: "required",
            Fecha_final: "required"
        },
        errorElement: 'span'
    });


    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": $.MisUrls.url.Url_ObtenerEncuesta,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Descripcion" },
            {
                "data": "Fecha_Inicio", render: function (data) {
                    return ObtenerFormatoFecha(data)
                }
            },

            {
                "data": "Fecha_Final", render: function (data) {
                    return ObtenerFormatoFecha(data)
                }
            },
            {
                "data": "Activo", "render": function (data) {
                    if (data) {
                        return '<span class="badge badge-success">Activo</span>'
                    } else {
                        return '<span class="badge badge-danger">No Activo</span>'
                    }
                }
            },
            {
                "data": "oUsuario", render: function (data) {
                    return data.Email
                }
            },
            {
                "data": "IdEncuesta", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>"
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

        $("#txtid").val(json.IdEncuesta);

        $("#txtdescripcion").val(json.Descripcion);
        $("#txtfecha_inicio").val(json.Fecha_Inicio);
        $("#txtfecha_final").val(json.Fecha_Final);


        var valor = 0;
        valor = json.Activo == true ? 1 : 0
        $("#cboEstado").val(valor);

    } else {
        $("#txtdescripcion").val("");
        $("#txtdescripcion").val("");
        $("#txtfecha_inicio").val("");
        $("#cboEstado").val(1);
    }

    $('#FormModal').modal('show');

}

function Guardar() {

    if ($("#formNivel").valid()) {

        var request = {
            objeto: {
                IdEncuesta: $("#txtid").val(),
                Descripcion: $("#txtdescripcion").val(),
                Fecha_Inicio: $("#txtfecha_inicio").val(),
                Fecha_Final: $("#txtfecha_final").val(),
                Activo: parseInt($("#cboEstado").val()) == 1 ? true : false
            }
        }

        jQuery.ajax({
            url: $.MisUrls.url.Url_GuardarEncuesta,
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


function ObtenerFormatoFecha(datetime) {

    var re = /-?\d+/;
    var m = re.exec(datetime);
    var d = new Date(parseInt(m[0]))


    var month = d.getMonth() + 1;
    var day = d.getDate();
    var output = (('' + day).length < 2 ? '0' : '') + day + '-' + (('' + month).length < 2 ? '0' : '') + month + '-' + d.getFullYear();

    return output;
}
