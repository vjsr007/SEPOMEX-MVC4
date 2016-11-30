Catalogo = {};
(function (self) {

    var formID = "#frmMunicipios"
    var $form = function (selector) { return $(formID).find(selector); }
    var pager = '#pgDatos';
    var ctrls = {
        get frmMunicipios() { return $(formID) },

        get txtNombre() { return $form('#txtNombre') },
        get txCodigo() { return $form("#txCodigo") },
        get chkActivo() { return $form("#chkActivo") },
        get cmbEstado() { return $form("#cmbEstado") },
        get cmbPais() { return $form("#cmbPais") },

        get btnAgregar() { return $form("#btnAgregar") },
        get btnEditar() { return $form("#btnEditar") },
        get btnBuscar() { return $form("#btnBuscar") },

        get opAgregar() { return $form("#opAgregar") },
        get opEditar() { return $form("#opEditar") },
        get opEliminar() { return $form("#opEliminar") },

        get add_tblDatos() { return $form("#add_tblDatos") },
        get edit_tblDatos() { return $form("#edit_tblDatos") },
        get del_tblDatos() { return $form("#del_tblDatos") },

        get grid() { return $form("#tblDatos") },
        get pager() { return $form(pager) }
    };
    var urls = {
        get ObtenerPaisesJson() { return webroot + 'Catalogo/ObtenerPaises' },
        get ObtenerEstadosJson() { return webroot + 'Catalogo/ObtenerEstados' },
        get EditarMunicipio() { return webroot + 'Catalogo/EditarMunicipio' },
        get AgregarMunicipio() { return webroot + 'Catalogo/AgregarMunicipio' },        
    }
    var Municipios = {};
    var postDataMunicipio = {};
    var optionSeleccione = '<option value="">[Seleccione]</option>';

    var BuscarMunicipios = function () {

        ctrls.grid.jqGrid("clearGridData", true);

        try {

            postDataMunicipio = {
                MunicipioID: 0,
                Nombre: $.trim(ctrls.txtNombre.val()) == '' ? ' ' : $.trim(ctrls.txtNombre.val()),
                Codigo: $.trim(ctrls.txCodigo.val()) == '' ? ' ' : $.trim(ctrls.txCodigo.val()),
                FechaUltimaModificacion: '1900/01/01',
                UsuarioID: 0,
                Activo: ctrls.chkActivo.is(":checked"),
                EstadoID: ctrls.cmbEstado.val(),
                PaisID: ctrls.cmbPais.val()
            }

            AppData.obtenerMunicipios(postDataMunicipio, function (data) {
                Result = data;

                if (Result.Error) {
                    Utils.mostrarMensaje("Error al obtener periodos", Result.Mensaje);
                }
                else {
                    ctrls.grid.setGridParam(
                    {
                        datatype: 'local',
                        data: data
                    });
                    ctrls.grid.trigger("reloadGrid", [{ page: 1 }]);
                }
            });
        } catch (e) {
            Utils.Utils.mostrarMensaje("Error de sintaxis", e.message);
        }
    };

    var cargarEstados = function () {
        var sHtml = optionSeleccione;
        if (ctrls.cmbPais.val() != "") {

            AppData.obtenerEstados(
                {
                    EstadoID: 0,
                    Nombre: ' ',
                    Codigo: ' ',
                    FechaUltimaModificacion: '',
                    UsuarioID: 0,
                    Activo: null,
                    PaisID: ctrls.cmbPais.val()
                },
                function (data)
                {
                    $.each(data, function (idx, d) {
                        sHtml += '<option value="' + d.EstadoID + '">' + d.Nombre + '</option>';
                    });
                    ctrls.cmbEstado.html(sHtml);
                }
            );
        }
        else {
            ctrls.cmbEstado.html(sHtml);
        }
    }

    var eventos = function () {
        ctrls.cmbEstado.html(optionSeleccione);

        ctrls.cmbPais.change(cargarEstados);

        ctrls.opAgregar.click(function () { ctrls.add_tblDatos.click() });
        ctrls.opEditar.click(function () { ctrls.edit_tblDatos.click() });
        ctrls.opEliminar.click(function () { ctrls.del_tblDatos.click() });

        ctrls.btnAgregar.click(function () { ctrls.add_tblDatos.click() });
        ctrls.btnEditar.click(function () { ctrls.edit_tblDatos.click() });
        ctrls.btnBuscar.click(BuscarMunicipios);

    }

    var inicializarControles = function () {
        AppData.obtenerPaises(
            {
                PaisID: null,
                Nombre: null,
                Codigo: null,
                Moneda: null,
                CodMoneda: null,
                FechaUltimaModificacion: null,
                UsuarioID: null,
                Activo: null
            }, function (data) {
                var sHtml = optionSeleccione;
                $.each(data, function (idx, d) {
                    sHtml += '<option value="' + d.PaisID + '">' + d.Nombre + '</option>';
                });
                ctrls.cmbPais.html(sHtml);
            }
        );
    }

    var configurarGrid = function () {
        ctrls.grid.jqGrid({
            datatype: 'local',
            data: Municipios,
            mtype: 'POST',
            colNames: ['MunicipioID', 'Nombre', 'Codigo', 'País', 'País', 'Estado', 'Estado', 'Fecha U.', 'Usuario', 'Activo'],
            colModel: [
                { name: 'MunicipioID', index: 'MunicipioID', key: true, hidden: false, sortable: true, sorttype: 'int' },
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
                        dataUrl: urls.ObtenerPaisesJson,
                        buildSelect: function (data) {
                            var response = $.parseJSON(data);
                            var s = '<select>';
                            s += optionSeleccione;
                            $.each(response, function (i, obj) {
                                s += '<option value="' + obj.PaisID + '">' + obj.Nombre + '</option>';
                            });
                            return s + "</select>";
                        },
                        dataInit: function (elem) {
                            $(elem).width(180);
                        },
                        dataEvents: [
                            {
                                type: 'change',
                                fn: function (e) {
                                    var sHtml = optionSeleccione;
                                    if ($(e.target).val() != "") {
                                        AppData.obtenerEstados({
                                            EstadoID: 0,
                                            Nombre: ' ',
                                            Codigo: ' ',
                                            FechaUltimaModificacion: '',
                                            UsuarioID: 0,
                                            Activo: null,
                                            PaisID: $(e.target).val()
                                        }, function (data) {
                                            $.each(data, function (idx, d) {
                                                sHtml += '<option value="' + d.EstadoID + '">' + d.Nombre + '</option>';
                                            });
                                            ctrls.cmbEstado.html(sHtml);
                                        }
                                        );
                                    }
                                    else {
                                        ctrls.cmbEstado.html(sHtml);
                                    }
                                }
                            }]
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
                    name: "EstadoID",
                    index: "EstadoID",
                    align: 'left',
                    sortable: true,
                    hidden: true,
                    editable: true,
                    edittype: 'select',
                    editrules: { edithidden: true, required: true },
                    editoptions: {
                        dataUrl: urls.ObtenerEstadosJson,
                        buildSelect: function (data) {
                            var response = $.parseJSON(data);
                            var selectedRow = ctrls.grid.getGridParam("selrow");
                            var rowData = ctrls.grid.getRowData(selectedRow);

                            var s = '<select>';
                            s += optionSeleccione;
                            $.each(response, function (i, obj) {
                                if (obj.PaisID == rowData.PaisID) {
                                    s += '<option value="' + obj.EstadoID + '">' + obj.Nombre + '</option>';
                                }
                            });
                            return s + "</select>";
                        },
                        dataInit: function (elem) {
                            $(elem).width(180);
                        }
                    }
                },
                {
                    name: "Estado",
                    index: "Estado",
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
            pager: ctrls.pager,
            rowNum: 10,
            loadui: 'disable',
            viewrecords: true,
            height: '100%',
            autowidth: true,
            sortname: 'MunicipioID',
            sortorder: "desc",
            loadComplete: function () {

            },
            loadError: function (xhr, st, err) {
                var error = $.parseJSON(xhr.responseText);
                dialogoError(error);
            }
        });

        BuscarMunicipios();

        configurarPager();
    }

    var configurarPager = function () {
        ctrls.grid.jqGrid('navGrid', pager, { search: false, refresh: false, del: false },
            {
                url: ctrls.EditarMunicipio,
                recreateForm: true,
                modal: true,
                width: "350",
                beforeInitData: function () {
                    ctrls.grid.jqGrid('setColProp', 'MunicipioID', { editable: true, editrules: { required: true }, editoptions: {} });

                    var cm = ctrls.grid.jqGrid('getColProp', 'MunicipioID');
                    cm.editoptions.disabled = true;

                    var selectedRow = ctrls.grid.getGridParam("selrow");
                    var rowData = ctrls.grid.getRowData(selectedRow);
                    ctrls.grid.setColProp('EstadoID', {
                        editoptions: {
                            dataUrl: urls.ObtenerEstadosJson + '?PaisID=' + rowData.PaisID
                        }
                    });
                },
                afterSubmit: function (response, postdata) {
                    Utils.mostrarMensaje("Edición de País", "Operación Exitosa");

                    BuscarMunicipios();

                    return [response.status, response.statusText, null];
                },
                errorTextFormat: function (data) {
                    var Mensaje = $(data.responseText).find('i').html();
                    Utils.mostrarMensaje("Error: " + data.statusText, Mensaje == null ? data.responseText : Mensaje);
                },
                closeAfterEdit: true
            },
            {
                url: urls.AgregarMunicipio,
                recreateForm: true,
                modal: true,
                width: "500",
                beforeInitData: function () {
                    ctrls.grid.jqGrid('setColProp', 'MunicipioID', { editable: false, editrules: { required: false }, editoptions: {} });

                    var cm = ctrls.grid.jqGrid('getColProp', 'MunicipioID');
                    cm.editoptions.disabled = true;
                },
                afterSubmit: function (response, postdata) {
                    Utils.mostrarMensaje("Edición de País", "Operación Exitosa");

                    BuscarMunicipios();

                    return [response.status, response.statusText, null];
                },
                errorTextFormat: function (data) {
                    var Mensaje = $(data.responseText).find('i').html();
                    Utils.mostrarMensaje("Error: " + data.statusText, Mensaje == null ? data.responseText : Mensaje);
                },
                closeAfterAdd: true
            }
        );
    }

    var init = function () {
        inicializarControles();

        eventos();

        configurarGrid();

        $.ajaxSetup({ cache: false });
    }

    $(init);
})(Catalogo.Municipio = {});