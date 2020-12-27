using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseIO.DBIO
{
    public class UserGroupDao
    {
        MyDB db = null;
        public UserGroupDao()
        {
            db = new MyDB();
        }

        public List<UserGroup> ListAll()
        {
            return db.UserGroups.OrderByDescending(x => x.Name).ToList();
        }
    }
}
