//@ sourceURL=Catalogo.EditarPais.js
var Catalogo = Catalogo ? Catalogo : {};
(function (self) {
    "use strict";

    var $form;
    self.Modal = null;

    var guardarPais = function () {

        var Accion = $form.find("#Accion").val();

        var Url = Accion == "A" ? webroot + "Catalogo/AgregarPais" : webroot + "Catalogo/EditarPais";

        var dataString = {
            Activo : $form.find("#Activo").val(),
            Codigo: $form.find("#Codigo").val(),
            CodMoneda: $form.find("#CodMoneda").val(),
            Moneda: $form.find("#Moneda").val(),
            Nombre: $form.find("#Nombre").val(),
            PaisID: $form.find("#PaisID").val(),
        };

        var isValid = $form.valid();

        if (isValid) {
            Utils.ejecutarAjax(
                dataString,
                Url,
                function (data) {
                    if (data.Error) {
                        Utils.mostrarMensaje("Error al validar Modelo", data.Mensaje);
                    }
                    else {
                        Catalogo.Pais.disparaBusqueda();

                        Utils.mostrarMensaje("Editar País", "Guardado Exitoso");
                        self.Modal.dialog({ beforeClose: {} });
                        self.Modal.dialog("close");

                    }
                }
            );
        }
        else {
            Utils.mostrarMensaje("Error en datos de entrada", $form.find("#frmErrores").html());
        }

        return false;
    }

    var eventos = function () {
        Utils.hackValidation($form);

        $form.find("#btnCerrar").click(function () {
            self.Modal.dialog("close");
        });

        $form.find("#btnGuardar").click(function () {
            guardarPais();
        });
    };

    self.init = function () {
        $.ajaxSetup({ cache: false });
        $form = $("#frmEditarPais");
        eventos();
    };

    $(self.init);
})(Catalogo.EditarPais = {});