using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using MyClass.View_Models;
using PagedList;
using System.Drawing.Printing;

namespace Thoi_Trang.Controllers
{
    public class TimkiemController : Controller
    {
        // GET: Tiemkiem
        ProductDAO productDao = new ProductDAO();
        CategoryDAO categoryDao = new CategoryDAO();       

        public ActionResult SearchResult()
        {
            IPagedList<ProductView> listcatpro = (IPagedList<ProductView>)Session["Search"];
            return View(listcatpro);
        }

        [HttpPost]
        public ActionResult SearchResult(FormCollection filed, int? page)
        {
            int pageNumber = page ?? 1; // trang hiện tại
            int pageSize = 9; // số mẫu tin hiển thị trên 1 trang           
            string searchName = filed["search"];
            string slug = XString.Str_Slug(searchName);
            Category category = categoryDao.getRow(slug);
            
            if (category != null)
            {
                List<int> listcatId = new List<int>();
                listcatId.Add(category.Id);
                List<Category> listCat2 = categoryDao.getListbyCategory(category.Id);
                if (listCat2.Count() > 0)
                {
                    foreach (var Cat2 in listCat2)
                    {
                        listcatId.Add(Cat2.Id);
                        List<Category> listCat3 = categoryDao.getListbyCategory(Cat2.Id);
                        if (listCat3.Count() > 0)
                        {
                            foreach (var Cat3 in listCat3)
                            {
                                listcatId.Add(Cat3.Id);
                            }
                        }
                    }
                }
                IPagedList<ProductView> list = productDao.getPagelistCatId(listcatId, pageSize, pageNumber);
                Session["Search"] = list;
                Session["SearchName"] = list;
                return View("SearchResult", list);
            }
            else
            {
                return View("");
            }
        }       
        public ActionResult SearchProName(FormCollection filed, int? page)
        {
            
            int pageNumber = page ?? 1; // trang hiện tại
            int pageSize = 9; // số mẫu tin hiển thị trên 1 trang 
            IPagedList<ProductView> listCat = (IPagedList<ProductView>)Session["SearchName"];
            //Session["Search"] = "";
            string productName = filed["productName"];
            IPagedList<ProductView> listProduct = null;
            if (listCat != null)
            {
                listProduct = listCat
                                 .Where(m => m.Name.Contains(productName))
                                 .OrderByDescending(m => m.CreateAt)
                                 .ToPagedList(pageNumber, pageSize);
                Session["Search"] = listProduct;
                return RedirectToAction("SearchResult", listProduct);
            }
            return RedirectToAction("SearchResult");
        }

    }
}