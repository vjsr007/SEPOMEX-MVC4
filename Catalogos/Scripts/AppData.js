var AppData = {};

(function (self) {

    self.obtenerPaises = function (filtros, callBack) {
        Utils.ejecutarAjax(
            filtros,
            webroot + 'Catalogo/ObtenerPaises',
            function (data) {
                if (callBack) callBack(data)
            }
        );
    };

})(AppData);