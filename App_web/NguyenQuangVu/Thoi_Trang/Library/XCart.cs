using MyClass.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Thoi_Trang
{
    public class XCart
    {
        public List<CartItem> AddCart(CartItem cartitem, int productid, int quantity)
        {
            List<CartItem> listcart;
            if (System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                cartitem.Qty = quantity;
                cartitem.Amuont = cartitem.Price * cartitem.Qty;
                listcart = new List<CartItem>();                
                listcart.Add(cartitem);
                System.Web.HttpContext.Current.Session["MyCart"] = listcart;
            }
            else
            {
                 listcart = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
                if (listcart.Where(m => m.productId == cartitem.productId).Count() != 0)
                {
                    int vt = 0;
                    foreach (var item in listcart)
                    {
                        if (item.productId == cartitem.productId)
                        {
                            listcart[vt].Qty += quantity;
                            listcart[vt].Amuont = listcart[vt].Price * listcart[vt].Qty;
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["MyCart"] = listcart;
                }
                else
                {
                    cartitem.Qty = quantity;
                    cartitem.Amuont = cartitem.Price * cartitem.Qty;
                    listcart.Add(cartitem);
                    System.Web.HttpContext.Current.Session["MyCart"] = listcart;
                }
            }
            return listcart;
        }
        public List<CartItem> GetCart()
        {
            if(System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                return null;
            }
            else
            {
                return (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
            }          
        }
        public void UpdateCart(int productid, int qty)
        {
            if (!System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                List<CartItem> listcart = this.GetCart();
                int vt = 0;
                foreach (var item in listcart)
                {
                    if (item.productId == productid && qty >= 1)
                    {                       
                        listcart[vt].Qty = qty;                      
                        listcart[vt].Amuont = qty * listcart[vt].Price;
                    }
                    vt++;                   
                }
                System.Web.HttpContext.Current.Session["MyCart"] = listcart;
            }
        }
        public void DelCart(int productid)
        {           
            if (!System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                List<CartItem> listcart = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
                int vt = 0;
                foreach (var item in listcart)
                {
                    if (item.productId == productid)
                    {
                        listcart.RemoveAt(vt);
                        break;
                    }
                    vt++;
                }
                System.Web.HttpContext.Current.Session["MyCart"] = listcart;
            }
        }
    }
}