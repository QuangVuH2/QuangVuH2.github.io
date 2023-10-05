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
    public class PostController : Controller
    {        
        PostDAO postDao = new PostDAO();
        TopicDAO topicDao = new TopicDAO();

        // GET: Admin/Post
        public ActionResult Index()
        {
            return View(postDao.getList("Index", "post"));
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDao.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.Topic = new SelectList(topicDao.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Slug = XString.Str_Slug(post.Title);
                post.PostTye = "post";
                post.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
                post.CreateAt = DateTime.Now;
                if(postDao.Insert(post) == 1)
                {
                    TempData["message"] = new XMessage("success", "Thêm mới thành công");
                }                
                return RedirectToAction("Index", "Post");
            }
            ViewBag.Topic = new SelectList(topicDao.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDao.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Topic = new SelectList(topicDao.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Slug = XString.Str_Slug(post.Title);
                post.PostTye = "post";
                post.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                post.UpdateAt = DateTime.Now;
                postDao.Update(post);
                return RedirectToAction("Index");
            }
            ViewBag.Topic = new SelectList(topicDao.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDao.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postDao.getRow(id);
            postDao.Delete(post);
            return RedirectToAction("Index");
        }

        // Các hàm viết thêm
        public ActionResult Trash()
        {
            return View(postDao.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            Post post = postDao.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            post.Status = (post.Status == 1) ? 2 : 1; // thay đổi trạng thái của sản phẩm từ 1 -> 2 và ngược lại
            post.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdateAt = DateTime.Now;
            postDao.Update(post);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Post");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            Post post = postDao.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            post.Status = 0; // thay đổi trạng thái rác = 0
            post.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdateAt = DateTime.Now;
            postDao.Update(post);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Post");
        }
        public ActionResult Restore_Trash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Post");
            }
            Post post = postDao.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Post");
            }
            post.Status = 2; // thay đổi trạng thái rác = 2
            post.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdateAt = DateTime.Now;
            postDao.Update(post);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Post");
        }
    }
}
