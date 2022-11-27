var tabladata;

$(document).ready(function () {
    ////validamos el formulario
    $("#formNivel").validate({
        rules: {
            Nombre: "required",
            Descripcion: "required"
        },
        messages: {
            Nombre: "(*)",
            Descripcion: "(*)"
        },
        errorElement: 'span'
    });

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
                "data": "Activo", "render": function (data) {
                    if (data) {
                        return '<span class="badge badge-success">Activo</span>'
                    } else {
                        return '<span class="badge badge-danger">No Activo</span>'
                    }
                }
            },
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

    function abrirPopUpForm(json) {

        $("#txtid").val(0);

        if (json != null) {

            $("#txtid").val(json.IdRol);
            $("#txtnombre").val(json.Nombre);
            $("#txtdescripcion").val(json.Descripcion);
            var valor = 0;
            valor = json.Activo == true ? 1 : 0
            $("#cboEstado").val(valor);

        } else {
            $("#txtnombre").val("");
            $("#txtdescripcion").val("");
            $("#cboEstado").val(1);
        }

        $('#FormModal').modal('show');

    }

    function Guardar() {

        if ($("#formNivel").valid()) {

            var request = {
                objeto: {
                    IdRol: $("#txtid").val(),
                    Nombre: $("#txtnombre").val(),
                    Descripcion: $("#txtdescripcion").val(),
                    Activo: parseInt($("#cboEstado").val()) == 1 ? true : false
                }
            }

            jQuery.ajax({
                url: $.MisUrls.url.Url_GuardarRol,
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
                        url: $.MisUrls.url.Url_EliminarRol + "?id=" + $id,
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