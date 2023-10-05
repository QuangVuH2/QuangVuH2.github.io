using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class PostDAO
    {
        private MyDBContext db = new MyDBContext();
        // Hàm trả về danh sách các mẫu tin
        public List<Post> getList(string status = "All", string type = "post")
        {
            List<Post> list = null;
            switch (status)
            {
                case "Index":
                    {
                        // Lấy ra những mẫu tin có trạng thái khác 0
                        list = db.Posts.Where(m => m.Status != 0 && m.PostTye == type).ToList();
                        break;
                    }
                case "Trash":
                    {
                        // Lấy ra những mẫu tin có trạng thái = 0
                        list = db.Posts.Where(m => m.Status == 0 && m.PostTye == type).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Posts.Where(m => m.PostTye == type).ToList();
                        break;
                    }
            }
            return list;
        }
        // Hàm trả về 1 mẫu tin
        public Post getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Posts.Find(id);
            }
        }
        public Post getRow(string slug)
        {
            if (slug == null)
            {
                return null;
            }
            else
            {
                return db.Posts.Where(m => m.Slug == slug && m.Status == 1 && m.PostTye == "post").FirstOrDefault();
            }
        }

        // Hàm thêm mẫu tin
        public int Insert(Post row)
        {
            db.Posts.Add(row);
            return db.SaveChanges();
        }

        // Hàm cập nhật mẫu tin
        public int Update(Post row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        // Hàm xóa mẫu tin
        public int Delete(Post row)
        {
            db.Posts.Remove(row);
            return db.SaveChanges();
        }
    }
}
