Catalogo = {};
(function (self) {
    "use strict";

    var grid;

    var Estados;

    var modalEstado;

    var BuscarEstados = function () {

        grid.jqGrid("clearGridData", true);

        try {

            var model = {
	            EstadoID : 0,
	            Nombre : $.trim($("#txtNombre").val())==''?' ':$.trim($("#txtNombre").val()),
	            Codigo : $.trim($("#txCodigo").val())==''?' ':$.trim($("#txCodigo").val()),
	            FechaUltimaModificacion: '1900/01/01',
	            UsuarioID: 0,
                Activo : $("#chkActivo").is(":checked"),
                PaisID : $("#cmbPais").val()
            }

            Utils.ejecutarAjax(
                model,
                webroot + "Catalogo/ObtenerEstados",
                function (data) {
                    if (data.Error) {
                        Utils.mostrarMensaje("Error al obtener periodos", Result.Mensaje);
                    }
                    else {
                        grid.setGridParam(
                        {
                            datatype: 'local',
                            data: data
                        });
                        grid.trigger("reloadGrid", [{ page: 1}]);
                    } 
                }
            );
        } catch (e) {
            Utils.mostrarMensaje("Error de sintaxis", e.message);
        }
    };

    var configGrid = function () {
        grid = $("#tblDatos");

        grid.jqGrid({
            datatype: 'local',
            data: Estados,
            mtype: 'POST',
            colNames: ['EstadoID', 'Nombre', 'Codigo', 'País', 'País', 'Fecha U.', 'Usuario', 'Activo'],
            colModel: [
                { name: 'EstadoID', index: 'EstadoID', key: true, hidden: false, sortable: true, sorttype: 'int' },
                {
                    name: 'Nombre',
                    index: 'Nombre',
                    sortable: true,
                    align: 'left',
                    editable: true
                },
                {
                    name: "Codigo",
                    index: "Codigo",
                    align: 'left',
                    sortable: true,
                    editable: true,
                    editrules: { required: true }
                },
                {
                    name: "PaisID",
                    index: "PaisID",
                    align: 'left',
                    sortable: true,
                    hidden: true,
                    editable: true,
                    edittype: 'select',
                    editrules: { edithidden: true, required: true },
                    editoptions: {
                        dataUrl: webroot + 'Catalogo/ObtenerPaisesJson',
                        buildSelect: function (data) {
                            var response = $.parseJSON(data);
                            var s = '<select>';
                            s += '<option value="">[Seleccione]</option>';
                            $.each(response, function (i, obj) {
                                s += '<option value="' + obj.PaisID + '">' + obj.Nombre + '</option>';
                            });
                            return s + "</select>";
                        },
                        dataInit: function (elem) {
                            $(elem).width(180);
                        }
                    }
                },
                {
                    name: "Pais",
                    index: "Pais",
                    align: 'left',
                    sortable: true,
                    editable: false,
                    editrules: { required: true }
                },
                {
                    name: 'FechaUltimaModificacion',
                    index: 'FechaUltimaModificacion',
                    align: 'left',
                    sortable: true,
                    editable: false,
                    editoptions: { maxlength: 10 },
                    editrules: { required: false },
                    formatter: "date",
                    formatoptions: { srcformat: "d/m/Y ", newformat: "d/m/Y" }
                },
                {
                    name: 'UsuarioID',
                    index: 'UsuarioID',
                    align: 'left',
                    sortable: true,
                    editable: false,
                    formatter: 'int',
                    formatoptions: { defaultValue: '' }
                },
                {
                    name: 'Activo',
                    index: 'Activo',
                    align: 'left',
                    sortable: true,
                    editable: true,
                    formatter: 'checkbox',
                    edittype: "checkbox",
                    editoptions: { value: "true:false" }
                }
            ],
            pager: $('#pgDatos'),
            rowNum: 10,
            loadui: 'disable',
            viewrecords: true,
            height: '100%',
            autowidth: true,
            sortname: 'EstadoID',
            sortorder: "desc"
        });
    }

    var mostrarEdicion = function (accion) {
        var selRow = accion == "A" ? -1 : grid.jqGrid('getGridParam', 'selrow');

        if (selRow) {
            var titulo = accion == "A" ? "Agregar Estado" : "Editar Estado";

            modalEstado = Utils.mostrarModal(
                            titulo,
                            webroot + "Catalogo/EstadoEditar",
                            { EstadoID: selRow, Accion: accion },
                            {
                                width: 350,
                                buttons: {},
                                show: { effect: "blind", duration: 800 },
                                beforeClose: function (event, ui) {
                                    var $dialog = $(this);

                                    Utils.mostrarConfirmar("", "¿Desea cerrar la ventana?", null, null, function () {
                                        $dialog.dialog({ beforeClose: {} });
                                        $dialog.dialog('close');
                                    });

                                    return false;
                                }
                            },
                            function (dialog) {
                                Catalogo.EditarEstado.Success = BuscarEstados;
                                Catalogo.EditarEstado.Modal = dialog;
                            }
                        );
        }
        else {
            Utils.mostrarMensaje("Catálogos", "Seleccione un registro");
        }
    }

    var eventos = function () {
        $("#btnBuscar").click(function () {
            BuscarEstados();
        });

        $("#opAgregar").click(function () {
            mostrarEdicion("A");
        });

        $("#btnAgregar").click(function () {
            mostrarEdicion("A");
        });

        $("#opEditar").click(function () {
            mostrarEdicion("E");
        });

        $("#btnEditar").click(function () {
            mostrarEdicion("E");
        });
    }

    var init = function () {
        $.ajaxSetup({ cache: false });

        eventos();

        configGrid();

        BuscarEstados();
    };

    $(init);

})(Catalogo.Estado = {});