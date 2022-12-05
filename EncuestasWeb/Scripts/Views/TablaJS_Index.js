var tabladata;
/*1. OBETENMOS LOS DATOS DEL CONTROLADOS, HE CORREGIDO NULOS*/

$(document).ready(function () {
    /*Interactua con la vista y trae datos*/
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
    //pasamos a Json el array ordenado 

    $.get("Geografia/ObtenerTablaNested", function (data) {
            jQuery.ajax({
                url: $.MisUrls.url.Url_ObtenerGeografia
            });
        //2. TENEMOS DATA QUE EL JSON DEL CONTROLADOR EN ESE FORMATO
        //3. VAMOS A TRANSFORMAR ESOS DATOS EN VARIABLES QUE PUEDA LEER EL JS, ADEMÁS QUIERO QUE APAREZCAS LOS HIJOS EN OTRA TABLA
        //4. CREAMOS VARIABLE 2 ARRAY PARA PADRES E HIJOS
        var ArrayPadres = [];
        var ArrayHijos = [];
        var ArrayTodosOrdenado = [];
        ////5. LA LONGITUD DE DATA ES DECIR LOS ELEMENTOS DE LA MISMA
        var nfilas = Object.keys(data).length;

        ////6., RECORREMOS EL DATAJSON Y LO TRANSFORMAMOS CON FUNCIONES PROPIAS DE JSON
        for (var i = 0; i < nfilas; i++) {
            //    //6.CADA ELEMENTO DATA EN UNA VARIABLE DATA JS
            let geografiaData = JSON.stringify(data[i]);
            //    //7. CONVERTIMOS DE JSON A VAR CON PROPIEDADES - ES UN OBJETO
            let geografia = JSON.parse(geografiaData);

            //    //8. SI EL PADRE ES = CERO QUIERE DECIR QUE ES PADRE LLENAMOS ARRAY PADRES E HIJOS
            if (geografia.Padre == 0) {
                ArrayPadres.push(geografia);
            } else {
                ArrayHijos.push(geografia);
            }
        }

        for (var j = 0; j < ArrayPadres.length; j++) {
            ArrayTodosOrdenado.push(ArrayPadres[j]);
            for (var k = 0; k < ArrayHijos.length; k++) {

                if (ArrayHijos[k].Padre == ArrayPadres[j].IdGeografia) {
                    ArrayTodosOrdenado.push(ArrayHijos[k]);
                }
            }

        }

        tabladata = $('#tbdata').DataTable({
            "data": ArrayTodosOrdenado,
            "datatype": "json",
            "columns": [ //le decimos qué columnas queremos mostrar
                { "data": "Pais" },//lo que tenemos en ddbb
                { "data": "CoordenadasX" },
                { "data": "CoordenadasY" },
                { "data": "Padre" },
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
            "sorting": false,
            "rowCallback": function (row, data) {
                if (data.Padre == 0) {
                    $(row).addClass('padre');
                    $(row).css('background-color', '#99ff9c');
                    $(row).css('font-weight', 'bold');

                } else {
                    //$(row).hide();
                    //$(row).css('display', 'none');
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

        $("#txtid").val(json.IdGeografia);
        $("#txtpais").val(json.Pais);
        $("#txtcoordenadasx").val(json.CoordenadasX);
        $("#txtcoordenadasy").val(json.CoordenadasY);
        $("#txtpadre").val(json.Padre);

    } else {
        $("#txtpais").val("");
        $("#txtcoordenadasx").val("");
        $("#txtcoordenadasy").val("");
        $("#txtpadre").val("");
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
                CoordenadasY: $("#txtcoordenadasy").val(),
                Padre: $("#txtpadre").val()
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
                    $('#FormModal').modal('hide');
                    $.get("Geografia/ObtenerTablaNested", function (data) {
                        //2. TENEMOS DATA QUE EL JSON DEL CONTROLADOR EN ESE FORMATO
                        //3. VAMOS A TRANSFORMAR ESOS DATOS EN VARIABLES QUE PUEDA LEER EL JS, ADEMÁS QUIERO QUE APAREZCAS LOS HIJOS EN OTRA TABLA
                        //4. CREAMOS VARIABLE 2 ARRAY PARA PADRES E HIJOS
                        var ArrayPadres = [];
                        var ArrayHijos = [];
                        var ArrayTodosOrdenado = [];
                        ////5. LA LONGITUD DE DATA ES DECIR LOS ELEMENTOS DE LA MISMA
                        var nfilas = Object.keys(data).length;

                        ////6., RECORREMOS EL DATAJSON Y LO TRANSFORMAMOS CON FUNCIONES PROPIAS DE JSON
                        for (var i = 0; i < nfilas; i++) {
                            //    //6.CADA ELEMENTO DATA EN UNA VARIABLE DATA JS
                            let geografiaData = JSON.stringify(data[i]);
                            //    //7. CONVERTIMOS DE JSON A VAR CON PROPIEDADES - ES UN OBJETO
                            let geografia = JSON.parse(geografiaData);

                            //    //8. SI EL PADRE ES = CERO QUIERE DECIR QUE ES PADRE LLENAMOS ARRAY PADRES E HIJOS
                            if (geografia.Padre == 0) {
                                ArrayPadres.push(geografia);
                            } else {
                                ArrayHijos.push(geografia);
                            }
                        }

                        for (var j = 0; j < ArrayPadres.length; j++) {
                            ArrayTodosOrdenado.push(ArrayPadres[j]);
                            for (var k = 0; k < ArrayHijos.length; k++) {

                                if (ArrayHijos[k].Padre == ArrayPadres[j].IdGeografia) {
                                    ArrayTodosOrdenado.push(ArrayHijos[k]);
                                }
                            }

                        }
                        $("#tbdata").dataTable().fnDestroy();
                        
                        tabladata = $('#tbdata').DataTable({
                            "data": ArrayTodosOrdenado,
                            "datatype": "json",
                            "columns": [ //le decimos qué columnas queremos mostrar
                                { "data": "Pais" },//lo que tenemos en ddbb
                                { "data": "CoordenadasX" },
                                { "data": "CoordenadasY" },
                                { "data": "Padre" },
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
                            "sorting": false,
                            "rowCallback": function (row, data) {
                                if (data.Padre == 0) {
                                    $(row).addClass('padre');
                                    $(row).css('background-color', '#99ff9c');
                                    $(row).css('font-weight', 'bold');

                                } else {
                                    //$(row).hide();
                                    //$(row).css('display', 'none');
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
                       
                        $('#FormModal').modal('hide');
                        $.get("Geografia/ObtenerTablaNested", function (data) {
                            //2. TENEMOS DATA QUE EL JSON DEL CONTROLADOR EN ESE FORMATO
                            //3. VAMOS A TRANSFORMAR ESOS DATOS EN VARIABLES QUE PUEDA LEER EL JS, ADEMÁS QUIERO QUE APAREZCAS LOS HIJOS EN OTRA TABLA
                            //4. CREAMOS VARIABLE 2 ARRAY PARA PADRES E HIJOS
                            var ArrayPadres = [];
                            var ArrayHijos = [];
                            var ArrayTodosOrdenado = [];
                            ////5. LA LONGITUD DE DATA ES DECIR LOS ELEMENTOS DE LA MISMA
                            var nfilas = Object.keys(data).length;

                            ////6., RECORREMOS EL DATAJSON Y LO TRANSFORMAMOS CON FUNCIONES PROPIAS DE JSON
                            for (var i = 0; i < nfilas; i++) {
                                //    //6.CADA ELEMENTO DATA EN UNA VARIABLE DATA JS
                                let geografiaData = JSON.stringify(data[i]);
                                //    //7. CONVERTIMOS DE JSON A VAR CON PROPIEDADES - ES UN OBJETO
                                let geografia = JSON.parse(geografiaData);

                                //    //8. SI EL PADRE ES = CERO QUIERE DECIR QUE ES PADRE LLENAMOS ARRAY PADRES E HIJOS
                                if (geografia.Padre == 0) {
                                    ArrayPadres.push(geografia);
                                } else {
                                    ArrayHijos.push(geografia);
                                }
                            }

                            for (var j = 0; j < ArrayPadres.length; j++) {
                                ArrayTodosOrdenado.push(ArrayPadres[j]);
                                for (var k = 0; k < ArrayHijos.length; k++) {

                                    if (ArrayHijos[k].Padre == ArrayPadres[j].IdGeografia) {
                                        ArrayTodosOrdenado.push(ArrayHijos[k]);
                                    }
                                }

                            }
                            $("#tbdata").dataTable().fnDestroy();

                            tabladata = $('#tbdata').DataTable({
                                "data": ArrayTodosOrdenado,
                                "datatype": "json",
                                "columns": [ //le decimos qué columnas queremos mostrar
                                    { "data": "Pais" },//lo que tenemos en ddbb
                                    { "data": "CoordenadasX" },
                                    { "data": "CoordenadasY" },
                                    { "data": "Padre" },
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
                                "sorting": false,
                                "rowCallback": function (row, data) {
                                    if (data.Padre == 0) {
                                        $(row).addClass('padre');
                                        $(row).css('background-color', '#99ff9c');
                                        $(row).css('font-weight', 'bold');

                                    } else {
                                        //$(row).hide();
                                        //$(row).css('display', 'none');
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

//function muestraHijos($id) {

//    tabladata2 = $('#tbdatahijos').DataTable({
//        "ajax": {
//            "url": $.MisUrls.url.Url_ObtenerHijosGeografia + "?id=" + $id,
//            "type": "GET",
//            "datatype": "json"
//        },
//        "columns": [
//            { "data": "Pais" },
//            { "data": "CoordenadasX" },
//            { "data": "CoordenadasY" },
//            { "data": "Padre" },
//            {
//                "data": "IdGeografia", "render": function (data, type, row, meta) {
//                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>" +
//                        "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i></button>"
//                },
//                "orderable": false,
//                "searchable": false,
//                "width": "90px"
//            }
//        ],
//        "language": {
//            "url": $.MisUrls.url.Url_datatable_spanish
//        }
//    });





//}


