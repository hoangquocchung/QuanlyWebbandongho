using KetNoiCSDL.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseIO.DBIO
{
    public class UserDao
    {
        MyDB db = null;
        public UserDao()
        {
            db = new MyDB();
        }

        public int Login(string UserName, string passWord)
        {
            var res = db.Users.SingleOrDefault(x => x.Username == UserName);
            if (res == null)
            {
                return 0;
            }
            else if (res.Status == false)
            {
                return -1;
            }
            else
            {
                if (res.Password == passWord)
                {
                    return 1;
                }
                else
                {
                    return -2;
                }
            }
        }

        public User GetByID(string username)
        {
            return db.Users.SingleOrDefault(x => x.Username == username);

        }

        /// <summary>
        /// Phân trang
        /// page là số trang truyền vào ; pageSize số bản ghi ta đưa lên
        /// tìm kiếm User dựa vào Username And Name
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Username.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public User ViewDetail(long id)
        {
            return db.Users.Find(id);
        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Username = entity.Username;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Name = entity.Name;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.Status = entity.Status;
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
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;

            db.SaveChanges();
            return user.Status;
        }
    }
}
