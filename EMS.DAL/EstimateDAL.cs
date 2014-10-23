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
    public sealed class EstimateDAL
    {
        public static int AddEditEstimate(IEstimate Est, string Mode, string Charges)
        {
            //
            string strExecution = "[fwd].[uspManageEstimate]";
            int Result = 0;
            string EstCode = string.Empty;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddNVarcharParam("@Mode", 1, Mode);
                oDq.AddNVarcharParam("@Charges", 4000, Charges);
                oDq.AddBigIntegerParam("@pk_EstimateID", Est.EstimateID);
                oDq.AddVarcharParam("@PorR", 1, Est.PorR);
                oDq.AddVarcharParam("@TransactionType", 1, Est.TransactionType);
                oDq.AddDateTimeParam("@EstimateDate", Est.EstimateDate);
                oDq.AddVarcharParam("@EstimateNo", 30, Est.EstimateNo);
                oDq.AddIntegerParam("@fk_CompanyID", Est.CompanyID);
                oDq.AddIntegerParam("@fk_JobID", Est.fk_JobID);
                oDq.AddIntegerParam("@fk_BillFromID", Est.fk_BillFromID);
                oDq.AddIntegerParam("@CreditDays", Est.CreditDays);
                oDq.AddIntegerParam("@UserID", Est.CreatedBy);
                
                oDq.AddIntegerParam("@Result", Result, QueryParameterDirection.Output);
                //oDq.AddVarcharParam("@TranCode", 50, TranCode, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@Result"));
                //TransactionCode = Convert.ToString(oDq.GetParaValue("@TranCode"));
            }
            return Result;
        }
    }
}
