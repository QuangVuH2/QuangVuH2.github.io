using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Text;
using System.Web.WebPages;
using MyClass.DAO;
using MyClass.Models;
using MyClass.View_Models;


namespace Thoi_Trang.Controllers
{
    public class GiohangController : Controller
    {
        // GET: Giohang
        ProductDAO productDAO = new ProductDAO();
        OrderDAO orderDAO = new OrderDAO();
        OderdetailDAO orderdetailDAO = new OderdetailDAO();
        XCart xcart = new XCart();
        UserDAO userDAO = new UserDAO();
        public ActionResult Index()
        {
            List<CartItem> listcart = xcart.GetCart();
            return View("Index", listcart);
        }
        public JsonResult CartAdd(int productid, int quantity = 1)
        {
            Product product = productDAO.getRow(productid);
            CartItem cartitem = new CartItem(product.Id, product.Name, product.Img, product.PriceSale, 1);
            // thêm giỏ hàng
            List<CartItem> listcart = xcart.AddCart(cartitem, productid, quantity);
            int soluong = listcart.Count();
            return Json(new
            {
                qty = soluong,
                status = true
            });
        }
        public JsonResult CartDel(int productid)
        {
            xcart.DelCart(productid);
            List<CartItem> listcart = (List<CartItem>)Session["MyCart"];
            decimal tongtieng = 0;
            foreach (var item in listcart)
            {
                tongtieng += item.Amuont;
            }
            return Json(new
            {
                status = true,
                totalPrice = tongtieng
            });
        }
        public JsonResult CartUpdate(int productid, int qty)
        {
            List<CartItem> listcart = null;
            decimal tongtieng = 0;
            int quantity = (int)qty;
            xcart.UpdateCart(productid, quantity);
            listcart = (List<CartItem>)Session["MyCart"];
            foreach (var item in listcart)
            {

                tongtieng += item.Amuont;
            }
            return Json(new
            {
                totalPrice = String.Format(new CultureInfo("vi-VN"), "{0:#,0}", tongtieng),
                listcart
            });
        }
        public JsonResult CartClearAll()
        {
            bool trangthai = false;
            if (!Session["MyCart"].Equals(""))
            {
                // List<CartItem> listcart = (List<CartItem>)Session["MyCart"];
                // listcart.Clear();
                Session["MyCart"] = "";
                trangthai = true;
            }
            return Json(new
            {
                status = trangthai
            });
        }
        public ActionResult ThanhToan()
        {
            if (!Session["UserCustomer"].Equals(""))
            {
                int CustomerId = (int)Session["UserId"];
                ViewBag.Customer = userDAO.getRowInfor(CustomerId);
            }
            List<CartItem> listcart = xcart.GetCart();
            return View(listcart);
        }

        [HttpPost]
        public ActionResult ThanhToan(FormCollection filed)
        {
            int customerId = 0;
            string receiverName = "";
            string email = "";
            string phone = "";
            string sonha = "";
            string thanhpho = "";
            string quan = "";
            string phuong = "";
            string Address = "";
            string note = "";
            string checkbox = filed["checkboxid"];
            bool shiptocheck = checkbox.IsBool();
            List<CartItem> listcart = xcart.GetCart();
            Order order = new Order();
            if (!Session["UserCustomer"].Equals(""))
            {
                int CustomerId = (int)Session["UserId"];
                ViewBag.Customer = userDAO.getRowInfor(CustomerId);
            }
            if (shiptocheck == false)
            {
                if (!Session["UserCustomer"].Equals(""))
                {
                    customerId = (int)Session["UserId"];
                    receiverName = filed["ordererName"];
                    email = filed["E-mail"];
                    phone = filed["ordererPhone"];
                    Address = filed["ordererAddress"];
                    note = filed["note"];
                }
                if (filed["ordererName"].Equals("") || filed["ordererPhone"].Equals(""))
                {
                    TempData["message"] = new XMessage("danger", "Bạn vui lòng điền đầy đủ thông tin.");
                }
                sonha = filed["so-nha"];
                thanhpho = filed["cty"];
                quan = filed["quan-huyen"];
                phuong = filed["xa-phuong"];               
                if (phuong.Equals("") && quan.Equals("") && thanhpho.Equals(""))
                {
                    TempData["message"] = new XMessage("danger", "Bạn vui lòng điền thông tin địa chỉ nhận hàng.");
                }
                else
                {
                    receiverName = filed["ordererName"];
                    email = filed["E-mail"];
                    phone = filed["ordererPhone"];                   
                    Address = string.Concat(sonha, "-", phuong, "-", quan, "-", thanhpho);
                    note = filed["note"];
                }
            }
            else
            {
                sonha = filed["so-nha1"];
                thanhpho = filed["cty1"];
                quan = filed["quan-huyen1"];
                phuong = filed["xa-phuong1"];
                if (filed["ReceiverName"].Equals("") || filed["ReceiverPhone"].Equals(""))
                {
                    TempData["message"] = new XMessage("danger", "Bạn vui lòng điền đầy đủ thông tin người nhận hàng.");
                }                            
                if (phuong.Equals("") && quan.Equals("") && thanhpho.Equals(""))
                {
                    TempData["message"] = new XMessage("danger", "Bạn vui lòng điền thông tin địa chỉ nhận hàng.");                   
                }
                else
                {
                    if (!Session["UserCustomer"].Equals(""))
                    {
                        customerId = (int)Session["UserId"];
                    }
                    receiverName = filed["ReceiverName"];
                    email = filed["ReceiverEmail"];
                    phone = filed["ReceiverPhone"];
                    Address = string.Concat(sonha, "-", phuong, "-", quan, "-", thanhpho);
                    note = filed["note"];
                }
            }

            if (!(receiverName.Equals("")) && !(Address.Equals("")) && !(phone.Equals("")))
            {
                order.UserId = customerId;
                order.ReceiverName = receiverName;
                order.ReceiverAddress = Address;
                order.ReceiverEmail = email;
                order.ReceiverPhone = phone;
                order.Note = note;
                order.Status = 2;
                order.CreateAt = DateTime.Now;
                if (orderDAO.Insert(order) == 1)
                {
                    // thêm chi tiết đơn hàng                                   
                    foreach (CartItem item in listcart)
                    {
                        Oderdetail orderdetail = new Oderdetail();
                        orderdetail.OrderId = order.Id;
                        orderdetail.ProductId = item.productId;
                        orderdetail.Price = item.Price;
                        orderdetail.Qty = item.Qty;
                        orderdetail.Amount = item.Amuont;
                        orderdetailDAO.Insert(orderdetail);
                    }
                    listcart.Clear();
                    Session["MyCart"] = "";
                    return View("DatHang");
                }
            }
            return View("ThanhToan", listcart);
        }
        public ActionResult DatHang(FormCollection filed)
        {
            return View();
        }
    }
}