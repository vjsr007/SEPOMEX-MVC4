using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catalogos.Filters;
using Catalogos.IntranetService;

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
            using (var client = new IntranetService.ServiceContractClient())
            {
                var fn = client.ObtenerUsuarioFunciones(new ObtenerUsuarioFuncionesReq
                {
                    FiltroUsuarioFuncion = uf
                });

                return Json(fn.UsuarioFunciones, JsonRequestBehavior.AllowGet);
            }
        }

        [ErrorHandlerJson]
        public JsonResult ObtenerRolFunciones(RolFuncion rf)
        {
            using (var client = new IntranetService.ServiceContractClient())
            {
                var fn = client.ObtenerRolFunciones(new ObtenerRolFuncionesReq
                {
                    FiltroRolFuncion = rf
                });

                return Json(fn.RolFunciones, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
