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

    }
}
