using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;

namespace EMS.DAL
{
   public class ExportMonthlyReportDAL
    {
       public static DataTable GetExportMonthlyReport(int LocationID, int LineID, int VesselID, long VoyageID, DateTime StartDate, DateTime EndDate, int Val)
        {
            string strExecution = "[exp].[prcExpMonthlyReport]";
            DataTable dt = new DataTable();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LOCID",LocationID);
                oDq.AddIntegerParam("@LINEID", LineID);
                oDq.AddIntegerParam("@VESSELID",VesselID);
                oDq.AddBigIntegerParam("@VOYAGEID", VoyageID);
                oDq.AddDateTimeParam("@StartDate", StartDate);
                oDq.AddDateTimeParam("@EndDate", EndDate);
                oDq.AddIntegerParam("@ReportType", Val);
                dt = oDq.GetTable();
            }
            return dt;
        }   
    }
}
