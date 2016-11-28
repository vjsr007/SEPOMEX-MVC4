var Home = {};
(function(self){

    var countingEntities = function () {
        Utils.ejecutarAjax(
            null,
            webroot + 'Home/CountingEntities',
            function (d) {
                if (d.Error) {
                    Utils.mostrarMensaje("Error al obtener paises", d.Mensaje);
                    return;
                }

                $("#txtCountPais").html(d.NoPaises.formatMoney(0, '.', ','));
                $("#txtCountEstado").html(d.NoEstados.formatMoney(0, '.', ','));
                $("#txtCountMunicipio").html(d.NoMunicipios.formatMoney(0, '.', ','));
                $("#txtCountCiudad").html(d.NoCiudades.formatMoney(0, '.', ','));
                $("#txtCountCP").html(d.NoCP.formatMoney(0, '.', ','));
            });
    };

    var init = function () {
        countingEntities();
    }

    $(init);

})(Home.Index = {})