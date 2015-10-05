using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catalogos.Models;

namespace Catalogos.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Index(int statusCode, Exception exception)
        {
            ErrorModel model = new ErrorModel { HttpStatusCode = statusCode, Exception = exception };
            Response.StatusCode = statusCode;
            return View(model);
        }
    }
}
