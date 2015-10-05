using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catalogos.Models.Login;
using Catalogos.Models;
using Catalogos.Filters;
using Catalogos.IntranetService;
using Catalogos.Models.Seguridad;

namespace Catalogos.Controllers
{
    public class LoginController : BaseController
    {

        public ActionResult Index()
        {
            return View();
            
        }

        [ValidJsonModel]
        [ErrorHandlerJson]
        public JsonResult ajaxLogin(UsuarioViewModel usu)
        {
            using (var client = new IntranetService.ServiceContractClient())
            {
                Usuarios Result = new Usuarios();
                var Usuarioweb = new UsuarioWeb();
                var f = new Usuario();

                f.Nombre = usu.nombre;
                f.Pass = usu.password;

                var res = client.ObtenerUsuario(new ObtenerUsuarioReq { 
                    FiltroUsuario = f
                });
                Result = res.Usuarios;

                if (Result.Count() > 0) {                    

                    Usuarioweb.Usuario = Result.FirstOrDefault();

                    UsuarioFuncion uf = new UsuarioFuncion
                    {
                        UsuarioID = res.Usuarios[0].UsuarioID,
                        RolActivo = true,
                        FuncionActivo = true,
                        Activo = true
                    };

                    var fn = client.ObtenerUsuarioFunciones(new ObtenerUsuarioFuncionesReq
                    {
                        FiltroUsuarioFuncion = uf
                    });

                    Usuarioweb.UsuarioFunciones = fn.UsuarioFunciones;
                    Session["UsuarioWeb"] = Usuarioweb;
                }

                return Json(Usuarioweb, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult logOut() {
            Session["UsuarioWeb"] = null;
            return View("Index");
        }

        public ActionResult NoAutorizado() {

            return View();
        }

    }
}
