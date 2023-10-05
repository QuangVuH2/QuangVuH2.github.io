using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]        
        [StringLength(255)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mặt khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên của bạn")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email của bạn")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email không đúng vui lòng nhập lại.")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? Gender { get; set; }
        public int? Roles { get; set; }
        public string Address { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
    }
}
