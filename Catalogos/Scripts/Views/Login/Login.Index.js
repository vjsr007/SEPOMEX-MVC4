var Login = {};
(function (self) {
    "use strict";

    var formID = "#frmLogin"
    var $form = function (selector) { return $(formID).find(selector); }
    var ctrls ={
        get $btnLogin() { return $form('#btnLogin') },
        get $frmLogin() { return $(formID) },
        get $error() { return $form("#error") },
        get $frmErrores() { return $form("#frmErrores") },
    };
    var urls ={
        get login(){ return webroot + "Login/ajaxLogin"},
    }
    var HomeUrl = "/Home/Index";

    var inicializarEventos = function(){
        ctrls.$btnLogin.click(function (e) {
            e.preventDefault();
            loginApp();
        });
    };

    var loginApp = function(){
        var post = { usu: Utils.url2json(ctrls.$frmLogin.serialize().toString(), false) };

        var isValid = ctrls.$frmLogin.valid();

        if (isValid) {
            Utils.ejecutarAjax(
                post,
                urls.login,
                function (data) {
                    if (data.Error || data.length<=0) {
                        ctrls.$btnLogin.val('Ingresar');
                        ctrls.$error.html("<span style='color:#cc0000'>Error:</span> " + (data.Mensaje == null ? "No tiene acceso a la aplicación" : data.Mensaje));
                    }
                    else {
                        window.location.href = HomeUrl;
                    }
                }
            );
        }
        else {
            Utils.mostrarMensaje("Error en datos de entrada", ctrls.$frmErrores.html(), null, function () { });
            ctrls.$frmErrores.hide();
        }
        return false;
    };

    var init = function () {
        inicializarEventos();
    }

    $(init);
})(Login.Index = {});