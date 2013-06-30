using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using System.Data;
using EMS.Entity;

namespace EMS.DAL
{
    public class TestDAL
    {
        public static DataTable GetTypeWiseStockSummary(string LineId, string LocationId, DateTime StockDate)
        {
            string strExecution = "prcRptTypeWiseStockSummary";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@Line", 100, LineId);
                oDq.AddVarcharParam("@Loc", 100, LocationId);
                oDq.AddDateTimeParam("@StockDate", StockDate);
                dt = oDq.GetTable();
            }

            return dt;
        }
    }
}
