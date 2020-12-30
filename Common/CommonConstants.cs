using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CommonConstants
    {
        public static string MEMBER_GROUP = "MEMBER";
        public static string ADIM_GROUP = "ADMIN";
        public static string MODE_GROUP = "MOD";

        private static readonly string[] VietNamChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
        public static string utf8Convert1(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 1; i < VietNamChar.Length; i++)
                {
                    //Thay thế và lọc dấu từng char 
                    for (int j = 0; j < VietNamChar[i].Length; j++)
                        str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
                while (str.IndexOf(" ") >= 0)//tim trong chuoi vi tri co 1 khoang trong tro len
                    str = str.Replace(" ", "-");   //sau do thay the bang dau "-" khoang trong   
            }
            return str;
        }
    }
}
