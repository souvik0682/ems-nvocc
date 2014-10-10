using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;

namespace EMS.DAL
{
   public class ExportPerformanceDAL
    {
       public static DataTable GetExportPerformance(int LineID, int LocationID, DateTime StartDate, DateTime EndDate, int ServiceID, string Stat, int VesselID, long VoyageID)
        {
            string strExecution = "[exp].[rptExportPerformanceReport]";
            DataTable dt = new DataTable();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LineID", LineID);
                oDq.AddIntegerParam("@LocationID", LocationID);
                oDq.AddDateTimeParam("@StartDate", StartDate);
                oDq.AddDateTimeParam("@EndDate", EndDate);
                oDq.AddIntegerParam("@ServiceID", ServiceID);
                oDq.AddVarcharParam("@Stat", 1, Stat);
                oDq.AddIntegerParam("@VesselID", VesselID);
                oDq.AddBigIntegerParam("@VoyageID", VoyageID);
                dt = oDq.GetTable();
            }
            return dt;
        }    
    }
}
