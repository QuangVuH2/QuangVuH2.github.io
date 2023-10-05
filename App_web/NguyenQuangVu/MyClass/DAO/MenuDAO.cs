using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class MenuDAO
    {
        private MyDBContext db = new MyDBContext();
        // Hàm trả về danh sách các mẫu tin
        public List<Menu> getListbyParentid(string positon ,int parentid = 0)
        {
            return db.Menus
                .Where(m => m.ParentId == parentid && m.Status == 1 && m.Position == positon)
                .OrderBy(m => m.Orders)               
                .ToList();
        }
        public List<Menu> getList(string status = "All")
        {
            List<Menu> list = null;
            switch (status)
            {
                case "Index":
                    {
                        // Lấy ra những mẫu tin có trạng thái khác 0
                        list = db.Menus.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        // Lấy ra những mẫu tin có trạng thái = 0
                        list = db.Menus.Where(m => m.Status == 0).ToList();
                        break;
                    }               
                default:
                    {
                        list = db.Menus.ToList();
                        break;
                    }
            }
            return list;
        }
        // Hàm trả về 1 mẫu tin
        public Menu getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Menus.Find(id);
            }
        }

        // Hàm thêm mẫu tin
        public int Insert(Menu row)
        {
            db.Menus.Add(row);
            return db.SaveChanges();
        }

        // Hàm cập nhật mẫu tin
        public int Update(Menu row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        // Hàm xóa mẫu tin
        public int Delete(Menu row)
        {
            db.Menus.Remove(row);
            return db.SaveChanges();
        }
    }
}
