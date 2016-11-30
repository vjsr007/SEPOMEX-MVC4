var AppData = {};

(function (self) {

    var urls = {
        get obtenerPaises() { return webroot + 'Catalogo/ObtenerPaises' },
        get obtenerMunicipios() { return webroot + 'Catalogo/ObtenerMunicipios' },
        get obtenerEstados() { return webroot + 'Catalogo/ObtenerEstados' },
    }

    self.obtenerPaises = function (filtros, callBack) {
        Utils.ejecutarAjax(
            filtros,
            urls.obtenerPaises,
            function (data) {
                if (callBack) callBack(data)
            }
        );
    };

    self.obtenerMunicipios = function (filtros, callBack) {
        Utils.ejecutarAjax(
            filtros,
            urls.obtenerMunicipios,
            function (data) {
                if (callBack) callBack(data)
            }
        );
    };

    self.obtenerEstados = function (filtros, callBack) {
        Utils.ejecutarAjax(
            filtros,
            urls.obtenerEstados,
            function (data) {
                if (callBack) callBack(data)
            }
        );
    };

})(AppData);