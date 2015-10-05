var Login = {};
(function (g) {
    "use strict";

    var ctrls =
    {
        $btnLogin: $('#btnLogin'),
        $frmLogin:$("#frmLogin"),
        $error:$("#error"),
        $frmErrores:$("#frmErrores"),
    };

    var init = function () {
        inicializarEventos();
    }

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
                webroot + "Login/ajaxLogin",
                function (data) {
                    if (data.Error || data.length<=0) {
                        ctrls.$btnLogin.val('Ingresar');
                        ctrls.$error.html("<span style='color:#cc0000'>Error:</span> " + (data.Mensaje == null ? "No tiene acceso a la aplicación" : data.Mensaje));
                    }
                    else {
                        window.location.href = "/Home/Index";
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

    $(init);
})(Login.Index = {});