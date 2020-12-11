using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnShopDongHo.Models
{
    [Serializable]
    public class CartItem
    {
        public Product product { set; get; }
        public int Quantity { set; get; }
    }
}