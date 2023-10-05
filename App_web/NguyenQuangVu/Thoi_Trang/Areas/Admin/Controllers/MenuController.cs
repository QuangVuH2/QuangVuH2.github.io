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
    public class MenuController : BaseController
    {
        private MyDBContext db = new MyDBContext();
        CategoryDAO categoryDAO = new CategoryDAO();
        PostDAO postDAO = new PostDAO();
        TopicDAO topicDAO = new TopicDAO();
        MenuDAO menuDAO = new MenuDAO();
        // GET: Admin/Menu
        public ActionResult Index()
        {
            ViewBag.listCategory = categoryDAO.getList("Index");
            ViewBag.listPage = postDAO.getList("Index", "page");
            ViewBag.listTopic = topicDAO.getList("Index");
            List<Menu> menuList = menuDAO.getList("Index");
            return View("Index", menuList);
        }

        // GET: Admin/Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Admin/Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Menu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            // Kiểm tra category
            if (!string.IsNullOrEmpty(form["AddCategory"]))
            {
                if (!string.IsNullOrEmpty(form["itemCat"]))
                {
                    var listItem = form["itemCat"];
                    var listArray = listItem.Split(',');
                    foreach (var item in listArray)
                    {
                        int id = int.Parse(item);
                        Category category = categoryDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = category.Name;
                        menu.Link = category.Slug;
                        menu.TableId = category.Id;
                        menu.TypeMenu = "category";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Orders = 0;
                        menu.Status = 2;
                        menu.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
                        menu.CreateAt = DateTime.Now;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm mới thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn danh mục sản phẩm");
                }
            }
            // Kiểm tra Topic
            if (!string.IsNullOrEmpty(form["AddTopic"]))
            {
                if (!string.IsNullOrEmpty(form["itemTopic"]))
                {
                    var listItem = form["itemTopic"];
                    var listArray = listItem.Split(',');
                    foreach (var item in listArray)
                    {
                        int id = int.Parse(item);
                        Topic topic = topicDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = topic.Name;
                        menu.Link = topic.Slug;
                        menu.TableId = topic.Id;
                        menu.TypeMenu = "topic";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Orders = 0;
                        menu.Status = 2;
                        menu.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
                        menu.CreateAt = DateTime.Now;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm mới thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn bài viết");
                }
            }
            // Page
            if (!string.IsNullOrEmpty(form["AddPage"]))
            {
                if (!string.IsNullOrEmpty(form["itemPage"]))
                {
                    var listItem = form["itemPage"];
                    var listArray = listItem.Split(',');
                    foreach (var item in listArray)
                    {
                        int id = int.Parse(item);
                        Post post = postDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = post.Title;
                        menu.Link = post.Slug;
                        menu.TableId = post.Id;
                        menu.TypeMenu = "page";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Orders = 0;
                        menu.Status = 2;
                        menu.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
                        menu.CreateAt = DateTime.Now;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm mới thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn bài viết");
                }
            }
            // Thêm custom
            if (!string.IsNullOrEmpty(form["AddCustom"]))
            {
                if (!string.IsNullOrEmpty(form["name"]) && !string.IsNullOrEmpty(form["link"]))
                {
                    List<Menu> listMenu = menuDAO.getListbyParentid("mainmenu");
                    Menu menu = new Menu();
                    int vt = 0;
                    int a = 0;
                    int order = 1;
                    if (!listMenu.Equals(""))
                    {
                        foreach (var item in listMenu)
                        {
                            if (!item.Orders.Equals(""))
                            {
                                vt++;
                                a = (int)item.Orders;
                            }
                        }
                        order = a + 1;
                    }
                    menu.Name = form["name"];
                    menu.Link = form["link"];
                    menu.TypeMenu = "custom";
                    menu.Position = form["Position"];
                    menu.ParentId = 0;
                    menu.Orders = order;
                    menu.Status = 2;
                    menu.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
                    menu.CreateAt = DateTime.Now;
                    menuDAO.Insert(menu);
                    TempData["message"] = new XMessage("success", "Thêm mới thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa nhập đủ thông tin");
                }
            }
            return RedirectToAction("Index", "Menu");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: Admin/Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                if(menu.ParentId == null)
                {
                    menu.ParentId = 0;
                }
                if(menu.Orders == null)
                {
                    menu.Orders = 0;
                }
                else
                {
                    menu.Orders += 1;
                }
                menu.UpdateAt = DateTime.Now;
                menu.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                menuDAO.Update(menu);
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name", 0);
            return View(menu);
        }

        // GET: Admin/Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Các hàm viết thêm
        public ActionResult Trash()
        {
            return View(menuDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Menu");
            }
            menu.Status = (menu.Status == 1) ? 2 : 1; // thay đổi trạng thái của sản phẩm từ 1 -> 2 và ngược lại
            menu.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            menu.UpdateAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Menu");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Menu");
            }
            menu.Status = 0; // thay đổi trạng thái rác = 0
            menu.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            menu.UpdateAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Menu");
        }
        public ActionResult Restore_Trash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Menu");
            }
            menu.Status = 2; // thay đổi trạng thái rác = 2
            menu.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            menu.UpdateAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Menu");
        }
    }
}
