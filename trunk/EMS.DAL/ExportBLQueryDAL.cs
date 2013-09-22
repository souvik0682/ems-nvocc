using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;
using System.Data;

namespace EMS.DAL
{
    public sealed class ExportBLQueryDAL
    {
        public static DataSet GetBLQuery(string BLNo, int ActivityType)
        {
            string strExecution = "[exp].[getBLQuery]";
            DataSet ds = new DataSet();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLNo", 60, BLNo);
                //oDq.AddIntegerParam("@ActivityType", ActivityType);
                ds = oDq.GetTables();
            }

            return ds;
        }
        public static DataTable GetAllInvoice(Int64 BLId)
        {
            string strExecution = "[trn].[GetInvoiceStatus]";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BlId", BLId);
                dt = oDq.GetTable();
            }

            return dt;
        }
    }
}
