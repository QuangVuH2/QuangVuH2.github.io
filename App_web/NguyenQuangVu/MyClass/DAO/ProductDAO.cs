using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
using MyClass.View_Models;
using PagedList;

namespace MyClass.DAO
{
    public class ProductDAO
    {
        private MyDBContext db = new MyDBContext();
        // Hàm trả về danh sách các mẫu tin
        public List<ProductView> getproduct()
        {
            List<ProductView> list = null;
            var model = from cat in db.Categorys
                        join pro in db.Products
                        on cat.Id equals pro.CatId
                        join supp in db.Suppliers
                        on pro.SupplierId equals supp.Id
                        select new ProductView()
                        {
                            Id = pro.Id,
                            CatId = pro.CatId,
                            SupplierId = pro.SupplierId,
                            Name = pro.Name,
                            CatName = cat.Name,
                            SupName = supp.Name,
                            Slug = pro.Slug,
                            Detail = pro.Detail,
                            Img = pro.Img,
                            Price = pro.Price,
                            PriceSale = pro.PriceSale,
                            Number = pro.Number,
                            MetaKey = pro.MetaKey,
                            CreateBy = pro.CreateBy,
                            CreateAt = pro.CreateAt,
                            UpdateBy = pro.UpdateBy,
                            UpdateAt = pro.UpdateAt,
                            Status = pro.Status
                        };
            list = model.ToList();
            return list;
        }

        public List<ProductView> getList(string status = "All")
        {
            List<ProductView> list = null;
            var model = getproduct();
            switch (status)
            {
                case "Index":
                    {
                        // Lấy ra những mẫu tin có trạng thái khác 0
                        //list = db.Products.Where(m => m.Status != 0).ToList();
                        list = model.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        // Lấy ra những mẫu tin có trạng thái = 0
                        // list = db.Products.Where(m => m.Status == 0).ToList();
                        list = model.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        //list = db.Products.ToList();
                        list = model.ToList();
                        break;
                    }
            }
            return list;
        }

        public List<ProductView> getListbyCatId(List<int> listcatid, int limit)
        {
            List<ProductView> list = null;
            var product = getproduct();
            return list = product
                .Where(m => listcatid.Contains((int)m.CatId) && m.Status == 1)
                .OrderByDescending(m => m.CreateAt)
                .Take(limit)
                .ToList();
        }
       
        public IPagedList<ProductView> getPagelistCatId(List<int> listcatid, int pageSize, int pageNumber)
        {
            IPagedList<ProductView> list = null;
            var product = from cat in db.Categorys
                          join pro in db.Products
                          on cat.Id equals pro.CatId
                          join supp in db.Suppliers
                          on pro.SupplierId equals supp.Id
                          select new ProductView()
                          {
                              Id = pro.Id,
                              CatId = pro.CatId,
                              SupplierId = pro.SupplierId,
                              Name = pro.Name,
                              CatName = cat.Name,
                              SupName = supp.Name,
                              Slug = pro.Slug,
                              Detail = pro.Detail,
                              Img = pro.Img,
                              Price = pro.Price,
                              PriceSale = pro.PriceSale,
                              Number = pro.Number,
                              MetaKey = pro.MetaKey,
                              CreateBy = pro.CreateBy,
                              CreateAt = pro.CreateAt,
                              UpdateBy = pro.UpdateBy,
                              UpdateAt = pro.UpdateAt,
                              Status = pro.Status
                          };
            list = product.Where(m => listcatid.Contains((int)m.CatId) && m.Status == 1)
                .OrderByDescending(m => m.CreateAt)
                .ToPagedList(pageNumber, pageSize);
            return list;

        }

        public List<ProductView> ListbyCatId(List<int> listcatid, int notId, int limit)
        {
            List<ProductView> list = null;
            var product = getproduct();
            return list = product
                .Where(m => listcatid.Contains((int)m.CatId) && m.Id != notId && m.Status == 1)
                .OrderByDescending(m => m.CreateAt)
                .Take(limit)
                .ToList();
        }
        // Hàm trả về 1 mẫu tin
        public Product getRow(int? id)
        {

            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Find(id);
            }
        }

        public Product getRow(string slug)
        {

            if (slug == null)
            {
                return null;
            }
            else
            {
                return db.Products.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
            }
        }

        // Lấy chi tiết sản phẩm
        public List<ProductView> getList(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                List<ProductView> list = null;
                var model = getproduct();
                list = model.Where(m => m.Id == id).ToList();
                return list;
            }
        }

        // Hàm thêm mẫu tin
        public int Insert(Product row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }

        // Hàm cập nhật mẫu tin
        public int Update(Product row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        // Hàm xóa mẫu tin
        public int Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
    }
}
