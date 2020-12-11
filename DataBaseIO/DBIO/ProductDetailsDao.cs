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
    }
}
