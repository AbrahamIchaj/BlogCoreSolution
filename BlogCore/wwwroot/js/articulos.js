var dataTable;

$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tblArticulos").DataTable({
        ajax: {
            url: "/admin/articulos/GetAll",
            type: "GET",
            datatype: "json"
        },
        columns: [
            { data: "id", width: "5%" },
            { data: "nombre", width: "20%" },
            { data: "categoria.nombre", width: "15%" },
            {
                data: "urlImagen",
                width: "20%",
                render: function (data) {
                    if (!data) return "";
                    return `<img src="/${data}" width="120" />`;
                }
            },
            {
                data: "fechaCreacion",
                width: "15%",
                render: function (data) {
                    if (!data) return "";
                    return new Date(data).toLocaleDateString("es-ES");
                }
            },
            {
                data: "id",
                width: "25%",
                render: function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Articulos/Edit/${data}" class="btn btn-success text-white" style="width:100px;">
                                    <i class="bi bi-pencil"></i> Editar
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Articulos/Delete/${data}") class="btn btn-danger text-white" style="width:100px;">
                                    <i class="bi bi-x-octagon"></i> Borrar
                                </a>
                            </div>`;
                }
            }
        ],
        language: {
            emptyTable: "No hay registros",
            info: "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            infoEmpty: "Mostrando 0 a 0 de 0 Entradas",
            infoFiltered: "(Filtrado de _MAX_ total entradas)",
            lengthMenu: "Mostrar _MENU_ Entradas",
            loadingRecords: "Cargando...",
            processing: "Procesando...",
            search: "Buscar:",
            zeroRecords: "Sin resultados encontrados",
            paginate: {
                first: "Primero",
                last: "Último",
                next: "Siguiente",
                previous: "Anterior"
            }
        },
        width: "100%"
    });
}

function Delete(url) {
    swal({
        title: "¿Está seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sí, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                } else {
                    toastr.error(data.message);
                }
            }
        });
    });
}