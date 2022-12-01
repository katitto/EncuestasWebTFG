
$(document).ready(function () {
    var contenido = "";
    var nombre = " hola katito"
    var contenido = "<h1>"+ nombre+ "</h1>";
    document.getElementById("divTabla").innerHTML = contenido;

    /*crearLista(data);*/
});
/*1. OBETENMOS LOS DATOS DEL CONTROLADOS, HE CORREGIDO NULOS*/

$.get("Geografia/ObtenerTablaNested", function (data) {
    //2. TENEMOS DATA QUE EL JSON DEL CONTROLADOR EN ESE FORMATO
    //3. VAMOS A TRANSFORMAR ESOS DATOS EN VARIABLES QUE PUEDA LEER EL JS, ADEMÁS QUIERO QUE APAREZCAS LOS HIJOS EN OTRA TABLA
    //4. CREAMOS VARIABLE 2 ARRAY PARA PADRES E HIJOS
    var ArrayPadres = [];
    var ArrayHijos = [];
    //5. LA LONGITUD DE DATA ES DECIR LOS ELEMENTOS DE LA MISMA
    var nfilas = Object.keys(data).length;

    //6., RECORREMOS EL DATAJSON Y LO TRANSFORMAMOS CON FUNCIONES PROPIAS DE JSON
    for (var i = 0; i < nfilas; i++) {
        //6.CADA ELEMENTO DATA EN UNA VARIABLE DATA JS
        let geografiaData = JSON.stringify(data[i]);
        //7. CONVERTIMOS DE JSON A VAR CON PROPIEDADES - ES UN OBJETO
        let geografia = JSON.parse(geografiaData);

        //8. SI EL PADRE ES = CERO QUIERE DECIR QUE ES PADRE LLENAMOS ARRAY PADRES E HIJOS
        if (geografia.Padre == 0) {
            ArrayPadres.push(geografia);

        } else {
            ArrayHijos.push(geografia);
        }
    }
    //9. UNA VEZ SEPARADOS VAMOS A EVALUAR SI ES HIJO DE, RECORREMOS PADRES SI EL IDGEOGRAFIA =  PADRE ENTONCES = HIJO 

    //10. creamos html para enviar a la view
    var contenido = "";
    contenido += "<table id = 'tablaGeografia' class= 'table'>";
    contenido += "<thead>";
    contenido += "<tr>";
    contenido += "<td> Id</td>";
    contenido += "<td> pais</td>";
    contenido += "<td> coordenada x</td>";
    contenido += "<td> coordenada y</td>";
    contenido += "<td> padre</td>";
    contenido += "</tr>";
    contenido += "</thead>";
    contenido += "<tbody>";

    for (var j = 0; j < ArrayPadres.length; j++) {
        contenido += "<tr>";
        contenido += "<td>" + ArrayPadres[j].IdGeografia + "</td>";
        contenido += "<td>" + ArrayPadres[j].Pais + "</td>";
        contenido += "<td>" + ArrayPadres[j].CoordenadasX + "</td>";
        contenido += "<td>" + ArrayPadres[j].CoordenadasY + "</td>";
        contenido += "<td>" + ArrayPadres[j].Padre + "</td>";
        contenido += "</tr>";

        for (var k = 0; k < ArrayHijos.length; k++) {   //11.CREAMOS LA SUB TABLA


            if (ArrayHijos[k].Padre == ArrayPadres[j].IdGeografia) {
                contenido += "<tr>";
                contenido += "<td>" + ArrayHijos[k].IdGeografia + "</td>";
                contenido += "<td>" + ArrayHijos[k].Pais + "</td>";
                contenido += "<td>" + ArrayHijos[k].CoordenadasX + "</td>";
                contenido += "<td>" + ArrayHijos[k].CoordenadasY + "</td>";
                contenido += "<td>" + ArrayHijos[k].Padre + "</td>";
                contenido += "</tr>";
            }

        }

    }
    contenido += "</tbody>";
    contenido += "</table>";

    document.getElementById("divTabla").innerHTML = contenido;
    //$("#tablaGeografia").dataTable({



    //});

});
    /*funcionalidad boton*/
var btnBuscar = document.getElementById("btnBuscar");
btnBuscar.onclick = function () {
    //tenenmos apuntamos al metodo que me devuelve mi Json en el controlador
    //a la url le tenemos que pasar el parametro que coge de la view
    var valorBuscado = document.getElementById("txtBuscar").value;
    $.get("Geografia/ObtenerBusqueda/?pais=" + valorBuscado, function (data) {

        crearLista(data);

    });
}


        //que debe pasar si se le da a limpiar 
var btnLimpiar = document.getElementById("btnLimpiar");
btnLimpiar.onclick = function () {
    //tenenmos apuntamos al metodo que me devuelve mi Json en el controlador
    //a la url le tenemos que pasar el parametro que coge de la view
    $.get("Geografia/ObtenerTablaNested", function (data) {
        /*Creación de la funcionalidad de buscador*/
        crearLista(data);
    });
    
    document.getElementById("txtBuscar").value="";
}















