$.get("Geografia/ObtenerPadres", function (data) {
    var contenido = "";
    var nfilas = Object.keys(data).length;
    for (var i = 0; i < nfilas; i++) {
        contenido += "<option value='"+data[i].IdGeografia+"'>"
        contenido += data[i].Pais;
        contenido += "</option>"
    }

    document.getElementById("cboPadres").innerHTML = contenido;

});