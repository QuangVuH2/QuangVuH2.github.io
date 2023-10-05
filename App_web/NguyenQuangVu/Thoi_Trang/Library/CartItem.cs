using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thoi_Trang
{
    public class CartItem
    {
        public int productId { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal Amuont { get; set; } 
        public CartItem() { }
        public CartItem(int proid, string name, string img, decimal price, int qty) { 
            this.productId = proid;
            this.Name = name;
            this.Img = img;
            this.Price = price;
            this.Qty = qty;
            this.Amuont =  price * qty;
        }
    }
}