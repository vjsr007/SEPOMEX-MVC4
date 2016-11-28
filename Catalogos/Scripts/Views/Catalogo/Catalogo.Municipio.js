Catalogo = {};
(function (self) {

    var Municipios = {};
    var postDataMunicipio = {};
    var grid;

    var BuscarMunicipios = function () {

        grid.jqGrid("clearGridData", true);

        try {

            postDataMunicipio = {
	            MunicipioID : 0,
	            Nombre : $.trim($("#txtNombre").val())==''?' ':$.trim($("#txtNombre").val()),
	            Codigo : $.trim($("#txCodigo").val())==''?' ':$.trim($("#txCodigo").val()),
	            FechaUltimaModificacion: '1900/01/01',
	            UsuarioID: 0,
                Activo : $("#chkActivo").is(":checked"),
                EstadoID : $("#cmbEstado").val(),
                PaisID : $("#cmbPais").val()
            }

            obtenerMunicipios(postDataMunicipio, function(data){
                Result = data;

                if (Result.Error) {
                    mostrarMensaje("Error al obtener periodos", Result.Mensaje);
                }
                else {
                    grid.setGridParam(
                    {
                        datatype: 'local',
                        data: data
                    });
                    grid.trigger("reloadGrid", [{ page: 1}]);
                } 
            });
        } catch (e) {
            mostrarMensaje("Error de sintaxis", e.message);
        }
    };

    var init = function () {
        $.ajaxSetup({ cache: false });

        obtenerPaises({
            PaisID: null,
            Nombre: null,
            Codigo: null,
            Moneda: null,
            CodMoneda: null,
            FechaUltimaModificacion: null,
            UsuarioID: null,
            Activo: null
        }, function (data) {
            var sHtml = '<option value="">[Seleccione]</option>';
            $.each(data, function (idx, d) {
                sHtml += '<option value="' + d.PaisID + '">' + d.Nombre + '</option>';
            });
            $('#cmbPais').html(sHtml);
        }
        );

        $('#cmbEstado').html('<option value="">[Seleccione]</option>');
        $('#cmbPais').change(function () {
            var sHtml = '<option value="">[Seleccione]</option>';
            if ($(this).val() != "") {
                obtenerEstados({
                    EstadoID: 0,
                    Nombre: ' ',
                    Codigo: ' ',
                    FechaUltimaModificacion: '',
                    UsuarioID: 0,
                    Activo: null,
                    PaisID: $(this).val()
                }, function (data) {
                    $.each(data, function (idx, d) {
                        sHtml += '<option value="' + d.EstadoID + '">' + d.Nombre + '</option>';
                    });
                    $('#cmbEstado').html(sHtml);
                }
                );
            }
            else {
                $('#cmbEstado').html(sHtml);
            }
        });

        grid = $("#tblDatos");

        grid.jqGrid({
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
                        },
                        dataEvents: [
                            {
                                type: 'change',
                                fn: function (e) {
                                    var sHtml = '<option value="">[Seleccione]</option>';
                                    if ($(e.target).val() != "") {
                                        obtenerEstados({
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
                                            $("#EstadoID").html(sHtml);
                                        }
                                        );
                                    }
                                    else {
                                        $("#EstadoID").html(sHtml);
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
                        dataUrl: webroot + 'Catalogo/ObtenerEstadosJson',
                        buildSelect: function (data) {
                            var response = $.parseJSON(data);
                            var selectedRow = grid.getGridParam("selrow");
                            var rowData = grid.getRowData(selectedRow);

                            var s = '<select>';
                            s += '<option value="">[Seleccione]</option>';
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
            pager: $('#pgDatos'),
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

        grid.jqGrid('navGrid', '#pgDatos', { search: false, refresh: false, del: false },
            {
                url: window.webroot + "Catalogo/EditarMunicipio",
                recreateForm: true,
                modal: true,
                width: "350",
                beforeInitData: function () {
                    grid.jqGrid('setColProp', 'MunicipioID', { editable: true, editrules: { required: true }, editoptions: {} });

                    var cm = grid.jqGrid('getColProp', 'MunicipioID');
                    cm.editoptions.disabled = true;

                    var selectedRow = grid.getGridParam("selrow");
                    var rowData = grid.getRowData(selectedRow);
                    grid.setColProp('EstadoID', {
                        editoptions: {
                            dataUrl: window.webroot + 'Catalogo/ObtenerEstadosJson?PaisID=' + rowData.PaisID
                        }
                    });
                },
                afterSubmit: function (response, postdata) {
                    mostrarMensaje("Edición de País", "Operación Exitosa");
                    BuscarMunicipios();

                    return [response.status, response.statusText, null];
                },
                errorTextFormat: function (data) {
                    var Mensaje = $(data.responseText).find('i').html();
                    mostrarMensaje("Error: " + data.statusText, Mensaje == null ? data.responseText : Mensaje);
                },
                closeAfterEdit: true
            },
            {
                url: window.webroot + "Catalogo/AgregarMunicipio",
                recreateForm: true,
                modal: true,
                width: "500",
                beforeInitData: function () {
                    grid.jqGrid('setColProp', 'MunicipioID', { editable: false, editrules: { required: false }, editoptions: {} });

                    var cm = grid.jqGrid('getColProp', 'MunicipioID');
                    cm.editoptions.disabled = true;
                },
                afterSubmit: function (response, postdata) {
                    mostrarMensaje("Edición de País", "Operación Exitosa");
                    BuscarMunicipios();

                    return [response.status, response.statusText, null];
                },
                errorTextFormat: function (data) {
                    var Mensaje = $(data.responseText).find('i').html();
                    mostrarMensaje("Error: " + data.statusText, Mensaje == null ? data.responseText : Mensaje);
                },
                closeAfterAdd: true
            }
        );

        $("#btnBuscar").click(function () {
            BuscarMunicipios();
        });

        $("#opAgregar").click(function () {
            $("#add_tblDatos").click();
        });

        $("#opEditar").click(function () {
            $("#edit_tblDatos").click();
        });

        $("#opEliminar").click(function () {
            $("#del_tblDatos").click();
        });

        BuscarMunicipios();
    }

    $(init);
})(Catalogo.Municipio = {});