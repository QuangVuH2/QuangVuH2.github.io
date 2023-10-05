using MyClass.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;

namespace Thoi_Trang.Controllers
{
    public class ModuleController : Controller
    {
        private MenuDAO menuDAO = new MenuDAO();
        private SliderDAO sliderDAO = new SliderDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        // GET: Module
        public ActionResult ManinMeu()
        {
            List<Menu> list = menuDAO.getListbyParentid("mainmenu", 0);
            return View("ManinMeu", list);
        }
        public ActionResult ManinMeuSub(int id)
        {
            Menu menu = menuDAO.getRow(id);
            List<Menu> list = menuDAO.getListbyParentid("mainmenu", id);
            if (list.Count == 0)
            {
                // Không có cấp con
                return View("ManinMeuSub1", menu);
            }
            else
            {
                // Có cấp con
                ViewBag.Menu = menu;
                return View("ManinMeuSub2", list);
            }           
        }
        // Slide Show
        public ActionResult SlideShow()
        {
            List<Slider> list = sliderDAO.getListbyPosition("slidershow");
            return View("SlideShow", list);
        }
        // ListCategory
        public ActionResult ListCategory()
        {
            List<Category> list = categoryDAO.getListbyCategory(0);
            return View("ListCategory", list);
        }
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult SearchNameProduct()
        {
            return View();
        }
        public ActionResult login()
        {
            return View();
        }
    }
}