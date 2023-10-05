using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.View_Models
{
    public class OrderdetailInfo
    {
        public int OrderdetailId { get; set; }
        public int OrderId { get; set; }       
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string OrderName { get; set; }
        public string OrderPhone { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverEmail { get; set; }
        public string Note { get; set; }
        public string ProductName { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
    }
}
