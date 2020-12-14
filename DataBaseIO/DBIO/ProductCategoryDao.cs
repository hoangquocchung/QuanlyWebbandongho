using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseIO.DBIO
{
    public class ProductCategoryDao
    {
        MyDB db = null;
        public ProductCategoryDao()
        {
            db = new MyDB();
        }

        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }

        // phần cho client
        public List<ProductCategory> ListByProductCategory(int top)
        {
            return db.ProductCategories.Where(x => x.Status == true && x.ParentID == null).Take(top).ToList();
        }

        public List<ProductCategory> ListAllProductCatregory()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public ProductCategory ViewPareID (long PareID)
        {
            return db.ProductCategories.Where(x => x.ID == x.ParentID && x.ParentID == PareID).First();
        }

        // Phần Cho Admin

        public List<ProductCategory> ListAllProductCate(string searchString, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.ProductCategories.Count();
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.NameCategory.Contains(searchString));
            }
            return model.Where(x=>x.ParentID != null).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<ProductCategory> ListSubMenu(string searchString, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.ProductCategories.Count();
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.NameCategory.Contains(searchString));
            }
            return model.Where(x => x.ParentID == null).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public long Create(ProductCategory entity)
        {
            entity.CreatedDate = DateTime.Now;
            db.ProductCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Edit(ProductCategory entity)
        {
            try
            {
                var model = db.ProductCategories.Find(entity.ID);
                model.NameCategory = entity.NameCategory;
                model.CreatedDate = DateTime.Now;
                model.DisplayOrder = entity.DisplayOrder;
                model.Image = entity.Image;
                model.MenuID = entity.MenuID;
                model.MetaTiile = entity.MetaTiile;
                model.ParentID = entity.ParentID;
                model.Status = entity.Status;
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
                var Productcategori = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(Productcategori);
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
            var category = db.ProductCategories.Find(id);
            category.Status = !category.Status;

            db.SaveChanges();
            return category.Status;
        }

        public List<ProductCategory> ListAllSubcategory()
        {
            return db.ProductCategories.Where(x => x.Status == true && x.ParentID == null).ToList();
        }

        public List<ProductCategory> ListByCate()
        {
            return db.ProductCategories.Where(x => x.Status == true && x.ParentID != null).ToList();
        }

        public List<Menu> ListAllMenu()
        {
            //var model = db.ProductCategories.Where(x => x.MenuID == id);
            return db.Menus.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
