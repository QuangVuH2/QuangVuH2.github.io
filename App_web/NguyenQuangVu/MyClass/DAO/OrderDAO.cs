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
    public class OrderDAO
    {
        private MyDBContext db = new MyDBContext();
        // Hàm trả về danh sách các mẫu tin

        public List<OrderdetailInfo> getListOrder()
        {
            List<OrderdetailInfo> list = null;            
            var model = from od in db.Orders
                        join ot in db.Oderdetails
                        on od.Id equals ot.OrderId
                        join u in db.Users
                        on od.UserId equals u.Id into ou 
                        from oud in ou.DefaultIfEmpty()
                        join pro in db.Products
                        on ot.ProductId equals pro.Id                       
                        select new OrderdetailInfo()
                        {
                            OrderdetailId = ot.Id,
                            OrderId = od.Id,
                            UserId = (oud == null) ? 0 : oud.Id,
                            ProductId = ot.ProductId,
                            OrderName = oud.FullName,
                            OrderPhone = oud.Phone,
                            ReceiverName = od.ReceiverName,
                            ReceiverPhone = od.ReceiverPhone,
                            ReceiverAddress = od.ReceiverAddress,
                            ReceiverEmail = od.ReceiverEmail,
                            Note = od.Note,
                            ProductName = pro.Name,
                            CreateAt = od.CreateAt,
                            UpdateAt = od.UpdateAt,
                            UpdateBy = od.UpdateBy,
                            Status = od.Status,
                            Price = ot.Price,
                            Qty = ot.Qty,
                            Amount = ot.Amount
                        };
            list = model.ToList();
            return list;
        }
        public List<Order> getList(string status = "All")
        {
            List<Order> list = null;           
            switch (status)
            {
                case "Index":
                    {
                        // Lấy ra những mẫu tin có trạng thái khác 0
                        list = db.Orders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        // Lấy ra những mẫu tin có trạng thái = 0
                        list = db.Orders.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Orders.ToList();
                        break;
                    }
            }
            return list;
        }
        // Hàm trả về 1 mẫu tin
        public Order getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Orders.Find(id);
            }
        }

        public List<OrderdetailInfo> getRowId(int? id)
        {
            if(id == null)
            {
                return null;
            }
            else
            {
                var model = getListOrder();
                List< OrderdetailInfo> list = model.Where(m=> m.OrderId == id).ToList();
                return list;
            }
        }

        // Hàm thêm mẫu tin
        public int Insert(Order row)
        {
            db.Orders.Add(row);
            return db.SaveChanges();
        }

        // Hàm cập nhật mẫu tin
        public int Update(Order row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        // Hàm xóa mẫu tin
        public int Delete(Order row)
        {
            db.Orders.Remove(row);
            return db.SaveChanges();
        }
    }
}
