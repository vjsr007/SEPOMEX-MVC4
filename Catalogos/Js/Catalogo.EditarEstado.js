//@ sourceURL=Catalogo.EditarEstado.js
var Catalogo = Catalogo ? Catalogo : {};
(function (self) {
    "use strict";

    var $form;

    self.Modal = null;

    var guardarEstado = function () {

        var Accion = $form.find("#Accion").val();

        var Url = Accion == "A" ? webroot + "Catalogo/AgregarEstado" : webroot + "Catalogo/EditarEstado";

        var dataString = {
            Activo : $form.find("#Activo").is(":checked"),
            Codigo: $form.find("#Codigo").val(),
            CodMoneda: $form.find("#CodMoneda").val(),
            Moneda: $form.find("#Moneda").val(),
            Nombre: $form.find("#Nombre").val(),
            PaisID: $form.find("#PaisID").val(),
            EstadoID: $form.find("#EstadoID").val(),
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
                        Utils.mostrarMensaje("Editar Estado", "Guardado Exitoso");
                        self.Modal.dialog({ beforeClose: {} });
                        self.Modal.dialog("close");
                        if (self.Success) self.Success();
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
            guardarEstado();
        });
    };

    self.init = function () {
        $.ajaxSetup({ cache: false });
        $form = $("#frmEditarEstado");
        eventos();
    };

    $(self.init);
})(Catalogo.EditarEstado = {});