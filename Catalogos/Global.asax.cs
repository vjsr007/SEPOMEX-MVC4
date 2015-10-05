using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Catalogos.Controllers;
using System.Web.Script.Serialization;
using Catalogos.Models;
using System.Globalization;
using System.Threading;

namespace Catalogos
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            bool isAjaxCall = string.Equals("XMLHttpRequest", Context.Request.Headers["x-requested-with"], StringComparison.OrdinalIgnoreCase);

            Exception lastError = Server.GetLastError();
            Server.ClearError();

            if (isAjaxCall)
            {
                Context.Response.ContentType = "application/json";
                Context.Response.StatusCode = 200;
                Context.Response.Write(
                    new JavaScriptSerializer().Serialize(
                        new Respuesta { Error = true, Mensaje = lastError.Message }
                    )
                );
            }
            else
            {

                int statusCode = 0;

                if (lastError.GetType() == typeof(HttpException))
                {
                    statusCode = ((HttpException)lastError).GetHttpCode();
                }
                else
                {
                    statusCode = 500;
                }

                RouteData routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                routeData.Values.Add("action", "Index");
                routeData.Values.Add("statusCode", statusCode);
                routeData.Values.Add("exception", lastError);

                IController controller = new ErrorController();

                RequestContext requestContext = new RequestContext(new HttpContextWrapper(Context), routeData);

                controller.Execute(requestContext);
                Response.End();
            }
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                CultureInfo cultureInfo = (CultureInfo)this.Session["Culture"];
                if (cultureInfo == null)
                {
                    string langName = "en";

                    if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);

                    cultureInfo = new CultureInfo(langName);
                    this.Session["Culture"] = cultureInfo;
                }

                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            }
        }
    }
}