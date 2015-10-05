using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catalogos.Models;
using System.Transactions;
using System.Net;
using System.Web.Http;

namespace Catalogos.Filters
{
    public class ErrorHandlerJson : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            filterContext.Result = new JsonResult
            {
                Data = new Respuesta { Error = true, Mensaje = filterContext.Exception.Message  },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}