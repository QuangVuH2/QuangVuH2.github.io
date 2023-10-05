using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Thoi_Trang.Controllers
{
    public class KhachhangController : Controller
    {
        UserDAO userDao = new UserDAO();
        // GET: Khachhang
        public ActionResult DangNhap()
        {

            return View();
        }

        public ActionResult DangKy()
        {                        
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(User user, FormCollection filed)
        {  
            if(ModelState.IsValid)
            {
                string error = filed["UserName"];                
                if (Regex.IsMatch(filed["UserName"], "[^a-zA-Z0-9_]"))
                {
                    ViewBag.UserError = "<div class='text-danger'>"+ "Tên đăng nhập không có khoản trắng và có dấu." +"</div>";
                }
                if (filed["Password"] != filed["confirmPass"])
                {
                    ViewBag.PassError = "<div class='text-danger'>" + "Mặt khẩu không giống nhau vui lòng nhập lại." + "</div>";
                }
                else
                {
                    string sonha = filed["so-nha"];
                    string thanhpho = filed["cty"];
                    string quan = filed["quan-huyen"];
                    string phuong = filed["xa-phuong"];
                    string Address = sonha + "-" + phuong + "-" + quan + "-" + thanhpho;
                    user.Address = Address;
                    user.Roles = 5;                   
                    user.Password = XString.ToMD5(filed["Password"]);
                    user.Status = 1;
                    user.CreateAt = DateTime.Now;
                    if (userDao.Insert(user) == 1)
                    {
                        Session["UserCustomer"] = user.FullName;
                        Session["UserId"] = user.Id.ToString();
                        ViewBag.Success = "<div class='success col-lg-12'>" + "<h4 class='font-weight-semi-bold mb-4' style='text-align:center;'>Đăng ký thông tin thành công</h4>" + "</div>";                        
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection filed)
        {
            string username = filed["username"];
            string pass = XString.ToMD5(filed["pass"]);
            User Customer = userDao.getRowCustomer(username, pass);
            if (Customer != null) 
            {
                Session["UserCustomer"] = Customer.FullName;
                Session["UserId"] = Customer.Id;
                return Redirect("~/trang-chu");
            }
            else
            {
                ViewBag.StrError = "<div class='text-danger'>" + "Tài khoản không đúng." + "</div>";
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["UserCustomer"] = "";
            Session["UserId"] = "";
            if (!Session["MyCart"].Equals(""))
            {
                Session["MyCart"] = "";
            }
            return Redirect("~/dang-nhap");
        }
    }
}