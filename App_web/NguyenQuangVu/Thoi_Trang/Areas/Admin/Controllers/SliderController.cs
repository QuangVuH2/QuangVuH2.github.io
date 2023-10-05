using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace Thoi_Trang.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        private MyDBContext db = new MyDBContext();       
        private SliderDAO sliderDAO = new SliderDAO(); 
        // GET: Admin/Slider
        public ActionResult Index()
        {
            return View(sliderDAO.getList("Index"));
        }

        // GET: Admin/Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Admin/Slider/Create
        public ActionResult Create()
        {
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/Slider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider)
        {                       
            if (ModelState.IsValid)
            {
                slider.Url = XString.Str_Slug(slider.Name);
                if(slider.Orders == null)
                {
                    slider.Orders = 1;
                }
                else
                {
                    slider.Orders += 1;
                }
                var Img = Request.Files["fileimg"];
                string[] FileExtention = { ".jpg", ".jpeg", ".png", ".gif" };
                if (Img != null && Img.ContentLength != 0)
                {
                    if (FileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        // Thực hiện up load file
                        string ImgName = slider.Url + Img.FileName.Substring(Img.FileName.LastIndexOf("."));
                        slider.Img = ImgName; // Lưu vào csdl
                        string PATH_Img = Path.Combine(Server.MapPath("~/Public/images/slideshow"), ImgName);
                        Img.SaveAs(PATH_Img); // Lưu lên server                       
                    }
                }
                slider.Position = "slidershow";
                slider.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
                slider.CreateAt = DateTime.Now;
                sliderDAO.Insert(slider);
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index", "Slider");
            }           
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // GET: Admin/Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // POST: Admin/Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                slider.Url = XString.Str_Slug(slider.Name);
                if (slider.Orders == null)
                {
                    slider.Orders = 1;
                }
                else
                {
                    slider.Orders += 1;
                }
                var Img = Request.Files["fileimg"];
                string[] FileExtention = { ".jpg", ".jpeg", ".png", ".gif" };
                if (Img != null && Img.ContentLength != 0)
                {
                    if (FileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        // Thực hiện up load file
                        string ImgName = slider.Url + Img.FileName.Substring(Img.FileName.LastIndexOf("."));
                        // Xóa hình                       
                        // Kiểm tra hình có tồn tại hay không
                        if (slider.Img != null)
                        {
                            string Del_Path = Path.Combine(Server.MapPath("~/Public/images/slideshow"), slider.Img);
                            System.IO.File.Delete(Del_Path);
                        }
                        slider.Img = ImgName; // Lưu vào csdl
                        string PATH_Img = Path.Combine(Server.MapPath("~/Public/images/slideshow"), ImgName);
                        Img.SaveAs(PATH_Img); // Lưu lên server                       
                    }
                }
                slider.Position = "slidershow";
                slider.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
                slider.CreateAt = DateTime.Now;
                sliderDAO.Update(slider);
                TempData["message"] = new XMessage("success", "Update thành công");
                return RedirectToAction("Index", "Slider");
            }
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // GET: Admin/Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            if (sliderDAO.Delete(slider) == 1)
            {
                TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");
            }
            return RedirectToAction("Trash", "Slider");
        }

        // POST: Admin/Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = sliderDAO.getRow(id);
            // Xóa hình                       
            // Kiểm tra hình có tồn tại hay không
            if (slider.Img != null)
            {
                string Del_Path = Path.Combine(Server.MapPath("~/Public/images/slideshow"), slider.Img);
                System.IO.File.Delete(Del_Path);
            }
            if (sliderDAO.Delete(slider) == 1)
            {
                TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");
            }
            return RedirectToAction("Trash", "Slider");
        }

        // Hàm viết thêm
        public ActionResult Trash()
        {
            return View(sliderDAO.getList("Trash"));
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã slider không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = (slider.Status == 1) ? 2 : 1; // thay đổi trạng thái của sản phẩm từ 1 -> 2 và ngược lại
            slider.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            slider.UpdateAt = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Slider");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã slider không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = 0; // thay đổi trạng thái rác = 0
            slider.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            slider.UpdateAt = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Slider");
        }

        public ActionResult Restore_Trash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã slider không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = 2; // thay đổi trạng thái rác = 0
            slider.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            slider.UpdateAt = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Slider");
        }
    }
}
