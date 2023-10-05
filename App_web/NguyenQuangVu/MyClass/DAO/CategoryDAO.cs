using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class CategoryDAO
    {
        private MyDBContext db = new MyDBContext();
        // Hàm trả về danh sách các mẫu tin

        public List<Category> getListbyCategory(int parentid)
        {
            return db.Categorys
                .Where(m => m.ParentId == parentid && m.Status == 1)
                .OrderBy(m => m.Orders)
                .ToList();
        }
        public List<Category> getList(string status = "All")
        {
            List<Category> list = null;
            switch (status)
            {
                case "Index":
                    {
                        // Lấy ra những mẫu tin có trạng thái khác 0
                        list = db.Categorys.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        // Lấy ra những mẫu tin có trạng thái = 0
                        list = db.Categorys.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Categorys.ToList();
                        break;
                    }
            }          
            return list;
        }
        // Hàm trả về 1 mẫu tin
        public Category getRow(int? id)
        {
            if(id == null)
            {
                return null;
            }
            else
            {
                return db.Categorys.Find(id);
            }
        }

        public Category getRow(string slug)
        {
            if (slug == null)
            {
                return null;
            }
            else
            {
                return db.Categorys.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
            }
        }

        // Hàm thêm mẫu tin
        public int Insert(Category row)
        {
            db.Categorys.Add(row);
            return db.SaveChanges();
        }

        // Hàm cập nhật mẫu tin
        public int Update(Category row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        // Hàm xóa mẫu tin
        public int Delete(Category row)
        {
            db.Categorys.Remove(row);
            return db.SaveChanges();
        }
    }
}
