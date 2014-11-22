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
    public class EstimateDAL
    {
        public static DataSet GetUnitMaster(ISearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[uspGetUnitType]";
            DataSet reader = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UnitTypeId", 0);
                oDq.AddIntegerParam("@CompanyID", Convert.ToInt32(searchCriteria.StringOption1));
                oDq.AddVarcharParam("@SchUnitName", 50, "");
                oDq.AddVarcharParam("@SortExpression", 30, "");
                oDq.AddVarcharParam("@SortDirection", 4, "");
                reader = oDq.GetTables();
            }

            return reader;
        }
        public static DataSet GetBillingGroupMaster(ISearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[usp_GetBillFrom]";
            DataSet reader = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                reader = oDq.GetTables();
            }

            return reader;
        }

        public static DataSet GetCharges(ISearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[usp_GetAllFwdCharges]";
            DataSet reader = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                reader = oDq.GetTables();
            }

            return reader;
        }

        public static Estimate GetEstimate(ISearchCriteria searchCriteria)
        {
            Estimate estimate = null;
            string strExecution = "[fwd].[spGetEstimate]";
            DataSet reader = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_EstimateID", Convert.ToInt32(searchCriteria.StringOption1));
                int result = 0;
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                reader = oDq.GetTables();

                if (reader != null && reader.Tables.Count > 1)
                {



                    var tblEst = reader.Tables[0].AsEnumerable().Select(x => new Estimate
                    {
                        PorR = Convert.ToString(x["PorR"].GetType() == typeof(DBNull) ? "" : x["PorR"]),
                        EstimateNo = Convert.ToString(x["EstimateNo"].GetType() == typeof(DBNull) ? "" : x["EstimateNo"]),
                        EstimateDate = Convert.ToDateTime(x["EstimateDate"].GetType() == typeof(DBNull) ? (DateTime?)null : x["EstimateDate"]),
                        TransactionType = Convert.ToString(x["TransactionType"].GetType() == typeof(DBNull) ? "" : x["TransactionType"]),
                        BillFromId = Convert.ToInt32(x["fk_BillFromID"].GetType() == typeof(DBNull) ? 0 : x["fk_BillFromID"]),
                        CreditDays = Convert.ToInt32(x["CreditDays"].GetType() == typeof(DBNull) ? 0 : x["CreditDays"]),
                        PartyId = Convert.ToInt32(x["fk_PartyID"].GetType() == typeof(DBNull) ? 0 : x["fk_PartyID"]),
                        PartyName = Convert.ToString(x["PartyName"].GetType() == typeof(DBNull) ? "" : x["PartyName"]),
                        UnitTypeId = Convert.ToInt32(x["fk_UnitTypeID"].GetType() == typeof(DBNull) ? 0 : x["fk_UnitTypeID"])

                    });

                    estimate = tblEst.FirstOrDefault();

                    estimate.Charges = reader.Tables[1].AsEnumerable().Select(x => new Charge
                    {
                        ChargeId = Convert.ToInt32(x["pk_ChargeRateID"].GetType() == typeof(DBNull) ? 0 : x["pk_ChargeRateID"]),
                        Unit = Convert.ToInt32(x["Nos"].GetType() == typeof(DBNull) ? 0 : x["Nos"]),
                        ChargeMasterId = Convert.ToInt32(x["fk_ChargeID"].GetType() == typeof(DBNull) ? 0 : x["fk_ChargeID"]),
                        ChargeMasterName = Convert.ToString(x["ChargeIDName"].GetType() == typeof(DBNull) ? "" : x["ChargeIDName"]),
                        CurrencyId = Convert.ToInt32(x["fk_CurrencyID"].GetType() == typeof(DBNull) ? 0 : x["fk_CurrencyID"]),
                        Currency = Convert.ToString(x["CurrencyName"].GetType() == typeof(DBNull) ? "" : x["CurrencyName"]),
                        ROE = Convert.ToInt32(x["ROE"].GetType() == typeof(DBNull) ? 0 : x["ROE"]),
                        Rate = Convert.ToInt32(x["ChargeAmount"].GetType() == typeof(DBNull) ? 0 : x["ChargeAmount"]),
                        INR = Convert.ToInt32(x["INRAmount"].GetType() == typeof(DBNull) ? 0 : x["INRAmount"]),
                        UnitId = Convert.ToInt32(x["fk_UnitID"].GetType() == typeof(DBNull) ? 0 : x["fk_UnitID"]),

                    }).ToList();

                }

            }

            return estimate;
        }

        public static DataSet GetCurrency(ISearchCriteria searchCriteria)
        {
            string strExecution = "[exp].[prcGetAllCurrency]";
            DataSet reader = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {

                reader = oDq.GetTables();
            }
            return reader;
        }

        public static DataSet GetExchange(ISearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[usp_GetExchangeRate]";
            DataSet reader = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@CurrID", Convert.ToInt64(searchCriteria.StringOption1));
                oDq.AddDateTimeParam("@CurrDate", (DateTime?)null);
                reader = oDq.GetTables();
            }
            return reader;
        }

        public static int SaveEstimate(Estimate estimate, string Mode)
        {
            string strExecution = "[fwd].[uspManageEstimate]";
            int Estimateid = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@mode", 1, Mode);
                oDq.AddIntegerParam("@pk_EstimateID", estimate.EstimateId);
                oDq.AddDateTimeParam("@EstimateDate", estimate.EstimateDate);

                oDq.AddIntegerParam("@fk_PartyID", estimate.PartyId);
                oDq.AddVarcharParam("@EstimateNo", 1, estimate.EstimateNo);
                oDq.AddIntegerParam("@fk_CompanyID", estimate.CompanyID);

                oDq.AddIntegerParam("@fk_JobID", estimate.JobID);
                oDq.AddIntegerParam("@fk_BillFromID", estimate.BillFromId);
                oDq.AddVarcharParam("@PorR", 1, estimate.PorR);

                oDq.AddVarcharParam("@TransactionType", 1, estimate.TransactionType);
                oDq.AddIntegerParam("@CreditDays", estimate.CreditDays);
                oDq.AddVarcharParam("@Charges", int.MaxValue, Utilities.GeneralFunctions.SerializeWithXmlTag(estimate.Charges).Replace("?<?xml version=\"1.0\" encoding=\"utf-16\"?>", ""));

                oDq.AddIntegerParam("@UserID", estimate.UserID);
                int result = 0;
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                var t = Convert.ToInt32(oDq.GetScalar());

                Estimateid = Convert.ToInt32(oDq.GetParaValue("@Result"));


            }
            return Estimateid;
        }

        ////  public static int DeleteEstimate(int EstimateID, int UserID, int CompanyID)
        //  {
        //      string strExecution = "[fwd].[uspManageEstimate]";
        //      int ret = 0;
        //      using (DbQuery oDq = new DbQuery(strExecution))
        //      {
        //          oDq.AddVarcharParam("@Mode", 1, "D");
        //          oDq.AddIntegerParam("@EstimateId", EstimateID);
        //          oDq.AddIntegerParam("@fk_CompanyID", CompanyID);

        //          oDq.AddIntegerParam("@LocId", 0);
        //          oDq.AddVarcharParam("@EstimateType", 1, "");
        //          oDq.AddVarcharParam("@EstimateName", 60,"");

        //          oDq.AddVarcharParam("@EstimateAddress", 500, "");
        //          oDq.AddIntegerParam("@fk_CountryID", 0);
        //          oDq.AddIntegerParam("@fk_FlineID", 0);

        //          oDq.AddVarcharParam("@Fax", 100, "");
        //          oDq.AddVarcharParam("@Phone", 100,"");
        //          oDq.AddVarcharParam("@ContactPerson", 100,"");

        //          oDq.AddVarcharParam("@PAN", 100, "");
        //          oDq.AddVarcharParam("@TAN", 100,"");
        //          oDq.AddVarcharParam("@EmailID", 100, "");

        //          oDq.AddBigIntegerParam("@fk_PrincipalID", 0);
        //          oDq.AddIntegerParam("@UserID", UserID);
        //          int result = 0;
        //          oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);


        //          ret = Convert.ToInt32(oDq.GetScalar());
        //      }

        //      return ret;
        //  }
    }
}
