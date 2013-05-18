using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;

namespace EMS.DAL
{
    public class LogisticReportDAL
    {
        public static System.Data.DataSet GetRptCargoDesc(string Line, string Loc, string Stat, string CntrType, string StockDate)
        {
            DateTime dt=string.IsNullOrEmpty(StockDate)?DateTime.Now:Convert.ToDateTime(StockDate);
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("prcRptStockDetail"))
            {
                dq.AddVarcharParam("@Line",80, Line);
                dq.AddVarcharParam("@Loc",100, Loc);
                dq.AddVarcharParam("@Stat", 100, Stat);
                dq.AddVarcharParam("@CntrType", 100, CntrType);
                dq.AddDateTimeParam("@StockDate", dt);
                ds = dq.GetTables();
            }
            return ds;

        }

        public static DataSet GetRptCntrStockSummery(string Line, string Loc, string StockDate)
        {
            DateTime dt = string.IsNullOrEmpty(StockDate) ? DateTime.Now : Convert.ToDateTime(StockDate);
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("prcRptStockSummery"))
            {
                dq.AddVarcharParam("@Line", 80, Line);
                dq.AddVarcharParam("@Loc", 100, Loc);
                dq.AddDateTimeParam("@StockDate", dt);
                ds = dq.GetTables();
            }
            return ds;
        }
    }
}
