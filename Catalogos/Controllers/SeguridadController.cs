using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catalogos.Filters;
using Catalogos.Models.Seguridad;
using Catalogos.Models.Intranet;

namespace Catalogos.Controllers
{
    [Seguridad]
    public class SeguridadController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ErrorHandlerJson]
        public JsonResult ObtenerUsuarioFunciones(UsuarioFuncion uf)
        {
            using (var client = new IntranetEntities())
            {
                var fn = client.spObtenerUsuarioFunciones(uf.UsuarioID, uf.Nombre, uf.RolID, uf.RolActivo, uf.Activo, uf.FuncionActivo).ToList();

                return Json(fn, JsonRequestBehavior.AllowGet);
            }
        }

        [ErrorHandlerJson]
        public JsonResult ObtenerRolFunciones(RolFuncionViewModel rf)
        {
            using (var client = new IntranetEntities())
            {
                var fn = client.spObtenerRolFunciones(rf.RolID, null, true, rf.FuncionID, null, null, null, null, null, null, null).ToList();

                return Json(fn, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
