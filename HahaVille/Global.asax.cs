using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using HahaVille.Models;
using Newtonsoft.Json;
using HahaVille.Authentication;

namespace HahaVille
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        //protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        //{
        //    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        //    if (authCookie != null)
        //    {

        //        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

        //        CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
        //        CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
        //        newUser.UserId = serializeModel.UserId;
        //        newUser.FirstName = serializeModel.FirstName;
        //        newUser.LastName = serializeModel.LastName;
        //        newUser.roles = serializeModel.roles;

        //        HttpContext.Current.User = newUser;
        //    }

        //}
    }
}
