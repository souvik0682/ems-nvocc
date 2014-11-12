using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using System.Data;
using EMS.Entity;

namespace EMS.DAL
{
    public class SettlementDAL
    {
        public static List<ISettlement> GetAllSettlements(SearchCriteria searchCriteria, int locationId)
        {
            string strExecution = "[uspGetSettlement]";
            List<ISettlement> lstBL = new List<ISettlement>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                //oDq.AddIntegerParam("@pk_SettlementID", searchCriteria.IntegerOption1);
                oDq.AddIntegerParam("@Locationid", searchCriteria.LocationID);
                oDq.AddVarcharParam("@SchBLNo", 100, searchCriteria.BLNo);
                oDq.AddVarcharParam("@SchLine", 100, searchCriteria.LineName);
                oDq.AddVarcharParam("@SchLocation", 100, searchCriteria.LocName);
                oDq.AddVarcharParam("@SchSettlementNo", 100, searchCriteria.BookingNo);
                oDq.AddVarcharParam("@SortExpression", 100, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 100, searchCriteria.SortDirection);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ISettlement bl = new SettlementEntity(reader);
                    lstBL.Add(bl);
                }

                reader.Close();
            }

            return lstBL;
        }

        public static DataSet GetSettlementWithBL(Int32 BLNo)
        {
            string strExecution = "[getBLSettlementWithBL]";
            DataSet ds = new DataSet();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BLID",BLNo);
                ds = oDq.GetTables();
            }

            return ds;
        }

        public static DataSet GetSettlementWithSettlment(Int32 SettlementID)
        {
            string strExecution = "[uspgetSettlementOnly]";
            DataSet ds = new DataSet();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_SettlementID", SettlementID);
                ds = oDq.GetTables();
            }

            return ds;
        }

        public static long SaveSettlement(ISettlement Settlement)
        {
            string strExecution = "[dbo].[usp_Settlement_Save]";
            long Settlementid = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddBigIntegerParam("@userID", Settlement.CreatedBy);
                oDq.AddBigIntegerParam("@SettlementID", Settlement.pk_SettlementID);
                oDq.AddBigIntegerParam("@fk_ImpBLID", Settlement.BLID);
                oDq.AddVarcharParam("@SettlementNo", 20, Settlement.SettlementNo);
                oDq.AddVarcharParam("@PorR", 10,Settlement.PorR);
                oDq.AddDateTimeParam("@SettlementDate",Settlement.SettlementDate);
                oDq.AddDecimalParam("@SettlementAmount", 12, 2, Settlement.SettlementAmount);
                oDq.AddDecimalParam("@OutstandingAmount", 12, 2, Settlement.OutstandingAmount);
                oDq.AddBigIntegerParam("@fk_CompanyID", Settlement.CompanyID);
                oDq.AddVarcharParam("@PayRcv", 50, Settlement.PayRcvd);
                oDq.AddVarcharParam("@BankName", 100, Settlement.BankName);
                oDq.AddVarcharParam("@ChequeDetail", 100, Settlement.ChequeDetail);
                oDq.AddDateTimeParam("@ChequeDate", Settlement.ChequeDate);
                oDq.AddVarcharParam("@RRPathName", 100, Settlement.RRFileUploadPath);
                oDq.AddVarcharParam("@CLPathName", 100, Settlement.CLFileUploadPath);
                Settlementid = Convert.ToInt64(oDq.GetScalar());

            }

            return Settlementid;
        }

        public static int DeleteSettlement(int SettlementId, int UserID)
        {
            string strExecution = "[dbo].[prcDeleteSettlement]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_SettlementID", SettlementId);
                oDq.AddIntegerParam("@UserID", UserID);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }
    }
}
