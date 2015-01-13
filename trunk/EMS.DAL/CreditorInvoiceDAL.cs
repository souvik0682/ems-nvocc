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
    public class CreditorInvoiceDAL
    {
        public static DataSet GetCreditor(ISearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[prcGetCreditor]";
            DataSet reader = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {                
                reader = oDq.GetTables();

            }
            return reader;
        }

        public static DataSet GetJobForCreInv(ISearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[uspGetJobForCreInv]";
            DataSet reader = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@JobId", Convert.ToInt32(searchCriteria.StringOption1));
                reader = oDq.GetTables();

            }
            return reader;
        }

        public static List<CreditorInvoice> GetCreditorInvoice(ISearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[uspGetCreInv]";
            List<CreditorInvoice> creditorInvoice = new List<CreditorInvoice>();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@pk_CInvoiceID", Convert.ToInt32(searchCriteria.StringOption1));
                
                DataSet reader = oDq.GetTables();

                if (reader != null && reader.Tables.Count > 0 && reader.Tables[0].Rows.Count > 0)
                {
                    var hasChild = (reader != null && reader.Tables.Count > 0 && reader.Tables[1].Rows.Count > 0);
                    creditorInvoice = reader.Tables[0].AsEnumerable().Select(x => new CreditorInvoice
                    {

                        CreditorInvoiceId = Convert.ToInt32(x["pk_CInvoiceID"]),
                        CreditorId = Convert.ToInt32(x["fk_CreditorID"]),
                        //CreditorName = Convert.ToString(x["pk_AdvAdjID"]),
                        JobNumber = Convert.ToString(x["fk_JobID"]),
                        CreInvoiceNo = Convert.ToString(x["BLARefno"]),
                        OurInvoiceRef = Convert.ToString(x["InvoiceNo"]),
                        PartyTypeID = Convert.ToInt32(x["fk_PartyTypeID"]),
                        //HouseBLNo = Convert.ToString(x["BLARefNo"]),
                        //Location = Convert.ToString(x["fk_UserAdded"]),
                        CreInvoiceDate = Convert.ToDateTime(x["InvoiceDate"]),
                        ReferenceDate = Convert.ToDateTime(x["BLARefDate"]),
                        ROE = Convert.ToDouble(x["ROE"]),
                        RoundingOff = Convert.ToDouble(x["Roff"]),
                        //HouseBLDate = Convert.ToDateTime(x["BLARefDate"]),
                        //InvoiceAmount = Convert.ToDouble(x["ChargeAmount"]),
                        //RoundingOff = Convert.ToDouble(x["ChargeAmount"])

                    }).ToList();
                    foreach (var obj in creditorInvoice)
                    {
                        if (hasChild)
                        {
                            var rows = reader.Tables[1].AsEnumerable().Where(x => Convert.ToInt32(x["fk_CInvoiceID"]) == obj.CreditorInvoiceId);
                            if (rows.Count() > 0)
                            {
                                obj.CreditorInvoiceCharges = rows.Select(x => new CreditorInvoiceCharge
                                {
                                    CreditorInvoiceChargeId = Convert.ToInt32(x["pk_InvChgID"]),
                                    ChargeId = Convert.ToInt32(x["fk_ChargesID"]),
                                  //ChargeName = Convert.ToString(x["ChargeRate"]),
                                    Rate = Convert.ToDouble(x["ChargeRate"]),
                                    Unit = Convert.ToDouble(x["Nos"]),
                                    UnitTypeID = Convert.ToInt32(x["fk_UnitTypeID"]),
                                    CntrSize = Convert.ToString(x["CntrSize"]),
                                    //Total = Convert.ToDouble(x["AdvOrInv"]),
                                    CurrencyId = Convert.ToInt32(x["fk_CurrencyID"]),
                                    //Currency = Convert.ToString(x["DorC"]),
                                    ConvRate = Convert.ToDouble(x["CROE"]),
                                    Gross = Convert.ToDouble(x["INRAmount"]),
                                    STaxPercentage = Convert.ToDouble(x["STPer"]),
                                    STax = Convert.ToDouble(x["STAmount"]),
                                    GTotal = Convert.ToDouble(x["ChargeAmount"])

                                }).ToList();

                            }
                            obj.CreditorInvoiceCharges.ForEach(x => { x.Total = x.Unit * x.Rate; });
                            obj.InvoiceAmount = obj.CreditorInvoiceCharges.Sum(x=>x.GTotal);
                            //obj.RoundingOff = Math.Round( obj.InvoiceAmount);
                        }
                    }

                }
            }
            return creditorInvoice;
        }

        public static int SaveCreditorInvoice(CreditorInvoice creditorInvoice, string Mode)
        {
            string strExecution = "[fwd].[uspManageCreInv]";
            int result = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@Mode", 1, Mode);
                oDq.AddIntegerParam("@pk_CinvoiceID", creditorInvoice.CreditorInvoiceId);
                oDq.AddIntegerParam("@fk_CompanyID", creditorInvoice.CompanyID);

                oDq.AddIntegerParam("@fk_JobID", Convert.ToInt32(creditorInvoice.JobNumber));
                oDq.AddIntegerParam("@fk_CreditorID", creditorInvoice.CreditorId);
                oDq.AddIntegerParam("@fk_PartyTypeID", creditorInvoice.PartyTypeID);
                oDq.AddVarcharParam("@InvoiceNo", 60, creditorInvoice.CreInvoiceNo);

                oDq.AddDateTimeParam("@InvoiceDate",  creditorInvoice.CreInvoiceDate);
                oDq.AddVarcharParam("@BLARefNo", 0, creditorInvoice.OurInvoiceRef);
               
                oDq.AddDateTimeParam("@BLARefDate", creditorInvoice.ReferenceDate);
                var charge=creditorInvoice.CreditorInvoiceCharges.FirstOrDefault();                
                oDq.AddIntegerParam("@fk_CurrencyID",charge.CurrencyId);
                oDq.AddDecimalParam("@ROE", 12, 3, Convert.ToDecimal(charge.ConvRate));
                oDq.AddDecimalParam("@Roff", 6, 2, Convert.ToDecimal(creditorInvoice.RoundingOff));
                oDq.AddBooleanParam("@CreInvActive",true);
                oDq.AddVarcharParam("@Charges", int.MaxValue, Utilities.GeneralFunctions.SerializeWithXmlTag(creditorInvoice.CreditorInvoiceCharges).Replace("?<?xml version=\"1.0\" encoding=\"utf-16\"?>", ""));

                oDq.AddIntegerParam("@UserID", creditorInvoice.UserID);

                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);


                var pk_AdvAdjInvID = Convert.ToInt32(oDq.GetScalar());
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));

            }
            return result;
        }
    }
}
