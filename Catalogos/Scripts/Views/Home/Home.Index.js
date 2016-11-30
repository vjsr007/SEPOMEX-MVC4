var Home = {} || Home;
(function (self) {
    "use strict";

    var formID = "#frmMenu"
    var $form = function (selector) { return $(formID).find(selector); }
    var ctrls = {
        get frmMenu() { return $(formID) },
        get txtCountPais() { return $form('#txtCountPais') },
        get txtCountEstado() { return $form("#txtCountEstado") },
        get txtCountMunicipio() { return $form("#txtCountMunicipio") },
        get txtCountCiudad() { return $form("#txtCountCiudad") },
        get txtCountCP() { return $form("#txtCountCP") },
    };
    var urls = {
        get CountingEntities() { return webroot + 'Home/CountingEntities' },
    }

    var countingEntities = function () {
        Utils.ejecutarAjax(
            null,
            urls.CountingEntities,
            function (d) {
                if (d.Error) {
                    Utils.mostrarMensaje("Error al obtener paises", d.Mensaje);
                    return;
                }

                ctrls.txtCountPais.html(d.NoPaises.formatMoney(0, '.', ','));
                ctrls.txtCountEstado.html(d.NoEstados.formatMoney(0, '.', ','));
                ctrls.txtCountMunicipio.html(d.NoMunicipios.formatMoney(0, '.', ','));
                ctrls.txtCountCiudad.html(d.NoCiudades.formatMoney(0, '.', ','));
                ctrls.txtCountCP.html(d.NoCP.formatMoney(0, '.', ','));
            });
    };

    var init = function () {
        countingEntities();
    }

    $(init);

})(Home.Index = {})