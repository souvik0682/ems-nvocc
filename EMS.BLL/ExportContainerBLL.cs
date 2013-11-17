using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL;

namespace EMS.BLL
{
    public class ExportContainerBLL
    {
        public DataTable GetExportPerformanceStatement(Int64 BookingID)
        {
            return ExportContainerDAL.GetExportContainer(BookingID);
        }
    }
}
