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

        public Order ViewDetail (long id)
        {
            return db.Orders.Find(id);
        }

        public List<Order> ListAllOrder(string searchString, ref int totalRecord, int pageIndex = 1, int pageSize = 5)
        {
            totalRecord = db.Orders.Count();
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipMobile.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public bool ChangeStatus(long id)
        {
            var order = db.Orders.Find(id);
            order.Status = !order.Status;

            db.SaveChanges();
            return order.Status;
        }
    }
}
