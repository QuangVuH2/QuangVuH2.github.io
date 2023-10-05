using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int? CatId { get; set; }
        public int? SupplierId { get; set; }
        [Required(ErrorMessage ="Tên sản phẩm không được để trống")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required(ErrorMessage = "Thông tin không được để trống")]
        public string Detail { get; set; }
        public string Img { get; set; }
        [Required(ErrorMessage = "giá bán sản phẩm không được để trống")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "giá khuyến mãi sản phẩm không được để trống")]
        public decimal PriceSale { get; set; }
        [Required(ErrorMessage = "Số lượng sản phẩm không được để trống")]
        public int Number { get; set; }
        [Required(ErrorMessage = "Từ khóa không được để trống")]
        public string MetaKey { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
    }
}
