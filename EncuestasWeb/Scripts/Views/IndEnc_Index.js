var tabladata;

$(document).ready(function () {


    //OBTENER ENCUESTAS
    jQuery.ajax({
        url: $.MisUrls.url.Url_ObtenerEncuesta,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $("#cboencuestas").html("");

            $("<option>").attr({ "value": 0 }).text("-- Seleccione --").appendTo("#cboencuestas");
            if (data.data != null)
                $.each(data.data, function (i, item) {

                    if (item.Activo == true) {
                        $("<option>").attr({ "value": item.IdEncuesta }).text(item.Descripcion).appendTo("#cboencuestas");
                    }
                })
        },
        error: function (error) {
            console.log(error)
        },
        beforeSend: function () {
        },
    });

    //cargar datos a cero
    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": $.MisUrls.url.Url_ObtenerPreguntasfiltradas + "?idencuesta=0",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Descripcion" },
            {
                "data": "Tipo"
            },
            {
                "data": "Nombre"
            },
            {
                "data": "RefPerfil"
            },
            { "data": "RefIndicador" },
            {
                "data": "IdIndicador", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ", " + $("#cboencuestas").val()+")'><i class='fa fa-trash'></i></button>"
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


function buscarPreguntas() {

    if ($("#cboelecciones").val() == 0) {
        
        swal("Mensaje", "Seleccione una Encuesta", "warning")
        return;
    }
    tabladata.ajax.url($.MisUrls.url.Url_ObtenerPreguntasfiltradas + "?idencuesta=" + $("#cboencuestas").val()).load();

}
function abrirPopUpForm(json) {
    CargaPreguntasCombo();
    $("#txtid").val(0);

    if (json != null) {

        $("#txtid").val(json.IdCandidato);

        $("#txtnombres").val(json.NombresCompleto);
        $("#txtmensaje").val(json.Mensaje);

        var valor = 0;
        valor = json.Activo == true ? 1 : 0
        $("#cboEstado").val(valor);

    } else {
        $("#txtnombres").val("");
        $("#txtmensaje").val("");

        $("#cboEstado").val(1);
    }

    $('#FormModal').modal('show');

}

//obtener preguntas para el combo  falta filtro para preguntas
function CargaPreguntasCombo() {

jQuery.ajax({
    url: $.MisUrls.url.Url_ObtenerPreguntas,
    type: "GET",
    dataType: "json",
    contentType: "application/json; charset=utf-8",
    success: function (data) {

        $("#cbopreguntas").html("");

        $("<option>").attr({ "value": 0 }).text("-- Seleccione --").appendTo("#cbopreguntas");
        if (data.data != null)
            $.each(data.data, function (i, item) {

                $("<option>").attr({ "value": item.IdIndicador }).text(item.Descripcion).appendTo("#cbopreguntas");

            })
    },
    error: function (error) {
        console.log(error)
    },
    beforeSend: function () {
    },
});
}

function Guardar() {

    var request = {
        objeto: {
            IdIndicador: $("#cbopreguntas").val(),
            IdEncuesta: $("#cboencuestas").val()
        }

    }
 

        jQuery.ajax({
            url: $.MisUrls.url.Url_GuardarIndEnc,
            type: "POST",
            data: JSON.stringify(request),
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
/*revisar para eliminar*/
function eliminar($IdIndicador, $IdEncuesta) {

    $IdEncuesta = $("#cboencuestas").val();
    swal({
        title: "Mensaje",
        text: "¿Desea eliminar la pregunta seleccionada?",
        type: "warning",
        showCancelButton: true,

        confirmButtonText: "Si",
        confirmButtonColor: "#DD6B55",

        cancelButtonText: "No",

        closeOnConfirm: true
    },


        function () {
            jQuery.ajax({
                url: $.MisUrls.url.Url_EliminarIndEnc + "?IdIndicador=" + $IdIndicador + "&IdEncuesta=" + $IdEncuesta,
                type: "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    if (data.resultado) {
                        tabladata.ajax.reload();
                    } else {
                        swal("Mensaje", "No se pudo eliminar la pregunta", "warning")
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
