using Common;
using KetNoiCSDL.EF;
using KetNoiCSDL.ViewModel;
using PagedList;
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
        public List<Product> ListRelatedProduct(long productid,int top)
        {
            var product = db.Products.Find(productid);
            return db.Products.Where(x => x.ID != productid && x.CategoryID == product.CategoryID).Take(top).ToList();
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
        public List<Product> ListAllProductCateID(long categoryID, ref long totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            var model = db.ProductCategories.Find(categoryID);
            var c = db.ProductCategories.Where(x => x.ParentID == model.ID);
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var product = db.Products.Where(x=> x.CategoryID == categoryID).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return product.ToList();
        }

        //tìm kiếm
        public List<Product> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name.Contains(keyword)).Count();
            db.Products.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return db.Products.ToList();
        }


        // Phần dùng chung cho Admin

        public List<Product> ListAllProduct(string searchString, ref int totalRecord, int pageIndex = 1, int pageSize = 5)
        {
            totalRecord = db.Products.Count();
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public IEnumerable<ProductModelView> listAllProductModel(int page, int pageSize)
        {
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
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);

        }

        public long create(Product entity)
        {
            if (!string.IsNullOrEmpty(entity.Name))
            {
                var chuyendoi = CommonConstants.utf8Convert1(entity.Name);
                entity.MetaTitle = chuyendoi;
            }
            
            entity.CreatedDate = DateTime.Now;
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Edit(Product entity)
        {
            try
            {
                
                var model = db.Products.Find(entity.ID);
                model.CategoryID = entity.CategoryID;
                model.Code = entity.Code;
                model.CreatedDate = DateTime.Now;
                model.Description = entity.Description;
                model.Detail = entity.Detail;
                model.Image = entity.Image;
                model.MetaDescriptions = entity.MetaDescriptions;
                if (!string.IsNullOrEmpty(entity.Name))
                {
                    var chuyendoi = CommonConstants.utf8Convert1(entity.Name);
                    model.MetaTitle = chuyendoi;
                }
                model.Name = entity.Name;
                model.Price = entity.Price;
                model.PromotionPrice = entity.PromotionPrice;
                model.Quantity = entity.Quantity;
                model.Status = entity.Status;
                model.TopHot = entity.TopHot;
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public void UpdateImage(long id,string images)
        {
            var product = db.Products.Find(id);
            product.MoreImages = images;
            db.SaveChanges();
        }

        public bool Delete(long id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var product = db.Products.Find(id);
            product.Status = !product.Status;

            db.SaveChanges();
            return product.Status;
        }

        public bool updateQuantity(Product entity)
        {
            var mode = db.Products.Find(entity.ID);
            entity.Quantity = mode.Quantity;
            db.SaveChanges();
            return true;
        }
    }
}
