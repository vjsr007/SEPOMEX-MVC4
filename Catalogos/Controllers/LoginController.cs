using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catalogos.Models.Login;
using Catalogos.Models;
using Catalogos.Filters;
using Catalogos.Models.Seguridad;
using Catalogos.Models.Intranet;

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
             
            using (var client = new IntranetEntities())
            {
                var Usuarioweb = new UsuarioWeb();
                var f = new Usuario();

                f.Nombre = usu.nombre;
                f.Pass = usu.password;

                var res = client.spObtenerUsuario(usu.nombre, usu.password).ToList();

                if (res.Count() > 0) {                    

                    Usuarioweb.Usuario = res.FirstOrDefault();

                    UsuarioFuncion uf = new UsuarioFuncion
                    {
                        UsuarioID = res.FirstOrDefault().UsuarioID,
                        RolActivo = true,
                        FuncionActivo = true,
                        Activo = true
                    };

                    var fn = client.spObtenerUsuarioFunciones(uf.UsuarioID, uf.Nombre, uf.RolID, uf.RolActivo, uf.Activo, uf.FuncionActivo).ToList();

                    Usuarioweb.UsuarioFunciones = fn;
                    Session["UsuarioWeb"] = Usuarioweb;
                }

                if (Usuarioweb.Usuario == null) throw new Exception("Usuario y/o Contraseña incorrectos.");

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
