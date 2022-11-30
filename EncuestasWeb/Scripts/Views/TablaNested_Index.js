$.get("Geografia/ObtenerTablaNested", function (data) {

    /*data el el nombre que queramos ya que esta apuntando a la dirección url controlador /accion*/

    /*1. inicializamos variable = "" */
    var contenido = "";
    /*+= contetena*/
    contenido += "<table id = 'tablaGeografia'class= 'table'>";
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
    var nfilas = Object.keys(data).length;
    for (var i = 0; i < nfilas; i++) {
            contenido += "<tr>";
            contenido += "<td>" + data[i].IdGeografia + "</td>";
            contenido += "<td>" + data[i].Pais + "</td>";
            contenido += "<td>" + data[i].CoordenadasX + "</td>";
            contenido += "<td>" + data[i].CoordenadasY + "</td>";
            contenido += "<td>" + data[i].Padre + "</td>";
            contenido += "</tr>";
        }
    contenido += "</tbody>";
    contenido += "</table>";
    document.getElementById("divTabla").innerHTML = contenido;
    /*creamos un DOm para llegar a la tabla por el id. formar contenidohtml y luego meter en el div*/
    $("#tablaGeografia").dataTable(
        {
            

        });

});