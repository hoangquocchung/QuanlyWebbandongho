using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseIO.DBIO
{
    public class OrderDao
    {
        MyDB db = null;
        public OrderDao()
        {
            db = new MyDB();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            order.CreatedDate = DateTime.Now;
            db.SaveChanges();
            return order.ID;
        }
    }
}
