﻿var tabladata;

$(document).ready(function () {
    ////validamos el formulario
    $("#formNivel").validate({
        rules: {
            Nombre: "required",
            Apellido: "required",
            User: "required",
            Contrasena: "required",
            Email: "required",
            IdEje: "required",
            IdRol: "required"
        },
        messages: {
            Nombre: "(*)",
            Apellido: "(*)",
            User: "(*)",
            Contrasena: "(*)",
            Email: "(*)",
            IdEje: "(*)",
            IdRol: "(*)"
        },
        errorElement: 'span'
    });


    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": $.MisUrls.url.Url_ObtenerUsuarios,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Nombre" },
            { "data": "Apellido" },
            { "data": "User" },
            { "data": "Contrasena" },
            { "data": "Email" },
            { "data": "IdEje" },
            { "data": "IdRol" },
            {
                "data": "IdUsuario", "render": function (data, type, row, meta) {
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

        $("#txtid").val(json.IdUsuario);
        $("#txtnombre").val(json.Nombre);
        $("#txtapellido").val(json.Apellido);
        $("#txtuser").val(json.User);
        $("#txtcontrasena").val(json.Contrasena);
        $("#txtemail").val(json.Email);
        $("#txtideje").val(json.IdEje);
        $("#txtidrol").val(json.IdRol);


    } else {

        $("#txtnombre").val("");
        $("#txtapellido").val("");
        $("#txtuser").val("");
        $("#txtcontrasena").val("");
        $("#txtemail").val("");
        $("#txtideje").val("");
        $("#txtidrol").val("");
    }

    $('#FormModal').modal('show');

}

function Guardar() {

    if ($("#formNivel").valid()) {

        var request = {
            objeto: {
                IdUsuario: $("#txtid").val(),
                Nombre: $("#txtnombre").val(),
                Apellido: $("#txtapellido").val(),
                User: $("#txtuser").val(),
                Contrasena: $("#txtcontrasena").val(),
                Email: $("#txtemail").val(),
                IdEje: $("#txtideje").val(),
                IdRol: $("#txtidrol").val()
            }
        }

        jQuery.ajax({
            url: $.MisUrls.url.Url_GuardarUsuario,
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
                url: $.MisUrls.url.Url_EliminarUsuario + "?id=" + $id,
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