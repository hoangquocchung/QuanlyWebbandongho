using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetNoiCSDL.ViewModel
{
    public class ProductModelView
    {
        public long ID { set; get; }
        public string Name { set; get; }
        public string Code { set; get; }
        public string Image { set; get; }
        public string MoreImages { set; get; }
        public string Metatitle { set; get; }
        public string Description { set; get; }
        public decimal? Price { set; get; }
        public decimal? PromotionPrice { set; get; }
        public int? Quantity { set; get; }
        public long? CategoryID { set; get; }
        public string Detail { set; get; }
        public DateTime? CreatedDate { set; get; }
        public DateTime? TopHot { set; get; }

        public string XuatXu { set; get; }
        public string Kieudang { set; get; }
        public string NangLuong { set; get; }
        public string DuongKinhMat { set; get; }
        public string DoChiuNuoc { set; get; }
        public string ChatLieuMat { set; get; }
        public string SizeDay { set; get; }
        public string ChatLieuDay { set; get; }
        public string ChatLieuVo { set; get; }
        public string CheDoBaoHanh { set; get; }

        public string NameCategoty { set; get; }

        public bool Status { set; get; }

        // khai báo khác
        public double? sell { set; get; }
    }
}
