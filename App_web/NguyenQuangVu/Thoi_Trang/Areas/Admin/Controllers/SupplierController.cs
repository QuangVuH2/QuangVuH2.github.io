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

namespace Thoi_Trang.Areas.Admin.Controllers
{
    public class SupplierController : BaseController
    {
       // private MyDBContext db = new MyDBContext();
        SupplierDAO supplierDAO = new SupplierDAO();
        LinkDAO linkDAO = new LinkDAO();

        // GET: Admin/Supplier
        public ActionResult Index()
        {
            return View(supplierDAO.getList("Index"));
        }

        // GET: Admin/Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Admin/Supplier/Create
        public ActionResult Create()
        {           
            //Tạo danh sách mới
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                // xử lý các trường null trong csdl
                supplier.Slug = XString.Str_Slug(supplier.Name);
                if (supplier.Orders == null)
                {
                    supplier.Orders = 1;
                }
                else
                {
                    supplier.Orders += 1;
                }               
                var Img = Request.Files["fileimg"];
                string[] FileExtention = { ".jpg", ".jpeg", ".png", ".gif" };
                if (Img != null && Img.ContentLength != 0)
                {
                    if (FileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        // Thực hiện up load file
                        string ImgName = supplier.Slug + Img.FileName.Substring(Img.FileName.LastIndexOf("."));
                        supplier.Img = ImgName; // Lưu vào csdl
                        string PATH_Img = Path.Combine(Server.MapPath("~/Public/images/suppliers"), ImgName);
                        Img.SaveAs(PATH_Img); // Lưu lên server                       
                    }
                }
                supplier.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
                supplier.CreateAt = DateTime.Now;
                if(supplierDAO.Insert(supplier) == 1)
                {                   
                    Link link = new Link();
                    link.Slug = supplier.Slug;
                    link.TableId = supplier.Id;
                    link.Type = "supplier";
                    linkDAO.Insert(link);
                    TempData["message"] = new XMessage("success", "Thêm thành công");
                }               
                return RedirectToAction("Index", "Supplier");
            }          
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                if (supplier.Orders == null)
                {
                    supplier.Orders = 1;
                }
                else
                {
                    supplier.Orders += 1;
                }
                supplier.Slug = XString.Str_Slug(supplier.Name);
                var Img = Request.Files["fileimg"];
                string[] FileExtention = { ".jpg", ".jpeg", ".png", ".gif" };
                if (Img != null && Img.ContentLength != 0)
                {
                    if (FileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        // Thực hiện up load file
                        string ImgName = supplier.Slug + Img.FileName.Substring(Img.FileName.LastIndexOf("."));
                        // Xóa hình                       
                        // Kiểm tra hình có tồn tại hay không
                        if (supplier.Img != null)
                        {
                            string Del_Path = Path.Combine(Server.MapPath("~/Public/images/suppliers"), supplier.Img);
                            System.IO.File.Delete(Del_Path);
                        }
                        supplier.Img = ImgName; // Lưu vào csdl
                        string PATH_Img = Path.Combine(Server.MapPath("~/Public/images/suppliers"), ImgName);
                        Img.SaveAs(PATH_Img); // Lưu lên server                       
                    }
                }
                supplier.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                supplier.UpdateAt = DateTime.Now;                               
                if(supplierDAO.Update(supplier) == 1)
                {
                      Link link = linkDAO.getRow(supplier.Id, "supplier");
                      link.Slug = supplier.Slug;
                      linkDAO.Update(link);                   
                    TempData["message"] = new XMessage("success", "Cập nhật thành công");
                }
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = supplierDAO.getRow(id);
            Link link = linkDAO.getRow(supplier.Id, "supplier");
            // Xóa hình                       
            // Kiểm tra hình có tồn tại hay không
            if (supplier.Img != null)
            {
                string Del_Path = Path.Combine(Server.MapPath("~/Public/images/suppliers"), supplier.Img);                
                System.IO.File.Delete(Del_Path);
            }
            if(supplierDAO.Delete(supplier) == 1)
            {
                TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");
                linkDAO.Delete(link);
            }           
            return RedirectToAction("Trash", "Supplier");
        }

        public ActionResult Trash()
        {
            return View(supplierDAO.getList("Trash"));
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã nhà sản xuất không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            supplier.Status = (supplier.Status == 1) ? 2 : 1; // thay đổi trạng thái của sản phẩm từ 1 -> 2 và ngược lại
            supplier.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdateAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Supplier");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã nhà sản xuất không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            supplier.Status = 0; // thay đổi trạng thái rác = 0
            supplier.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdateAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Supplier");
        }

        public ActionResult Restore_Trash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã nhà sản xuất không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            supplier.Status = 2; // thay đổi trạng thái rác = 0
            supplier.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdateAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Supplier");
        }
    }
}
