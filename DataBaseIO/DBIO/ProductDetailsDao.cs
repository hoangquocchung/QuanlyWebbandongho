using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseIO.DBIO
{
    public class ProductDetailsDao
    {
        MyDB db = null;
        public ProductDetailsDao()
        {
            db = new MyDB();
        }
        //dùng chung
        public ProductDetail ViewProductDetail(long id)
        {
            return db.ProductDetails.Find(id);
        }

        public long Create(ProductDetail entity)
        {
            db.ProductDetails.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(ProductDetail entity)
        {
            try
            {
                var model = db.ProductDetails.Find(entity.ID);
                model.ChatLieuDay = entity.ChatLieuDay;
                model.ChatLieuMat = entity.ChatLieuMat;
                model.ChatLieuVo = entity.ChatLieuVo;
                model.DoChiuNuoc = entity.DoChiuNuoc;
                model.DuongKinhMat = entity.DuongKinhMat;
                model.KhieuDang = entity.KhieuDang;
                model.nangLuong = entity.nangLuong;
                model.SizeDay = entity.SizeDay;
                model.XuatXu = entity.XuatXu;
                model.CheDoBaoHanh = entity.CheDoBaoHanh;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var detial = db.ProductDetails.Find(id);
                db.ProductDetails.Remove(detial);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }


        }

    }
}
