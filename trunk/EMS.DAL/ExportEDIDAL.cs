using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;

namespace EMS.DAL
{
   public sealed class ExportEDIDAL
   {
       public static DataSet GetExportEDI(int locationid, long veselid, long voyageid, int loadingport)
        {
            string strExecution = "[exp].[textExportEDI]";
            DataSet dt = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@locationid", locationid);
                oDq.AddBigIntegerParam("@vesselid", veselid);
                oDq.AddBigIntegerParam("@voyageid", voyageid);
                oDq.AddIntegerParam("@POLid", loadingport);

                //oDq.AddIntegerParam("@LocationID", LocationID);
                //oDq.AddDateTimeParam("@StartDate", StartDate);
                //oDq.AddDateTimeParam("@EndDate", EndDate);
                dt = oDq.GetTables();
            }
            return dt;
        }

        public static DataTable GetLoadPort(int VoyageID)
        {
            string strExecution = "[exp].[GetLoadingPortListByVoyage]";
            DataTable dt = new DataTable();

            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(strExecution);

            dquery.AddIntegerParam("@VoyageId", VoyageID);
        

            return dquery.GetTable();

        }
    }
}
