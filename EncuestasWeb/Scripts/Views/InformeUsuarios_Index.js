var tabladata;

var tablacarga;


$(document).ready(function () {


    ////validamos el formulario
    $("#formNivel").validate({
        rules: {
            Nombre: "required",
            Apellido: "required",
            User: "required",
            Contrasena: "required",
            Email: "required",
            RefEje: "required",
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
            { "data": "Contrasena" },//debe ir oculta
            { "data": "Email" },
            {
                "data": "RefEje"
            },
            {
                "data": "oRol", render: function (data) {
                    return data.Nombre
                }
            },
            { "data": "IdUsuario" },

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



function ObtenerFormatoFecha(datetime) {

    var re = /-?\d+/;
    var m = re.exec(datetime);
    var d = new Date(parseInt(m[0]))


    var month = d.getMonth() + 1;
    var day = d.getDate();
    var output = (('' + day).length < 2 ? '0' : '') + day + '-' + (('' + month).length < 2 ? '0' : '') + month + '-' + d.getFullYear();

    return output;
}

function printData() {

    if ($('#tbdata tbody tr').length == 0) {
        swal("Mensaje", "No existen datos para imprimir", "warning")
        return;
    }

    var divToPrint = document.getElementById("tbdata");

    var style = "<style>";
    style = style + "table {width: 100%;font: 17px Calibri;}";
    style = style + "table, th, td {border: solid 1px #DDD; border-collapse: collapse;";
    style = style + "padding: 2px 3px;text-align: center;}";
    style = style + "</style>";

    newWin = window.open("");


    newWin.document.write(style);
    newWin.document.write("<h3>USUARIOS- " + "</h3>");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}