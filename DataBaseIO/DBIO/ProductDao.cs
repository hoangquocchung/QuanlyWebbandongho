using KetNoiCSDL.EF;
using KetNoiCSDL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseIO.DBIO
{
    public class ProductDao
    {
        MyDB db = null;
        public ProductDao()
        {
            db = new MyDB();
        }

        //dùng chung
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }

        public List<Product> ListByProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();

        }

        public List<Product> ListFeatureProduct(int top)
        {

            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }


        /// <summary>
        /// lấy thông tin của sản phầm theo id
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public List<Product> ListRelatedProduct(long productid)
        {
            var product = db.Products.Find(productid);
            return db.Products.Where(x => x.ID != productid && x.CategoryID == product.CategoryID).ToList();
        }
        /// <summary>
        /// Lấy ra danh sách sản phẩm theo ProductCategory
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="totalRecord"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Product> ListByCategoryId(long categoryID, ref long totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model = db.Products.Where(x => x.CategoryID == categoryID).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }


        // Phần dùng chung cho Admin

        public List<ProductModelView> listAllProductModel(ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Count();
            //IQueryable<Product> model = db.Products;
            var model = (from a in db.Products
                         join b in db.ProductDetails
                         // Trong bảng SanPhams join với bảng ChitietSanphams
                         on a.ID equals b.ID
                         join c in db.ProductCategories
                         on a.CategoryID equals c.ID
                         // Lấy các bản ghi mà SanPhams.ID = ChitietSanphams.ID
                         select new
                         {
                             ID = a.ID,
                             Name = a.Name,
                             Code = a.Code,
                             Image = a.Image,
                             MoreImages = a.MoreImages,
                             Metatitle = a.MetaTitle,
                             Description = a.Description,
                             Price = a.Price,
                             PromotionPrice = a.PromotionPrice,
                             //sell = Math.Round((double)(100 - (((a.Price + a.PromotionPrice)/a.Price *100) -100))),
                             Quantity = a.Quantity,
                             CreatedDate = DateTime.Now,
                             TopHot = a.TopHot,

                             NameCategoty = c.NameCategory,

                             XuatXu = b.XuatXu,
                             Kieudang = b.KhieuDang,
                             NangLuong = b.nangLuong,
                             DuongKinhMat = b.DuongKinhMat,
                             DoChiuNuoc = b.DoChiuNuoc,
                             ChatLieuMat = b.ChatLieuMat,
                             SizeDay = b.SizeDay,
                             ChatLieuDay = b.ChatLieuDay,
                             ChatLieuVo = b.ChatLieuVo,
                             CheDoBaoHanh = b.CheDoBaoHanh,

                         }).AsEnumerable().Select(x => new ProductModelView()
                         {
                             ID = x.ID,
                             Name = x.Name,
                             Code = x.Code,
                             Image = x.Image,
                             MoreImages = x.MoreImages,
                             Metatitle = x.Metatitle,
                             Description = x.Description,
                             Price = x.Price,
                             PromotionPrice = x.PromotionPrice,
                             //sell = Math.Round((double)(100 - (((x.Price + x.PromotionPrice) / x.Price * 100) - 100))),
                             Quantity = x.Quantity,
                             CreatedDate = DateTime.Now,
                             TopHot = x.TopHot,

                             NameCategoty = x.NameCategoty,

                             XuatXu = x.XuatXu,
                             Kieudang = x.Kieudang,
                             NangLuong = x.NangLuong,
                             DuongKinhMat = x.DuongKinhMat,
                             DoChiuNuoc = x.DoChiuNuoc,
                             ChatLieuMat = x.ChatLieuMat,
                             SizeDay = x.SizeDay,
                             ChatLieuDay = x.ChatLieuDay,
                             ChatLieuVo = x.ChatLieuVo,
                             CheDoBaoHanh = x.CheDoBaoHanh,

                         });
            // Mỗi bản ghi lấy được sẽ chuyển thành object ProductViewModel
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();

        }
    }
}
