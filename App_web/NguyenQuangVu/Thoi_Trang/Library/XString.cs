using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Thoi_Trang
{
    public static class XString
    {
        public static string Str_Slug(string s)
        {
            String[][] symbols =
            {
                new String[] { "[áàảãạăắằẳẵặâấầẩẫậ]", "a" },
                new String[] {"[đ]", "d"},
                new String[] { "[éèẻẽẹêếềểễệ]", "e" },
                new String[] { "[íìỉĩị]", "i" },
                new String[] { "[óòỏõọôốồổỗộơớờởỡợ]", "o"},
                new String[] { "[úùủũụưứừửữự]", "u"},
                new String[] { "[ýỳỷỹỵ]", "y"},
                new String[] { "[\\s'\";,]", "-"}
            };
            s = s.ToLower();
            foreach(var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }

        // Mã hóa mặt khẩu
        public static string ToMD5(this string s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            var hash = MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        
    }
}