﻿using Flogging.Web;
using mvc_todo.Controllers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace mvc_todo
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {                        
            var ex = Server.GetLastError();
            var httpContext = ((MvcApplication)sender).Context;

            int httpStatus;
            string errorControllerAction;

            Helper.GetHttpStatus(ex, out httpStatus);
            var notFoundUrl = "";
            switch (httpStatus)
            {
                case 404:
                    errorControllerAction = "NotFound";
                    notFoundUrl = httpContext.Request.Url.ToString();
                    break;
                default:
                    Helper.LogWebError(Constants.ProductName, Constants.LayerName, ex);
                    errorControllerAction = "Index";
                    break;
            }

            
            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            httpContext.Response.TrySkipIisCustomErrors = true;

            httpContext.Items["NotFoundUrl"] = notFoundUrl;

            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = errorControllerAction;

            var controller = new ErrorController();
            //controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        }                
    }
}
