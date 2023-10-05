using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using System.IO;
using MyClass.View_Models;

namespace Thoi_Trang.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private ProductDAO productDAO = new ProductDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private SupplierDAO supplierDAO = new SupplierDAO();
        //Category category = new Category();

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(productDAO.getList("Index"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<MyClass.View_Models.ProductView> product = productDAO.getList(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {          
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListSup = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {           
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);                
                var Img = Request.Files["fileimg"];
                string[] FileExtention = { ".jpg", ".jpeg",".png", ".gif" };
                if (Img != null && Img.ContentLength != 0)
                {
                    if(FileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        // Thực hiện up load file
                        string ImgName = product.Slug + Img.FileName.Substring(Img.FileName.LastIndexOf("."));
                        product.Img = ImgName; // Lưu vào csdl
                        string PATH_Img = Path.Combine(Server.MapPath("~/Public/images/products"), ImgName);
                        Img.SaveAs(PATH_Img); // Lưu lên server                       
                    }
                }
                product.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
                product.CreateAt = DateTime.Now;
                productDAO.Insert(product);
                TempData["message"] = new XMessage("success", "Thêm sản phẩm thành công");
                return RedirectToAction("Index", "Product");
            }
            ViewBag.ListCat = new SelectList(productDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListSup = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }          
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListSup = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);
                var Img = Request.Files["fileimg"];
                string[] FileExtention = { ".jpg", ".jpeg", ".png", ".gif" };
                if (Img != null && Img.ContentLength != 0)
                {
                    if (FileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        // Thực hiện up load file
                        string ImgName = product.Slug + Img.FileName.Substring(Img.FileName.LastIndexOf("."));
                        // Xóa hình
                        string Del_Path = Path.Combine(Server.MapPath("~/Public/images/products"), product.Img);
                        // Kiểm tra hình có tồn tại hay không
                        if(System.IO.File.Exists(Del_Path))
                        {
                            System.IO.File.Delete(Del_Path);
                        }
                        product.Img = ImgName; // Lưu vào csdl
                        string PATH_Img = Path.Combine(Server.MapPath("~/Public/images/products"), ImgName);
                        Img.SaveAs(PATH_Img); // Lưu lên server                       
                    }
                }                
                product.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
                product.CreateAt = DateTime.Now;              
                TempData["message"] = new XMessage("success", "Cập nhật sản phẩm thành công");               
                productDAO.Update(product);
                return RedirectToAction("Index");
            }            
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListSup = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if(productDAO.Delete(product) == 1)
            {
                TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");
            }
            return RedirectToAction("Trash", "Product");            
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            // Xóa hình ảnh
            string Del_Path = Path.Combine(Server.MapPath("~/Public/images/products"), product.Img);
            // Kiểm tra hình có tồn tại hay không
            if (System.IO.File.Exists(Del_Path))
            {
                System.IO.File.Delete(Del_Path);
            }
            productDAO.Delete(product);
            return RedirectToAction("Index");
        }

        // Các hàm viết thêm
        public ActionResult Trash()
        {
            return View(productDAO.getList("Trash"));
        }
        public ActionResult Status(int id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1; // thay đổi trạng thái của sản phẩm từ 1 -> 2 và ngược lại
            product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdateAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult DelTrash(int id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = 0; // thay đổi trạng thái rác = 0
            product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdateAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Restore_Trash(int id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            product.Status = 2; // thay đổi trạng thái rác = 2
            product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdateAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Product");
        }
    }
}
