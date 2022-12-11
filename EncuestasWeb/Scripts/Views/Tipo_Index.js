var tabladata;

$(document).ready(function () {
    ////validamos el formulario
    
    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": $.MisUrls.url.Url_ObtenerTipo,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Nombre" },
            { "data": "CoordenadasX" },
            { "data": "CoordenadasY"}
        ],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        }
    });

    $('#tbdata').on('click', 'tr', function () {
        var map;
        var data = table.row(this).data();
        var nombre = data[0];
        var x = data[1];
        var y = data[2];
        map = new google.maps.Map(document.getElementById('main'), {
            center: { lat: y, lng: x },
            zoom: 8
        });
        var marker = new google.maps.Marker({
            position: { lat: y, lng: x },
            map: map,
            title: nombre
        });
    });

});
