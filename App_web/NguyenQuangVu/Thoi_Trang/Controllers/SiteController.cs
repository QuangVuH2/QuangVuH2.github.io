using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MyClass.DAO;
using MyClass.Models;
using MyClass.View_Models;
using PagedList;

namespace Thoi_Trang.Controllers
{
    public class SiteController : Controller
    {
        // GET: Site
        LinkDAO linkDAO = new LinkDAO();
        ProductDAO productDAO = new ProductDAO();
        PostDAO postDAO = new PostDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        public ActionResult Index(string slug = null, int? page = null)
        {
            if (slug == null)
            {
                return this.Home();
            }
            else
            {
                Link link = linkDAO.getRow(slug);
                if (link != null)
                {
                    string typeLink = link.Type;
                    switch (typeLink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug, page);
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug);
                            }
                        case "page":
                            {
                                return this.PostPage(slug);
                            }
                        default:
                            {
                                return this.Error404(slug); 
                            }
                    }
                }
                else
                {
                    Product product = productDAO.getRow(slug);
                    if(product != null)
                    {
                        return this.ProductDetail(product);
                    }
                    else
                    {
                        Post post = postDAO.getRow(slug);
                        if(post != null)
                        {
                            return this.PostDetail(post);
                        }
                        else
                        {
                           return this.Error404(slug);
                        }
                    }
                }
            }           
        }

        public ActionResult Home()
        {
           List<Category> cat = categoryDAO.getListbyCategory(0);
           return View("Index", cat);
        }
        public ActionResult HomeProduct(int id)
        {
            Category category = categoryDAO.getRow(id);
            ViewBag.Category = category;
            // Lấy danh mục theo 3 cấp
            List<int> listcatId = new List<int>();
            listcatId.Add(id);
            List<Category> listCat2 = categoryDAO.getListbyCategory(id);
            if (listCat2.Count() > 0)
            {
                foreach (var Cat2 in listCat2)
                {
                    listcatId.Add(Cat2.Id);
                    List<Category> listCat3 = categoryDAO.getListbyCategory(Cat2.Id);
                    if (listCat3.Count() > 0)
                    {
                        foreach (var Cat3 in listCat3)
                        {
                            listcatId.Add(Cat3.Id);
                        }
                    }
                }
            }
            List<ProductView> list = productDAO.getListbyCatId(listcatId, 8);            
            return View("HomeProduct", list);
        }
        // Nhóm sản phẩm
        public ActionResult Product(int? page)
        {
            int pageNumber = page ?? 1; // trang hiện tại
            int pageSize = 8; // số mẫu tin hiển thị trên 1 trang
            return View("Product");
        }
        public ActionResult ProductCategory(string slug, int? page)
        {
            int pageNumber = page ?? 1; // trang hiện tại
            int pageSize = 9; // số mẫu tin hiển thị trên 1 trang
            Category category = categoryDAO.getRow(slug);
            ViewBag.Category = category;
            List<int> listcatId = new List<int>();
            listcatId.Add(category.Id);
            List<Category> listCat2 = categoryDAO.getListbyCategory(category.Id);
            if (listCat2.Count() > 0)
            {
                foreach (var Cat2 in listCat2)
                {
                    listcatId.Add(Cat2.Id);
                    List<Category> listCat3 = categoryDAO.getListbyCategory(Cat2.Id);
                    if (listCat3.Count() > 0)
                    {
                        foreach (var Cat3 in listCat3)
                        {
                            listcatId.Add(Cat3.Id);
                        }
                    }
                }
            }
            IPagedList<ProductView> list = productDAO.getPagelistCatId(listcatId, pageSize, pageNumber);
            return View("ProductCategory", list);
        }
        public ActionResult ProductDetail(Product product)
        {
            Category category = categoryDAO.getRow(product.CatId);
            ViewBag.Product = product;
            List<int> listcatId = new List<int>();
            listcatId.Add(category.Id);
            List<Category> listCat2 = categoryDAO.getListbyCategory(category.Id);
            if (listCat2.Count() > 0)
            {
                foreach (var Cat2 in listCat2)
                {
                    listcatId.Add(Cat2.Id);
                    List<Category> listCat3 = categoryDAO.getListbyCategory(Cat2.Id);
                    if (listCat3.Count() > 0)
                    {
                        foreach (var Cat3 in listCat3)
                        {
                            listcatId.Add(Cat3.Id);
                        }
                    }
                }
            }
            List<ProductView> list = productDAO.ListbyCatId(listcatId, product.Id, 4);
            return View("ProductDetail", list);
        }
        // Nhóm Post
        public ActionResult Post()
        {
            return View("Post");
        }
        public ActionResult PostTopic(string slug)
        {
            return View("PostTopic");
        }
        public ActionResult PostPage(string slug)
        {
            return View("PostPage");
        }
        public ActionResult PostDetail(Post post)
        {
            return View("PostDetail");
        }
        // Error 404
        public ActionResult Error404(string slug)
        {
            return View("Error404");
        }
    }
}