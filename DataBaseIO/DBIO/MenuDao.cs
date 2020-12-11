using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseIO.DBIO
{
    public class MenuDao
    {
        MyDB db = null;
        public MenuDao()
        {
            db = new MyDB();
        }

        public Menu ViewDetail(int id)
        {
            return db.Menus.Find(id);
        }

        public List<Menu> ListAll(int id)
        {
            return db.Menus.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public List<Menu> ListAllMenu(string searchString, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Menus.Count();
            IQueryable<Menu> model = db.Menus;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Text.Contains(searchString));
            }
            return model.OrderByDescending(x => x.DisplayOrder).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public long Create(Menu entity)
        {
            db.Menus.Add(entity);
            db.SaveChanges();
            return entity.IDMenu;
        }

        public bool Edit(Menu id)
        {
            try
            {
                var model = db.Menus.Find(id);
                model.Text = id.Text;
                model.Target = id.Target;
                model.Status = id.Status;
                model.Link = id.Link;
                model.typeID = id.typeID;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }



        }

        public List<MenuType> ListAllMenutype()
        {
            //var model = db.ProductCategories.Where(x => x.MenuID == id);
            return db.MenuTypes.OrderBy(x => x.Name).ToList();
        }
    }
}
