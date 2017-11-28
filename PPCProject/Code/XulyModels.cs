using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Web;
using System.Data.Common;
using System.Configuration;
using PPCProject.Model;


namespace PPCProject.Code
{


    public class XulyModels
    {
        Team35Entities db = new Team35Entities();
        public long Insert(PROPERTY entytiy)
        {
            db.PROPERTies.Add(entytiy);
            db.SaveChanges();
            return entytiy.ID;
        }
    }
}