using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnShopDongHo.Areas.Admin.Models
{
    public class LoginModels
    {
        [Required(ErrorMessage = "Mời bạn nhập UserName")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Mời bạn nhập passWord")]
        public string PassWord { set; get; }
        public bool RememberMe { set; get; }
        // đã đưa lên git
    }
}