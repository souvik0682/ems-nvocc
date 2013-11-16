using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL;

namespace EMS.BLL
{
  public  class ExportMonthlyReportBLL
    {
      public DataTable GetExportMonthlyReport(int LocationID, int LineID, int VesselID, long VoyageID, DateTime StartDate, DateTime EndDate)
        {
            return ExportMonthlyReportDAL.GetExportMonthlyReport(LocationID, LineID, VesselID, VoyageID, StartDate, EndDate);
        }
    }
}
