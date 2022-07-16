var dataTable;

$(document).ready(function () {
    loadDataTable();
});


//acorde con el API que creamos de Book, se crea un metodo que me carga e la variable dataTable
// un objeto json que contiene todos los registros de la base de datos
//esta funcion que me retorna ese json se encuentra en el url "api/book" , es el metodo de tipo GET y 
// retorna un json y establecemos el formato en el que queremos que se inserten los datos de cada registro en el json
// (UTILIZAR NOTACION CAMELCASE, si en el modelo sale la propiedad NameABC, aqui llamarlo nameAbc)
function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/book",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "autor", "width": "20%" },
            { "data": "isbn", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                               <a href="/BookList/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                                  Edit
                               </a>
                               &nbsp;
                               <a onclick=Delete('/api/book?id='+${data}) class='btn btn-danger text-white' style='cursor:pointer; width:70px;'>
                                  Delete
                               </a>
                            </div> `;
                },"width":"40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width":"100%"
    })
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        //si el usuario le da a Yes(confirmar eliminacion)
        if (willDelete) {
            //se realiza una llamada a Ajax
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        //imprime el mensaje del controlador success del  API Book
                        toastr.success(data.message);
                        //volvemos a cargar la tabla con los registros
                        dataTable.ajax.reload();
                    } else {
                        //imprime el mensaje del controlador error del  API Book
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}