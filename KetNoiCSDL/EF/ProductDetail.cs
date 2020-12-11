namespace KetNoiCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID { get; set; }

        [StringLength(50)]
        public string XuatXu { get; set; }

        [StringLength(150)]
        public string KhieuDang { get; set; }

        [StringLength(50)]
        public string nangLuong { get; set; }

        [StringLength(50)]
        public string DoChiuNuoc { get; set; }

        [StringLength(150)]
        public string ChatLieuMat { get; set; }

        [StringLength(50)]
        public string DuongKinhMat { get; set; }

        [StringLength(50)]
        public string SizeDay { get; set; }

        [StringLength(150)]
        public string ChatLieuDay { get; set; }

        [StringLength(150)]
        public string ChatLieuVo { get; set; }

        [StringLength(150)]
        public string CheDoBaoHanh { get; set; }
    }
}
