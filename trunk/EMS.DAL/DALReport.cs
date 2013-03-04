using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;

namespace EMS.DAL
{
   public class DALReport
    {

        public static System.Data.DataSet GetMoneyRcptDetails(string invNo)
        {
            DataSet ds = new DataSet();

            using (DbQuery dq = new DbQuery("prcRptMoneyRcpt"))
            {
                dq.AddVarcharParam("@invNo", 20, invNo);
                ds = dq.GetTables();
            }
            return ds;
        }
    }

}
