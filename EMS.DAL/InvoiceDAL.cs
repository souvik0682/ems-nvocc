using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;

namespace EMS.DAL
{
    public sealed class InvoiceDAL
    {
        #region Invoice Type
        public static DataTable GetInvoiceType()
        {
            string strExecution = "uspGetInvoiceType";
            DataTable dt = new DataTable();


            //using (DbQuery oDq = new DbQuery(strExecution))
            //{
            //    dt = oDq.GetTable();
            //}

            return dt;
        }
        #endregion

        #region Location
        public static DataTable GetLocation()
        {
            string strExecution = "usp_Invoice_GetLocation";
            DataTable dt = new DataTable();


            //using (DbQuery oDq = new DbQuery(strExecution))
            //{
            //    dt = oDq.GetTable();
            //}

            return dt;
        }
        #endregion

        #region BL No
        public static DataTable GetBLno()
        {
            string strExecution = "usp_Invoice_GetBLno";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                dt = oDq.GetTable();
            }

            return dt;
        }
        #endregion

        public static DataTable GrossWeight(string BLno)
        {
            string strExecution = "usp_Invoice_GetGrossWeight";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLno", 60, BLno);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable TEU(string BLno)
        {
            string strExecution = "usp_Invoice_GetTEU";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLno", 60, BLno);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable FEU(string BLno)
        {
            string strExecution = "usp_Invoice_GetFEU";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLno", 60, BLno);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable Volume(string BLno)
        {
            string strExecution = "usp_Invoice_GetVolume";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLno", 60, BLno);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable BLdate(string BLno)
        {
            string strExecution = "usp_Invoice_GetBLdate";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLno", 60, BLno);
                dt = oDq.GetTable();
            }
            return dt;
        }
    }
}
