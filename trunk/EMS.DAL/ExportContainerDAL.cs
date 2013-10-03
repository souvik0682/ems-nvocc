using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;

namespace EMS.DAL
{
    public class ExportContainerDAL
    {
        public static DataTable GetExportContainer(Int64 BookingID)
        {
            string strExecution = "[exp].[prcGetExpCntrStatus]";
            DataTable dt = new DataTable();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BookingId", BookingID);
                dt = oDq.GetTable();
            }
            return dt;
        }  
    }
}
