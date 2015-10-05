using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catalogos.Models.Catalogos;
using Catalogos.Models;
using System.Globalization;
using Catalogos.Utils;
using System.Web.Script.Serialization;
using Catalogos.Filters;
using Catalogos.IntranetService;

namespace Catalogos.Controllers
{
    [Seguridad]
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [ErrorHandlerJson]
        public JsonResult CountingEntities() {
            using (var client = new IntranetService.ServiceContractClient())
            {

                var res = client.ObtenerCountingEntities(new ObtenerCountingEntitiesReq { 
                    empty = 0
                });

                return Json(res.CountingEntities, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }

    }
}
