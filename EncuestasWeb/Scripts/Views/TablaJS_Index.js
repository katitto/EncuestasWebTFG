/*/*$.get("Geografia/ObtenerTablaNested", function (data) {*/
    /*Creación de la funcionalidad de buscador*/
$(document).ready(function () {
    var contenido = "";
    var nombre = " hola katito"
    var contenido = "<h1>"+ nombre+ "</h1>";
    document.getElementById("divTabla").innerHTML = contenido;
});
    /*creamos un DOm para llegar a la tabla por el id. formar contenidohtml y luego meter en el div*/
/*    crearLista(data);*/


    /*separamos padres de hijos*/

    //var nfilas = Object.keys(data).length;
    //alert(nfilas);
    //var ArrayPadres = [];
    //var ArrayHijos = [];
    //for (var i = 0; i < nfilas; i++) {
        
    //    let geografiaData = JSON.stringify(data[i]);
    //    let geografia = JSON.parse(geografiaData);
    //    if (geografia.Padre == 0) {
    //        var objeto = [geografia.IdGeografia, geografia.Pais, geografia.CoordenadasX, geografia.CoordenadasY, geografia.Padre];
    //        ArrayPadres.push(objeto);

    //    } else {
    //        var objeto2 = [geografia.IdGeografia, geografia.Pais, geografia.CoordenadasX, geografia.CoordenadasY, geografia.Padre];
    //        ArrayHijos.push(objeto2);
    //    }

        
    //}

    //for (var j = 0; j < ArrayPadres.length; j++) {
    //    for (var k = 0; k < ArrayHijos.length; k++)
    //    {
    //        alert(ArrayPadres[j]);
    //         if (ArrayHijos[k].Padre == ArrayPadre[j].IdGeografia)
    //         {
    //             alert(ArrayHijos[k]);
                
    //        }

    //    }

    //}



/*});*/

//    /*funcionalidad boton*/
//var btnBuscar = document.getElementById("btnBuscar");
//btnBuscar.onclick = function () {
//    //tenenmos apuntamos al metodo que me devuelve mi Json en el controlador
//    //a la url le tenemos que pasar el parametro que coge de la view
//    var valorBuscado = document.getElementById("txtBuscar").value;
//    $.get("Geografia/ObtenerBusqueda/?pais=" + valorBuscado, function (data) {

//        crearLista(data);

//    });
//}

//        //que debe pasar si se le da a limpiar 
//var btnLimpiar = document.getElementById("btnLimpiar");
//btnLimpiar.onclick = function () {
//    //tenenmos apuntamos al metodo que me devuelve mi Json en el controlador
//    //a la url le tenemos que pasar el parametro que coge de la view
//    $.get("Geografia/ObtenerTablaNested", function (data) {
//        /*Creación de la funcionalidad de buscador*/
//        crearLista(data);
//    });
    
//    document.getElementById("txtBuscar").value="";
//}
///*data el el nombre que queramos ya que esta apuntando a la dirección url controlador /accion*/

//function crearLista(data) { 
//    /*1. inicializamos variable = "" */
//    var contenido = "";
//    /*+= contetena*/
//    contenido += "<table id = 'tablaGeografia'class= 'table'>";
//    contenido += "<thead>";
//    contenido += "<tr>";
//    contenido += "<td> Id</td>";
//    contenido += "<td> pais</td>";
//    contenido += "<td> coordenada x</td>";
//    contenido += "<td> coordenada y</td>";
//    contenido += "<td> padre</td>";
//    contenido += "</tr>";
//    contenido += "</thead>";
//    contenido += "<tbody>";
//    var nfilas = Object.keys(data).length;
//    for (var i = 0; i < nfilas; i++) {
//        contenido += "<tr>";
//        contenido += "<td>" + data[i].IdGeografia + "</td>";
//        contenido += "<td>" + data[i].Pais + "</td>";
//        contenido += "<td>" + data[i].CoordenadasX + "</td>";
//        contenido += "<td>" + data[i].CoordenadasY + "</td>";
//        contenido += "<td>" + data[i].Padre + "</td>";
//        contenido += "</tr>"; 

//    }
//    contenido += "</tbody>";
//    contenido += "</table>";

//    document.getElementById("divTabla").innerHTML = contenido;
//    /*creamos un DOm para llegar a la tabla por el id. formar contenidohtml y luego meter en el div*/


//}













