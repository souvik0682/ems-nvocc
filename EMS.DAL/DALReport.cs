using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;

namespace EMS.DAL
{
   public class DALReport
    {

        public static System.Data.DataSet GetMoneyRcptDetails(Int64 moneyRecptNo)
        {
            DataSet ds = new DataSet();

            using (DbQuery dq = new DbQuery("prcRptMoneyRcpt"))
            {
                //dq.AddVarcharParam("@invNo", 20, invNo);
                dq.AddBigIntegerParam("@MrId", moneyRecptNo);
                ds = dq.GetTables();
            }
            return ds;
        }

        public static System.Data.DataSet GetMoneyRcptDetailsAccounts(string Location, string Line)
        {
            DataSet ds = new DataSet();

            using (DbQuery dq = new DbQuery("prcRptMoneyRcptAccounts"))
            {
                //dq.AddVarcharParam("@invNo", 20, invNo);
                dq.AddBigIntegerParam("@LocId", int.Parse(Location));
                dq.AddBigIntegerParam("@LineId", int.Parse(Line));
                ds = dq.GetTables();
            }
            return ds;
        }
        public static DataSet FillDDLMoneyRcpt(string invNo)
        {
            DataSet ds = new DataSet();

            using (DbQuery dq = new DbQuery("prcGetMoneyRcpts"))
            {
                dq.AddVarcharParam("@invoiceNO", 20, invNo);
                ds = dq.GetTables();
            }

            return ds;
        }

        public static DataSet GetImpBill(string BLRefNo, string dt)
        {
            DataSet ds = new DataSet();
            dt = dt.Trim() == "" ? System.DateTime.Now.ToShortDateString() : dt;
            DateTime dt_ = Convert.ToDateTime(dt);
            int BLid = BLRefNo.Trim() == "" ? 0 : Convert.ToInt32(BLRefNo);
            using (DbQuery dq = new DbQuery("prcRptimportbillingstatement"))
            {
                dq.AddIntegerParam("@BLRefNo", BLid);
                dq.AddDateTimeParam("@billDate", dt_);
                ds = dq.GetTables();
            }

            return ds;
        }

        public static DataSet GetImpBillAnne(string BLRefNo, string dt)
        {
            DataSet ds = new DataSet();
            dt = dt.Trim() == "" ? System.DateTime.Now.ToShortDateString() : dt;
            DateTime dt_ = Convert.ToDateTime(dt);
            int BLid = BLRefNo.Trim() == "" ? 0 : Convert.ToInt32(BLRefNo);
            using (DbQuery dq = new DbQuery("report.prcRptImportBillingAnnex"))
            {
                dq.AddIntegerParam("@BLRefNo", BLid);
                dq.AddDateTimeParam("@billDate", dt_);
                ds = dq.GetTables();
            }

            return ds;
        }

        public static string GetAddByCompName(string compname)
        {
            string data = string.Empty;
            using (DbQuery dq = new DbQuery("prcGetAddByCompName"))
            {
                dq.AddVarcharParam("@compname", 100, compname);

                data = Convert.ToString(dq.GetScalar());
            }

            return data;
        }

        public static string GetNumToWords(long num)
        {
            string data = string.Empty;
            using (DbQuery dq = new DbQuery("prcGetWordsFromNum"))
            {
                dq.AddBigIntegerParam("@num", num);

                data = Convert.ToString(dq.GetScalar());
            }

            return data;
        }
    }

}
