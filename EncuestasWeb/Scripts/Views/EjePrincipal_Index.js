var tabladata;
var tabladataEncuesta;

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
    $.get("EjePrincipal/Obtener", function (data) {
        var ArrayPadres = [];
        var ArrayHijos = [];
        var ArrayTodosOrdenado = [];
        var nfilas = Object.keys(data).length;
        for (var i = 0; i < nfilas; i++) {
            let ejePrincipalData = JSON.stringify(data[i]);
            let ejePrincipal = JSON.parse(ejePrincipalData);
            if (ejePrincipal.IdEjePadre == 0) {
                ArrayPadres.push(ejePrincipal);
            } else {
                ArrayHijos.push(ejePrincipal);
            }
        }

        for (var j = 0; j < ArrayPadres.length; j++) {
            ArrayTodosOrdenado.push(ArrayPadres[j]);
            for (var k = 0; k < ArrayHijos.length; k++) {

                if (ArrayHijos[k].IdEjePadre == ArrayPadres[j].IdEje) {
                    ArrayTodosOrdenado.push(ArrayHijos[k]);
                }
            }

        }
    tabladata = $('#tbdata').DataTable({ /*genera un objeto tabledata con el id indicado*/
        "data": ArrayTodosOrdenado, //le damos la url que va ir al controlador
        "datatype": "json",//le decimos el tipo
        "columns": [ //le decimos qué columnas queremos mostrar
            { "data": "RefEje" },//lo que tenemos en ddbb
            { "data": "Nivel" },
            { "data": "Nombre" },
            { "data": "IdEjePadre" },//debe ir oculto
            {
                "data": "oPerfil", render: function (data) {
                    return data.Descripcion
                }               
            },
            
            {
                "data": "oGeografia", render: function (data) {
                    return data.Pais
                }
            },
            {
                "data": "IdEje", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>" +
                        "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i></button>"
                        + "<button class='btn btn-info btn-sm ml-2' type='button' onclick='buscarEncuestas(" + data + ")'><i class='fas fa-list'></i></button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "100px"
            },
        ],
        "columnDefs": [
            {
                "targets": 3,
                "visible": false
            }
        ],
        "sorting": false,
        "rowCallback": function (row, data) {
            if (data.IdEjePadre == 0) {
                $(row).addClass('padre');
                $(row).css('background-color', '#99ff9c');
                $(row).css('font-weight', 'bold');

            } else {

                $(row).addClass('cat1');
                $(row).css('font-size', '15px');

            }
        },
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        }
        });
    });
});

function abrirPopUpForm(json) {

    $("#txtid").val(0);

    if (json != null) {

        $("#txtid").val(json.IdEje);
        $("#txtrefeje").val(json.RefEje);
        $("#txtnivel").val(json.Nivel);
        $("#txtnombre").val(json.Nombre);
        //$("#txtidejepadre").val(json.IdEjePadre);
        //$("#txtidperfil").val(json.IdPerfil);
        //$("#txtidgeografia").val(json.IdGeografia);
        $.get("EjePrincipal/ObtenerPadres", function (data) {
            cargaComboSelecPadres(json.IdEjePadre, data, document.getElementById("cboPadres"));
        });

        $.get("EjePrincipal/ObtenerPerfiles", function (data) {
            cargaComboSelecPerfil(json.oPerfil.IdPerfil, data, document.getElementById("cboPerfiles"));
        });
        $.get("EjePrincipal/ObtenerGeografias", function (data) {
            cargaComboSelecGeografia(json.oGeografia.IdGeografia, data, document.getElementById("cboGeografias"));
        });

    } else {
        $("#txtrefeje").val("");
        $("#txtnivel").val("");
        $("#txtnombre").val("");
        //$("#txtidejepadre").val("");
       // $("#txtidperfil").val("");
        //$("#txtidgeografia").val("");
        $.get("EjePrincipal/ObtenerPadres", function (data) {
            cargaComboSelecPadres("", data, document.getElementById("cboPadres"));
        });
        $.get("EjePrincipal/ObtenerPerfiles", function (data) {
            cargaComboSelecPerfil("", data, document.getElementById("cboPerfiles"));
        });
        $.get("EjePrincipal/ObtenerGeografias", function (data) {
            cargaComboSelecGeografia("", data, document.getElementById("cboGeografias"));
        });
    }

    $('#FormModal').modal('show');

}

function Guardar() {

    if ($("#formNivel").valid()) {

        //Objeto Geografia
        var request = {           
            objeto: {
                IdEje: $("#txtid").val(),
                RefEje: $("#txtrefeje").val(),
                Nivel: $("#txtnivel").val(),
                Nombre: $("#txtnombre").val(),
                IdEjePadre: $("#cboPadres").val(),
                oPerfil : {
                    IdPerfil: $("#cboPerfiles").val() 
                },
                oGeografia : {
                    IdGeografia: $("#cboGeografias").val()
                }
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
                    $('#FormModal').modal('hide');
                    refrescaTabla();
                    
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
        text: "¿Desea eliminar el La organización seleccionada?",
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
                        $('#FormModal').modal('hide');
                        refrescaTabla();
                    } else {
                        swal("Mensaje", "No se pudo eliminar la Organización", "warning")
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
function cargaComboSelecPerfil(value, data, control) {
    var contenido = "";
    var nfilas = Object.keys(data).length;
    //cargar el combo
    for (var i = 0; i < nfilas; i++) {
        contenido += "<option value='" + data[i].IdPerfil +"'>"
        contenido += data[i].Descripcion;
        contenido += "</option>"
    }
    control.innerHTML = contenido;
    control.value = value;
}
function cargaComboSelecGeografia(value, data, control) {
    var contenido = "";
    var nfilas = Object.keys(data).length;
    //cargar el combo
    for (var i = 0; i < nfilas; i++) {
        contenido += "<option value='" + data[i].IdGeografia + "'>"
        contenido += data[i].Pais;
        contenido += "</option>"
    }
    control.innerHTML = contenido;
    control.value = value;
}
function cargaComboSelecPadres(value, data, control) {
    var contenido = "";
    var nfilas = Object.keys(data).length;
    contenido += "<option value='0'>TOP";
    //cargar el combo
    for (var i = 0; i < nfilas; i++) {
        contenido += "<option value='" + data[i].IdEje + "'>"
        contenido += data[i].Nombre;
        contenido += "</option>"
    }
    control.innerHTML = contenido;
    control.value = value;
}
function refrescaTabla() {
    $.get("EjePrincipal/Obtener", function (data) {
        var ArrayPadres = [];
        var ArrayHijos = [];
        var ArrayTodosOrdenado = [];
        var nfilas = Object.keys(data).length;
        for (var i = 0; i < nfilas; i++) {
            let ejePrincipalData = JSON.stringify(data[i]);
            let ejePrincipal = JSON.parse(ejePrincipalData);
            if (ejePrincipal.IdEjePadre == 0) {
                ArrayPadres.push(ejePrincipal);
            } else {
                ArrayHijos.push(ejePrincipal);
            }
        }

        for (var j = 0; j < ArrayPadres.length; j++) {
            ArrayTodosOrdenado.push(ArrayPadres[j]);
            for (var k = 0; k < ArrayHijos.length; k++) {

                if (ArrayHijos[k].IdEjePadre == ArrayPadres[j].IdEje) {
                    ArrayTodosOrdenado.push(ArrayHijos[k]);
                }
            }

        }
        $("#tbdata").dataTable().fnDestroy();
        tabladata = $('#tbdata').DataTable({ /*genera un objeto tabledata con el id indicado*/
            "data": ArrayTodosOrdenado, //le damos la url que va ir al controlador
            "datatype": "json",//le decimos el tipo
            "columns": [ //le decimos qué columnas queremos mostrar
                { "data": "RefEje" },//lo que tenemos en ddbb
                { "data": "Nivel" },
                { "data": "Nombre" },
                { "data": "IdEjePadre" },//debe ir oculto
                {
                    "data": "oPerfil", render: function (data) {
                        return data.Descripcion
                    }
                },

                {
                    "data": "oGeografia", render: function (data) {
                        return data.Pais
                    }
                },
                {
                    "data": "IdEje", "render": function (data, type, row, meta) {
                        return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>" +
                            "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i></button>"
                            + "<button class='btn btn-info btn-sm ml-2' type='button' onclick='buscarEncuestas(" + data + ")'><i class='fas fa-list'></i></button>"
                            
                    },
                    "orderable": false,
                    "searchable": false,
                    "width": "90px"
                },
            ],
            "columnDefs": [
                {
                    "targets": 3,
                    "visible": false
                }
            ],
            "sorting": false,
            "rowCallback": function (row, data) {
                if (data.IdEjePadre == 0) {
                    $(row).addClass('padre');
                    $(row).css('background-color', '#99ff9c');
                    $(row).css('font-weight', 'bold');

                } else {

                    $(row).addClass('cat1');
                    $(row).css('font-size', '15px');

                }
            },
            "language": {
                "url": $.MisUrls.url.Url_datatable_spanish
            }
        });
        $("#tbdata").dataTable().clear();
        tabladata.ajax.reload();
    });

}

function buscarEncuestas($id) {
//buscamos encuestas desplegadas por id primero hacemos un controlador de leng que nos lance la query 
//si es = 0 No Camapañas desplegadas en esta entidad
//si no abre pop up o nueva pestaña mejor no?
    $.get("EjePrincipal/ObtenerEncuestas/?id=" + $id, function (data) {
        var nfilas = Object.keys(data).length;
        if (nfilas == 0) {
            swal({
                title: "Mensaje",
                text: "No existen Encuestas desplegadas en esta Entidad",
                type: "warning"
                
            });

        }
        else {           

            abrirPopUpFormEncuestas($id);
            

        }

    });

}

function abrirPopUpFormEncuestas($id) {
    //debería mostrar Tabla de encuestas:

    $.get("EjePrincipal/ObtenerEncuestasTabla/?id=" + $id, function (data) {
        var ArrayTodosOrdenado = [];
        var nfilas = Object.keys(data).length;
        for (var i = 0; i < nfilas; i++) {
            let todosData = JSON.stringify(data[i]);
            let todos = JSON.parse(todosData);
            ArrayTodosOrdenado.push(todos);
        }
        $("#tbdataencuestas").dataTable().fnDestroy();

        tabladataEncuesta = $('#tbdataencuestas').DataTable({ /*genera un objeto tabledata con el id indicado*/
            "data": ArrayTodosOrdenado, //le damos la url que va ir al controlador
            "datatype": "json",//le decimos el tipo
            "columns": [ //le decimos qué columnas queremos mostrar

                { "data": "DescripcionEncuesta" },//lo que tenemos en ddbb
                { "data": "DescripcionIndicador" },
                { "data": "Respuesta" },
                { "data": "IdEje" },
                {
                    "data": "IdData", "render": function (data, type, row, meta) {
                        return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpFormPreguntas(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>"
                            
                    },
                    "orderable": false,
                    "searchable": false,
                    "width": "90px"
                },

            ],
            "columnDefs": [
                {
                    "targets": 3,
                    "visible": false
                }
            ],

            "language": {
                "url": $.MisUrls.url.Url_datatable_spanish
            }
        });
  


    });

    $('#FormModalEncuestas').modal('show');

}


function abrirPopUpFormPreguntas(json) {

    $("#txtid").val(0);

    if (json != null) {
        $("#txtideje").val(json.IdEje);
        $("#txtid").val(json.IdData);
        $("#txtdescripcionencuesta").val(json.DescripcionEncuesta);
        $("#txtdescripcionindicador").val(json.DescripcionIndicador);
        $("#txtrespuesta").val(json.Respuesta);
       


    } else {
        $("#txtideje").val("");
        $("#txtid").val("");
        $("#txtdescripcionencuesta").val("");
        $("#txtdescripcionindicador").val("");
        $("#txtrespuesta").val("");
    }

    $('#FormModalPreguntas').modal('show');

}

function GuardarRespuesta() {

    if ($("#formNivelPreguntas").valid()) {

        //Objeto Geografia
        var request = {
            objeto: {
                IdData: $("#txtid").val(),
                Respuesta: $("#txtrespuesta").val(),              
            }
        }

        jQuery.ajax({
            url: $.MisUrls.url.Url_GuardarRespuesta,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.resultado) {

                    $('#FormModal').modal('hide');
                    swal({
                        title: "Mensaje",
                        text: "Guardado Correctamente",
                        type: "success"

                    });

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

function refrescaTablaEncuestas() {
    var idEje = document.getElementById("txtideje").value;
    abrirPopUpFormEncuestas(idEje);

}
