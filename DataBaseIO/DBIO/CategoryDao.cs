using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseIO.DBIO
{
    public class CategoryDao
    {
        MyDB db = null;
        public CategoryDao()
        {
            db = new MyDB();
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        public List<Category> ListAllCate()
        {
            return db.Categories.Where(x => x.Status == true).OrderByDescending(x=>x.DisplayOrder).ToList();
        }

    }
}
