var Catalogo = {};
(function (self) {
    "use strict";

    var Paises = {};
    var postDataPais = {};
    var grid;
    var modalPais;

    var BuscarPaises = function () {
        grid.jqGrid("clearGridData", true);

        try {

            postDataPais = {
	            PaisID : null,
	            Nombre : $.trim($("#txtNombre").val())==''?' ':$.trim($("#txtNombre").val()),
	            Codigo : $.trim($("#txCodigo").val())==''?' ':$.trim($("#txCodigo").val()),
	            Moneda : ' ',
	            CodMoneda: ' ',
	            FechaUltimaModificacion: '1900/01/01',
	            UsuarioID: 0,
                Activo : $("#chkActivo").is(":checked")
            }

            Utils.ejecutarAjax(
                postDataPais,
                webroot + 'Catalogo/ObtenerPaises',
                function (data) {
                    if (data.Error) {
                        Utils.mostrarMensaje("Error al obtener paises", data.Mensaje);
                    }
                    else {
                        reloadGrid(data);
                    } 
                }
            );
        } catch (e) {
            Utils.mostrarMensaje("Error de sintaxis", e.message);
        }
    };

    var configurarGrid = function(){
        grid = $("#tblDatos");

        grid.jqGrid({
            datatype: 'local',
            data: Paises,
            colNames: ['PaisID', 'Nombre', 'Codigo', 'Moneda', 'Codigo Moneda', 'Fecha U.', 'Usuario', 'Activo'],
            colModel: [
                { name: 'PaisID', index: 'PaisID', key: true, hidden: false, sortable: true },
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
                    name: "Moneda",
                    index: "Moneda",
                    align: "left",
                    sortable: true,
                    editable: true,
                    editrules: { required: true }
                },
                {
                    name: "CodMoneda",
                    index: "CodMoneda",
                    align: "right",
                    width: 80,
                    sortable: true,
                    editable: true,
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
                    edittype:"checkbox",
                    editoptions: {value: "true:false"}
                }
            ],
            pager: $('#tblDatosPager'),
            rowNum: 10,
            loadui: 'disable',
            viewrecords: true,
            height: '100%',
            autowidth: true,
            sortname: 'PaisID',
            sortorder: "desc",
            loadComplete: function () {

            },
            loadError: function (xhr, st, err) {
                var error = $.parseJSON(xhr.responseText);
                Utils.mostrarMensaje("Error", error);
            }
        });    
    };

    var configurarGridPager = function(){
        grid.jqGrid('navGrid', '#tblDatosPager', { search: false, refresh:false, del: false, add: true, edit: true },
            {
                url: window.webroot + "Catalogo/EditarPais",
                recreateForm: true,
                modal: true,
                width: "350",
                beforeInitData: function () {
                    grid.jqGrid('setColProp', 'PaisID', { editable: true, editrules: { required: true },editoptions:{} });

                    var cm = grid.jqGrid('getColProp', 'PaisID');
                    cm.editoptions.disabled = true;
                },
                afterSubmit: function (response, postdata) {
                    var data = $.parseJSON(response.responseText);

                    if(!data.Error){
                        Utils.mostrarMensaje("Guardar País",  "Guardado Exitoso");
                        BuscarPaises();
                    }
                    else{
                        Utils.mostrarMensaje("Error", data.Mensaje);
                    }

                    return [response.status, response.statusText, null];
                },
                errorTextFormat: function (data) {
                      var Mensaje= $(data.responseText).find('i').html();
                      Utils.mostrarMensaje("Error: " + data.statusText, Mensaje == null ? data.responseText : Mensaje);
                },
                closeAfterEdit: true
            },
            {
                url: window.webroot + "Catalogo/AgregarPais",
                recreateForm: true,
                modal: true,
                width: "500",
                beforeInitData: function () {
                    grid.jqGrid('setColProp', 'PaisID', { editable: true, editrules: { required: false },editoptions:{} });

                    var cm = grid.jqGrid('getColProp', 'PaisID');
                    cm.editoptions.disabled = true;
                },
                afterSubmit: function (response, postdata) {
                    var data = $.parseJSON(response.responseText);

                    if(!data.Error){
                        Utils.mostrarMensaje("Guardar País", "Guardado Exitoso");
                        BuscarPaises();
                    }
                    else{
                        Utils.mostrarMensaje("Error", data.Mensaje);
                    }

                    return [response.status, response.statusText, null];
                },
                errorTextFormat: function (data) {
                      var Mensaje= $(data.responseText).find('i').html();
                      Utils.mostrarMensaje("Error: " + data.statusText, Mensaje == null ? data.responseText : Mensaje);
                },
                closeAfterAdd: true
            }
        );
    };

    var reloadGrid = function (data) {
        grid.setGridParam(
        {
            datatype: 'local',
            data: data
        });
        grid.trigger("reloadGrid", [{ page: 1 }]);
    };

    var mostrarEdicion = function (accion) {
        var selRow = accion=="A" ? -1 : grid.jqGrid('getGridParam', 'selrow');

        if (selRow) {
            var titulo = accion == "A" ? "Agregar País" : "Editar País";

            modalPais = Utils.mostrarModal(
                            titulo,
                            webroot + "Catalogo/PaisEditar",
                            { PaisID: selRow, Accion: accion },
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
                                Catalogo.EditarPais.Modal = dialog;
                            }
                        );
        }
        else {
            Utils.mostrarMensaje("Catálogos", "Seleccione un registro");
        }
    }

    var enlazarEventos = function(){
        $("#btnBuscar").click(function () {
            BuscarPaises();
        });

        $("#opAgregar").click(function(){
            mostrarEdicion("A");
        });

        $("#btnAgregar").click(function () {
            mostrarEdicion("A");
        });

        $("#opEditar").click(function(){
            mostrarEdicion("E");
        });

        $("#btnEditar").click(function () {
            mostrarEdicion("E");
        });
    };

    self.init = function () {

        $.ajaxSetup({ cache: false });

        configurarGrid();

        configurarGridPager();

        enlazarEventos();

        BuscarPaises();
    };

    self.disparaBusqueda = BuscarPaises;

    $(self.init);
})(Catalogo.Pais = {});