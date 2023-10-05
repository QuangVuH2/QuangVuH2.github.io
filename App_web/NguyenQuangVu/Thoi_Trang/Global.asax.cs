using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Thoi_Trang
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start()
        {
            Session["UserAdmin"] = "";
            Session["UserId"] = "1";
            Session["MyCart"] = "";
            Session["List"] = "";
            // người dùng
            Session["UserCustomer"] = "";
            // tìm kiếm
            Session["Search"] = "";
            Session["SearchName"] = "";
        }
    }
}
