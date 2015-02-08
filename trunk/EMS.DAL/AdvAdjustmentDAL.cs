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
    public class AdvAdjustmentDAL
    {
        public static DataSet GetAdjustmentModelDataset(ISearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[uspGetAdvAdj]";
           DataSet reader =new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@AdvAdjId", Convert.ToInt16(searchCriteria.StringParams[0]));
                oDq.AddVarcharParam("@AdjustmentNo", 3, searchCriteria.StringParams[1]);
                oDq.AddVarcharParam("@JobNo", 50, searchCriteria.StringParams[2]);
                oDq.AddVarcharParam("@InvoiceNo", 50, searchCriteria.StringParams[3]);
                oDq.AddVarcharParam("@AdvOrAdj", 50, searchCriteria.StringParams[4]);
                oDq.AddVarcharParam("@PartyType", 50, searchCriteria.StringParams[5]);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                reader = oDq.GetTables();
            }
            return reader;
        }
        public static List<AdjustmentModel> GetAdjustmentModel(ISearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[uspGetAdvAdj]";
            List<AdjustmentModel> adjustmentModel = new List<AdjustmentModel>();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@AdvAdjId", Convert.ToInt16(searchCriteria.StringParams[0]));
                oDq.AddVarcharParam("@AdjustmentNo", 3, searchCriteria.StringParams[1]);
                oDq.AddVarcharParam("@InvoiceNo", 50, searchCriteria.StringParams[2]);
                oDq.AddVarcharParam("@AdvOrAdj", 50, searchCriteria.StringParams[3]);
                oDq.AddVarcharParam("@PartyType", 1, searchCriteria.StringParams[4]);
                oDq.AddVarcharParam("@JobNo", 50, searchCriteria.StringParams[5]);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataSet reader = oDq.GetTables();

                if (reader != null && reader.Tables.Count > 0 && reader.Tables[0].Rows.Count > 0)
                {

                    //for (int i = 0; i < reader.Tables[0].Rows.Count; i++)
                    //{
                    //    IParty iParty = new PartyEntity(reader.Tables[0].Rows[i]);
                    //    lstParty.Add(iParty);
                    //}

                    var hasChild=(reader != null && reader.Tables.Count > 0 && reader.Tables[1].Rows.Count > 0);
                     adjustmentModel = reader.Tables[0].AsEnumerable().Select(x => new AdjustmentModel {

                        AdjustmentNo = Convert.ToString(x["AdjustmentNo"]),
                        AdjustmentPk = Convert.ToInt32(x["pk_AdvAdjID"]),
                        AdjustmentDate = Convert.ToDateTime(x["AdjustmentDate"]),
                        JobNo = Convert.ToString(x["fk_JobID"]),
                        DOrC = Convert.ToString(x["DorC"]),
                        CompanyID = Convert.ToInt32(x["fk_CompanyID"]),
                        UserID = Convert.ToInt32(x["fk_UserAdded"]),
                        DebtorOrCreditorName = Convert.ToString(x["fk_PartyID"]),
                        PartyType = Convert.ToString(x["PartyType"]),
                        PartyName = Convert.ToString(x["PartyName"]),
                        AdvanceID = Convert.ToInt32(x["fk_AdvanceID"]),
                        AdvanceNo = Convert.ToString(x["AdvanceNo"])
                    
                    }).ToList();
                    foreach(var obj in adjustmentModel){
                    if (hasChild) {
                        var rows = reader.Tables[1].AsEnumerable().Where(x => Convert.ToInt32( x["fk_AdvAdjID"]) == obj.AdjustmentPk);
                        if (rows.Count() > 0) {
                            obj.LstInvoiceJobAdjustment = rows.Select(x => new InvoiceJobAdjustment
                            {
                                InvoiceJobAdjustmentPk = Convert.ToInt32(x["pk_AdvAdjInvID"]),
                                CrAmount = Convert.ToString(x["DorC"]).Equals("C") ? Convert.ToDouble(x["AdjAmount"]) : 0,
                                DrAmount = Convert.ToString(x["DorC"]).Equals("D") ? Convert.ToDouble(x["AdjAmount"]) : 0,
                                InvOrAdv = Convert.ToString(x["AdvOrInv"]),
                                InvoiceOrAdvNo = Convert.ToString(x["fk_InvID"])
                                
                            }).ToList();
                            
                        }
                        
                    }
                    }

                }
            }
            return adjustmentModel;
        }

        public static DataSet GetDetailFromJob(int? JobId = null, char? DorC = null)
        {
            string strExecution = "[fwd].[uspGetDetailFromJob]";
            DataSet reader = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@JobId", JobId);
                oDq.AddCharParam("@DorC", 3, DorC);
                 reader = oDq.GetTables();
            }
            return  reader;
        }

        public static DataSet GetAdvaneDetail(int AdvanceID)
        {
            string strExecution = "[fwd].[uspGetAdvanceDetails]";
            DataSet reader = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@AdvanceId", AdvanceID);
                reader = oDq.GetTables();
            }
            return reader;
        }

        public static DataSet GetInvoiceFromJob(int? JobId = null, char? DorC = null, char? NorAdj = null)
        {
            string strExecution = "[fwd].[uspGetInvoiceFromJob]";
            DataSet reader = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@JobId", JobId);
                oDq.AddCharParam("@DorC", 3, DorC);
                oDq.AddCharParam("@NorAdj", 1, NorAdj);
                reader = oDq.GetTables();
            }
            return reader;
        }

        public static DataSet GetUnadjustedAdvances(int? JobId = null, char? DorC = null, int? PartyID = null)
        {
            string strExecution = "[fwd].[uspGetUnadjustedAdvances]";
            DataSet reader = new DataSet();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@JobId", JobId);
                oDq.AddCharParam("@DorC", 3, DorC);
                oDq.AddIntegerParam("@PartyID", PartyID);
                reader = oDq.GetTables();
            }
            return reader;
        }

        public static int SaveAdjustmentModel(AdjustmentModel adjustmentModel, string Mode)
        {
            string strExecution = "[fwd].[uspManageAdvAdj]";
            int result = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@Mode", 1, Mode);
                oDq.AddIntegerParam("@pk_AdvAdjID", adjustmentModel.AdjustmentPk);
                oDq.AddIntegerParam("@fk_CompanyID", adjustmentModel.CompanyID);

                oDq.AddIntegerParam("@fk_JobID", Convert.ToInt32(adjustmentModel.JobNo));
                oDq.AddVarcharParam("@DorC", 1, adjustmentModel.DOrC);
                oDq.AddIntegerParam("@fk_PartyID", Convert.ToInt32(adjustmentModel.DebtorOrCreditorName));

                oDq.AddVarcharParam("@AdjustmentNo", 60, adjustmentModel.AdjustmentNo);
                oDq.AddDateTimeParam("@AdjustmentDate", adjustmentModel.AdjustmentDate);
                oDq.AddIntegerParam("@AdvanceID", adjustmentModel.AdvanceID);
               // oDq.AddVarcharParam("@Adjustments", int.MaxValue, Utilities.Utilities.ConvertDTOToXML<InvoiceJobAdjustment>("Root", "Adjustment", adjustmentModel.LstInvoiceJobAdjustment));
                oDq.AddVarcharParam("@Adjustments", int.MaxValue, Utilities.GeneralFunctions.SerializeWithXmlTag(adjustmentModel.LstInvoiceJobAdjustment).Replace("?<?xml version=\"1.0\" encoding=\"utf-16\"?>", ""));

                oDq.AddIntegerParam("@UserID", adjustmentModel.UserID);
               
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);

               
                var pk_AdvAdjInvID = Convert.ToInt32(oDq.GetScalar());                
                result =  Convert.ToInt32(oDq.GetParaValue("@Result"));
                
            }
            return result;
        }

        public static int DeleteAdjustment(int AdjustmentModelID, int UserID, int CompanyID)
        {
            string strExecution = "[fwd].[uspManageAdvAdj]";
            int ret = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddVarcharParam("@Mode", 1, "D");
                oDq.AddIntegerParam("@pk_AdvAdjInvID", AdjustmentModelID);
                oDq.AddIntegerParam("@fk_CompanyID", CompanyID);

                oDq.AddIntegerParam("@fk_JobID", 0);
                oDq.AddVarcharParam("@DorC", 1, "");
                oDq.AddIntegerParam("@fk_PartyID",0);

                oDq.AddVarcharParam("@AdjustmentNo", 60, "");
                oDq.AddDateTimeParam("@AdjustmentDate", DateTime.Now);
                oDq.AddVarcharParam("@Adjustments", int.MaxValue, "");

                oDq.AddIntegerParam("@UserID", UserID);
                int result = 0;
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);

               


                ret = Convert.ToInt32(oDq.GetScalar());
            }

            return ret;
        }
    }
}
