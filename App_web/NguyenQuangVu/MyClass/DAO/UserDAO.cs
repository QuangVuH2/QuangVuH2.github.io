using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class UserDAO
    {
        private MyDBContext db = new MyDBContext();
        // Hàm trả về danh sách các mẫu tin
        public List<User> getList(string status = "All")
        {
            List<User> list = null;
            switch (status)
            {
                case "Index":
                    {
                        // Lấy ra những mẫu tin có trạng thái khác 0
                        list = db.Users.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        // Lấy ra những mẫu tin có trạng thái = 0
                        list = db.Users.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Users.ToList();
                        break;
                    }
            }
            return list;
        }
        // Hàm trả về 1 mẫu tin
        public User getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Users.Find(id);
            }
        }

        public List<User> getRowInfor(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                List<User> Infor = null;              
                Infor = db.Users.Where(m => m.Id == id && m.Status == 1).ToList();
                return Infor;
            }
        }

        public User getRow(string username = null, string pass = null)
        {
            if (username == null)
            {
                return null;
            }
            else
            {
                return db.Users
                    .Where(m=>(m.UserName == username || m.Email == username) && m.Password.Equals(pass) && m.Roles == 1 && m.Status == 1)
                    .FirstOrDefault();
            }
        }

        public User getRowCustomer(string username = null, string pass = null)
        {
            if(username == null)
            {
                return null;
            }
            else
            {
                return db.Users
                    .Where(m => (m.UserName == username || m.Email == username) && m.Password.Equals(pass) && m.Roles == 5 && m.Status == 1)
                    .FirstOrDefault();
            }
        }

        public bool checkName(string username = null, string email = null)
        {
            return db.Users
                    .Count(m => (m.UserName == username || m.Email == email)) > 0;                   
        }
        // Hàm thêm mẫu tin
        public int Insert(User row)
        {
            db.Users.Add(row);
            return db.SaveChanges();
        }

        // Hàm cập nhật mẫu tin
        public int Update(User row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        // Hàm xóa mẫu tin
        public int Delete(User row)
        {
            db.Users.Remove(row);
            return db.SaveChanges();
        }
    }
}
