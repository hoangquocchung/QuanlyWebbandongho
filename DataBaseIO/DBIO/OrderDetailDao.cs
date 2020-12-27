using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseIO.DBIO
{
    public class OrderDetailDao
    {
        MyDB db = null;
        public OrderDetailDao()
        {
            db = new MyDB();
        }

        public bool Insert(OrderDetail entity)
        {
            try
            {
                db.OrderDetails.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }


        }

        public List<OrderDetail> ListOrdetail(long id)
        {
            var model = db.Orders.Find(id);
            return db.OrderDetails.Where(x => x.OrderID == id).ToList();
        }
    }
}
