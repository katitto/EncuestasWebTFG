var tabladata;

$(document).ready(function () {
    ////validamos el formulario
    //$.get("InformeMaps/Obtener", function (data) {

    //    var ArrayTodos = [];
    //    ////5. LA LONGITUD DE DATA ES DECIR LOS ELEMENTOS DE LA MISMA
    //    var nfilas = Object.keys(data).length;

    //    ////6., RECORREMOS EL DATAJSON Y LO TRANSFORMAMOS CON FUNCIONES PROPIAS DE JSON
    //    for (var i = 0; i < nfilas; i++) {
    //        //    //6.CADA ELEMENTO DATA EN UNA VARIABLE DATA JS
    //        let TodosData = JSON.stringify(data[i]);
    //        //    //7. CONVERTIMOS DE JSON A VAR CON PROPIEDADES - ES UN OBJETO
    //        let Todos = JSON.parse(TodosData);

    //        //    //8. SI EL PADRE ES = CERO QUIERE DECIR QUE ES PADRE LLENAMOS ARRAY PADRES E HIJOS
  
    //            ArrayTodos.push(Todos);

    //    }
    //});
    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": $.MisUrls.url.Url_ObtenerInformeMaps,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Nombre" },
            {
                "data": "oGeografia", render: function (data) {
                    return data.CoordenadasX
                }
            },
            {
                "data": "oGeografia", render: function (data) {
                    return data.CoordenadasY
                }
            }
        ],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        }
    });
});

