var tabladata;

$(document).ready(function () {
    ////validamos el formulario
    $("#formNivel").validate({
        rules: {
            Descripcion: "required",
            IdUnidad: "required",
            IdTipo: "required",
            IdPerfil: "required",
            RefIndicador: "required"
        },
        messages: {
            Descripcion: "(*)",
            IdUnidad: "(*)",
            IdTipo: "required",
            IdPerfil: "required",
            RefIndicador: "required"

        },
        errorElement: 'span'
    });


    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": $.MisUrls.url.Url_ObtenerIndicador,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Descripcion" },
            {
                "data": "oUnidad", render: function (data) {
                    return data.Tipo
                }
            },
            {
                "data": "oTipo", render: function (data) {
                    return data.Nombre
                }
            },
            {
                "data": "oPerfil", render: function (data) {
                    return data.RefPerfil
                }
                   
            },
            { "data": "RefIndicador" },
            {
                "data": "IdIndicador", "render": function (data, type, row, meta) {
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

        $("#txtid").val(json.IdIndicador);
        $("#txtdescripcion").val(json.Descripcion);
        //$("#txtunidad").val(json.Unidad);
        $.get("Indicador/ObtenerUnidades", function (data) {
            cargaComboSelecUnidades(json.oUnidad.IdUnidad, data, document.getElementById("cboUnidades"));
        });
        $.get("Indicador/ObtenerTipos", function (data) {
            cargaComboSelecTipos(json.oTipo.IdTipo, data, document.getElementById("cboTipos"));
        });
        $.get("Indicador/ObtenerPerfiles", function (data) {
            cargaComboSelecPerfiles(json.oPerfil.IdPerfil, data, document.getElementById("cboPerfiles"));
        });
        $("#txtrefindicador").val(json.Descripcion);


    } else {

        $("#txtdescripcion").val("");
        //$("#txtunidad").val("");
        $.get("Indicador/ObtenerUnidades", function (data) {
            cargaComboSelecUnidades("", data, document.getElementById("cboUnidades"));
        });
        $.get("Indicador/ObtenerTipos", function (data) {
            cargaComboSelecTipos("", data, document.getElementById("cboTipos"));
        });
        $.get("Indicador/ObtenerPerfiles", function (data) {
            cargaComboSelecPerfiles("", data, document.getElementById("cboPerfiles"));
        });
        $("#txtrefindicador").val("");
    }

    $('#FormModal').modal('show');

}

function Guardar() {

    if ($("#formNivel").valid()) {

        var request = {
            objeto: {
                IdIndicador: $("#txtid").val(),
                Descripcion: $("#txtdescripcion").val(),
               // Unidad: $("#txtunidad").val()
                oUnidad: {
                    IdUnidad: $("#cboUnidades").val()
                },
                oTipo: {
                    IdTipo: $("#cboTipos").val()
                },
                oPerfil: {
                    IdPerfil: $("#cboPerfiles").val()
                },
                RefIndicador: $("#txtrefindicador").val()

            }
        }

        jQuery.ajax({
            url: $.MisUrls.url.Url_GuardarIndicador,
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
        text: "¿Desea eliminar el rol seleccionado?",
        type: "warning",
        showCancelButton: true,

        confirmButtonText: "Si",
        confirmButtonColor: "#DD6B55",

        cancelButtonText: "No",

        closeOnConfirm: true
    },

        function () {
            jQuery.ajax({
                url: $.MisUrls.url.Url_EliminarIndicador + "?id=" + $id,
                type: "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    if (data.resultado) {
                        tabladata.ajax.reload();
                    } else {
                        swal("Mensaje", "No se pudo eliminar el rol", "warning")
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
//cargar cmbos

function cargaComboSelecUnidades(value, data, control) {
    var contenido = "";
    var nfilas = Object.keys(data).length;
    //cargar el combo
    for (var i = 0; i < nfilas; i++) {
        contenido += "<option value='" + data[i].IdUnidad + "'>"
        contenido += data[i].Tipo;
        contenido += "</option>"
    }
    control.innerHTML = contenido;
    control.value = value;
}
function cargaComboSelecTipos(value, data, control) {
    var contenido = "";
    var nfilas = Object.keys(data).length;
    //cargar el combo
    for (var i = 0; i < nfilas; i++) {
        contenido += "<option value='" + data[i].IdTipo + "'>"
        contenido += data[i].Nombre;
        contenido += "</option>"
    }
    control.innerHTML = contenido;
    control.value = value;
}
function cargaComboSelecPerfiles(value, data, control) {
    var contenido = "";
    var nfilas = Object.keys(data).length;
    //cargar el combo
    for (var i = 0; i < nfilas; i++) {
        contenido += "<option value='" + data[i].IdPerfil + "'>"
        contenido += data[i].Descripcion;
        contenido += "</option>"
    }
    control.innerHTML = contenido;
    control.value = value;
}