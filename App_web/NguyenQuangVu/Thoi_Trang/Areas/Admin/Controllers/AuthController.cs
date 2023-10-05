using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace Thoi_Trang.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        UserDAO userDAO = new UserDAO();
        // GET: Admin/Auther
        public ActionResult DangNhap()
        {
            ViewBag.StrError = "";
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection filed)
        {
            string user = filed["username"];
            string pass = XString.ToMD5(filed["pass"]);
            string error = "";
            User user_row = userDAO.getRow(user, pass);
            if (user_row != null )
            {
                Session["UserAdmin"] = user_row.UserName;
                Session["UserId"] = user_row.Id.ToString();
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                error = "Tài khoản không đúng";
            }
            ViewBag.StrError = "<div class='text-danger'>"+ error +"</div>";
            return View();
        }

        public ActionResult DangXuat()
        {
            Session["UserAdmin"] = "";
            Session["UserId"] = "";
            return Redirect("~/Admin/Login");
        }
    }
}