using Common;
using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseIO.DBIO
{
    public class ContentDao
    {
        MyDB db = null;
        public ContentDao()
        {
            db = new MyDB();
        }
        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        public List<Content> ListBycontent()
        {
            return db.Contents.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public List<Content> ListAllContent(string searchString, ref int totalRecord, int pageIndex = 1, int pageSize = 5)
        {
            totalRecord = db.Products.Count();
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(); ;
        }

        public long Insert(Content id)
        {
            if (!string.IsNullOrEmpty(id.Name))
            {
                var chuyendoi = CommonConstants.utf8Convert1(id.Name);
                id.MetaTitle = chuyendoi;
            }
            id.CreatedDate = DateTime.Now;
            db.Contents.Add(id);
            db.SaveChanges();
            return id.ID;
        }

        public bool Edit(Content id)
        {
            try
            {
                var content = db.Contents.Find(id);
                content.Name = id.Name;
                if (!string.IsNullOrEmpty(id.Name))
                {
                    var chuyendoi = CommonConstants.utf8Convert1(id.Name);
                    content.MetaTitle= chuyendoi;
                }
                content.Description = id.Description;
                content.Image = id.Image;
                content.CategoryID = id.CategoryID;
                content.Detail = id.Detail;
                content.CreatedDate = DateTime.Now;
                content.Status = id.Status;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
