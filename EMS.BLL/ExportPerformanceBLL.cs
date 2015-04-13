using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL;

namespace EMS.BLL
{
   public class ExportPerformanceBLL
    {
       public DataTable GetExportPerformanceStatement(int LineID, int LocationID, DateTime StartDate, DateTime EndDate, int ServiceID, string Status, int VesselID, long VoyageID)
        {
            return ExportPerformanceDAL.GetExportPerformance(LineID, LocationID,StartDate, EndDate, ServiceID, Status, VesselID, VoyageID);
        }

       public DataTable GetExportPerformanceWithoutBL(int LineID, int LocationID, DateTime StartDate, DateTime EndDate, int ServiceID, string Status, int VesselID, long VoyageID)
       {
           return ExportPerformanceDAL.GetExportPerformanceWithoutBL(LineID, LocationID, StartDate, EndDate, ServiceID, Status, VesselID, VoyageID);
       }
    }
}
