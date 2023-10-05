using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace Thoi_Trang.Controllers
{
    public class LienheController : Controller
    {
        UserDAO userDAO = new UserDAO();
        ContactDAO contactDAO = new ContactDAO();
        // GET: Lienhe
        public ActionResult Index()
        {
            if (!Session["UserCustomer"].Equals(""))
            {
                int userId = (int)Session["UserId"];
                ViewBag.infoUser = userDAO.getRowInfor(userId);
            }
            return View();
        }

        public ActionResult SendContact(FormCollection filed)
        {
            Contact contact = new Contact();            
            if(!Session["UserCustomer"].Equals(""))
            {
                contact.UserId = (int)Session["UserId"];
                contact.FullName = filed["fullName"];
                contact.Email = filed["Email"];
                contact.Phone = filed["Phone"];
                
            }
            else
            {
                contact.UserId = 0;
                contact.FullName = filed["fullName"];
                contact.Email = filed["Email"];
                contact.Phone = filed["Phone"];
            }
            contact.Title = filed["Title"];
            contact.Detail = filed["Detail"];
            contact.CreateAt = DateTime.Now;
            contact.Status = 2;
            if(contactDAO.Insert(contact) == 1)
            {
                TempData["message"] = new XMessage("success", "Cảm ơn bạn đã liên hệ với chúng tôi.");
            }
            return Redirect("~/lien-he");
        }
    }
}