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
using MyClass.View_Models;
using System.Globalization;

namespace Thoi_Trang.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private MyDBContext db = new MyDBContext();
        private OrderDAO orderDAO = new OrderDAO();
        private OderdetailDAO OrderdetailDAO = new OderdetailDAO();
        // GET: Admin/Order
        public ActionResult Index()
        {            
            return View(orderDAO.getList("Index"));
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            ViewBag.OrderInfo = orderDAO.getRowId(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            List<OrderdetailInfo> listproduct = orderDAO.getRowId(id);
            ViewBag.OrderInfo = listproduct;
            ViewBag.totalProduct = listproduct.Count();
            Session["List"] = OrderdetailDAO.getlist(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                order.UpdateAt = DateTime.Now;
                order.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                if (orderDAO.Update(order) == 1)
                {
                    List<Oderdetail> listproduct = this.getSession();
                    foreach(var item in listproduct)
                    {
                        Oderdetail orderdetail = OrderdetailDAO.getRow(order.Id, item.ProductId);
                        orderdetail.Price = item.Price;
                        orderdetail.Qty = item.Qty;
                        orderdetail.Amount = item.Amount;
                        OrderdetailDAO.Update(orderdetail);
                    }
                    TempData["message"] = new XMessage("success", "Cập nhật đơn hàng thành công");
                }
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            ViewBag.listproduct = orderDAO.getRowId(order.Id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = orderDAO.getRow(id);
            int orderId = order.Id;
            List<Oderdetail> listproduct = OrderdetailDAO.getlist(order.Id);
            if(orderDAO.Delete(order) == 1)
            {
                foreach(var item in listproduct)
                {
                    Oderdetail orderdetail = OrderdetailDAO.getRow(orderId, item.ProductId);                    
                    OrderdetailDAO.Delete(orderdetail);
                }
                TempData["message"] = new XMessage("success", "Xóa đơn hàng thành công");
            }
            return RedirectToAction("Index");
        }

        // Các hàm viết thêm
        public ActionResult Trash()
        {
            return View(orderDAO.getList("Trash"));
        }

        public ActionResult DelTrash(int? id)
        {
            if(id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã đơn hàng không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if(order == null)
            {
                TempData["message"] = new XMessage("danger", "Mã đơn hàng không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            order.Status = 0;
            order.UpdateAt = DateTime.Now;
            order.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Order");
        }

        public ActionResult Restore_Trash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã đơn hàng không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mã đơn hàng không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            order.Status = 2;
            order.UpdateAt = DateTime.Now;
            order.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Khôi phục đơn hàng thành công");
            return RedirectToAction("Trash", "Order");
        }
        public JsonResult Status(int? id)
        {           
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                //TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return Json(new
                {
                    status = 0
                });
            }
            order.Status = (order.Status == 1) ? 2 : 1; // thay đổi trạng thái của sản phẩm từ 1 -> 2 và ngược lại
            order.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            order.UpdateAt = DateTime.Now;
            orderDAO.Update(order);
            //TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return Json(new
            {
                status = order.Status
            });
        }

        public List<Oderdetail> getSession()
        {
            if (Session["List"] == null)
            {
                return null;
            }
            else
            {
                return (List<Oderdetail>) Session["List"];
            }
        }

        public void UpdateProduct(int productid, int qty)
        {         
            if (!Session["List"].Equals(""))
            {
                List<Oderdetail> list = this.getSession();
                int vt = 0;
                foreach (var item in list)
                {
                    if (item.ProductId == productid)
                    {
                        list[vt].Qty = qty;
                        list[vt].Amount = list[vt].Price * qty;
                    }
                    vt++;
                }
                Session["List"] = list;
            }
        }
        public JsonResult UpdateOrderdetail(int productid, int qty)
        {           
            List<Oderdetail> listProduct = null;
            decimal tongtien = 0;
            this.UpdateProduct(productid, qty);
            listProduct = (List<Oderdetail>)Session["List"];
            foreach (var producItem in listProduct)
            {
                tongtien += producItem.Amount;
            }
            return Json(new 
            {
                totalPrice = tongtien,
                listProduct
            });
        }

        public void DeleteProduct(int productId)
        {
            if(!Session["List"].Equals(""))
            {
                List<Oderdetail> listProduct = (List<Oderdetail>) Session["List"];
                int vt = 0;
                foreach(var producItem in listProduct)
                {
                    if( producItem.ProductId == productId )
                    {
                        listProduct.RemoveAt(vt);
                        break;
                    }
                    vt++;
                }
                Session["List"] = listProduct;
            }
        }
        public JsonResult DeleteOrderdetail(int orderId, int productId)
        {            
            Oderdetail orderdetail = OrderdetailDAO.getRow(orderId, productId);
            this.DeleteProduct(productId);
            List<Oderdetail> list = (List<Oderdetail>)Session["List"];
            int soluongSp = list.Count();
            decimal tongtien = 0;
            if (OrderdetailDAO.Delete(orderdetail) == 1)
            {  
                foreach (var item in list)
                {
                    tongtien += item.Amount;
                }
            }
            
            if (soluongSp == 0)
            {
                Order order = orderDAO.getRow(orderId);
                orderDAO.Delete(order);
            }
            return Json(new {
                totalPrice = String.Format(new CultureInfo("vi-VN"), "{0:#,0}", tongtien),
                soluongSp,
                status = true
            });
        }
    }
}
