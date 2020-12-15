using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnShopDongHo.Common
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string Username { set; get; }
        public string GroupID { set; get; }
    }
}