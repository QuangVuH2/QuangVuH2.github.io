using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
using MyClass.View_Models;

namespace MyClass.DAO
{
    public class OderdetailDAO
    {
        private MyDBContext db = new MyDBContext();
        // Hàm trả về danh sách các mẫu tin
        public List<OrderdetailInfo> getpaymentorder()
        {
            List<OrderdetailInfo> list = null;
            var model = from ordetail in db.Oderdetails
                        join or in db.Orders
                        on ordetail.OrderId equals or.Id
                        join pro in db.Products
                        on ordetail.ProductId equals pro.Id
                        select new OrderdetailInfo()
                        {
                            OrderdetailId = ordetail.Id,
                            OrderId = or.Id,
                            UserId = or.UserId,
                            ProductId = ordetail.ProductId,
                            ReceiverName = or.ReceiverName,
                            ReceiverAddress = or.ReceiverAddress,
                            ReceiverEmail = or.ReceiverEmail,
                            ReceiverPhone = or.ReceiverPhone,
                            Note = or.Note,
                            ProductName = pro.Name,
                            Price = ordetail.Price,
                            Qty = ordetail.Qty,
                            Amount = ordetail.Amount,
                            CreateAt = or.CreateAt,
                            UpdateAt = or.UpdateAt,
                            Status = or.Status
                        };
            list = model.ToList();
            return list;
        }      
        public List<Oderdetail> getlist(int? id)
        {
            List<Oderdetail> list = null;
            if (id == null)
            {
                return null;
            }
            else
            {
                return list = db.Oderdetails.Where(m => m.OrderId == id).ToList();
            }
        }
        // Hàm trả về mẫu tin
        public Oderdetail getRow(int? id, int? productId)
        {            
            if (id == null)
            {
                return null;
            }          
            else
            {
                return db.Oderdetails.Where(m => m.OrderId == id && m.ProductId == productId).FirstOrDefault();
            };
        }
        // Hàm thêm mẫu tin
        public int Insert(Oderdetail row)
        {
            db.Oderdetails.Add(row);
            return db.SaveChanges();
        }

        // Hàm cập nhật mẫu tin
        public int Update(Oderdetail row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        // Hàm xóa mẫu tin
        public int Delete(Oderdetail row)
        {
            db.Oderdetails.Remove(row);
            return db.SaveChanges();
        }
    }
}
