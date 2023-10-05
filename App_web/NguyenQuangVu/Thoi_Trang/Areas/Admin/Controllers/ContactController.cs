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

namespace Thoi_Trang.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        private MyDBContext db = new MyDBContext();
        private ContactDAO contactDAO = new ContactDAO();
        // GET: Admin/Contact
        public ActionResult Index()
        {
            List<Contact> listContact = contactDAO.getList("Index");
            return View(listContact);
        }

        // GET: Admin/Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactDAO.getRow(id);
            contact.Status = 1;
            contactDAO.Update(contact);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Admin/Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,FullName,Phone,Email,Title,Detail,CreateAt,UpdateBy,UpdateAt,Status")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Admin/Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FullName,Phone,Email,Title,Detail,CreateAt,UpdateBy,UpdateAt,Status")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Admin/Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = contactDAO.getRow(id);
            if(contactDAO.Delete(contact) == 1)
            {
                TempData["message"] = new XMessage("success", "Xóa liên hệ thành công");
            }
            return RedirectToAction("Index");
        }

        // Các hàm viết thêm
        public JsonResult Status(int? id)
        {
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                //TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return Json(new
                {
                    status = 0
                });
            }
            contact.Status = (contact.Status == 1) ? 2 : 1; // thay đổi trạng thái của sản phẩm từ 1 -> 2 và ngược lại
            contact.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            contact.UpdateAt = DateTime.Now;
            contactDAO.Update(contact);
            //TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return Json(new
            {
                status = contact.Status
            });
        }

        public ActionResult Trash()
        {
            return View(contactDAO.getList("Trash"));
        }

        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Contact");
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Contact");
            }
            contact.Status = 0; // thay đổi trạng thái rác = 0
            contact.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            contact.UpdateAt = DateTime.Now;
            contactDAO.Update(contact);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Contact");
        }

        public ActionResult Restore_Trash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Contact");
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Contact");
            }
            contact.Status = 2; // thay đổi trạng thái rác = 2
            contact.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            contact.UpdateAt = DateTime.Now;
            contactDAO.Update(contact);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Contact");
        }
    }
}
