using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Catalogos.Filters
{
    public class LoadJSFile : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            var controller = filterContext.RouteData.Values["controller"];
            var viewResult = filterContext.Result as ViewResultBase;
            var viewName = viewResult.ViewName;
            var convencion = String.Format("Scripts/Views/{0}/{0}.{1}.js", controller, viewName);

            var url = VirtualPathUtility.ToAbsolute(String.Format("~/{0}", convencion));

            var pathJS = filterContext.HttpContext.Server.MapPath(
                        string.Format(
                            "~/{0}",
                            convencion
                        )
                    );

            if (File.Exists(pathJS))
            {
                filterContext.HttpContext.Response.Write(String.Format("<script src='{0}'></script>", url));
            }
        }
    }
}