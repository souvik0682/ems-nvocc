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
    public sealed class AdvAdjDAL
    {
        public static List<IAdvAdj> GetAdvAdj(SearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[uspGetAdvAdj]";
            List<IAdvAdj> lstAdvAdj = new List<IAdvAdj>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@AdvAdjId", searchCriteria.AdvAdjID);
                oDq.AddVarcharParam("@SchAdvanceNo", 100, searchCriteria.AdvanceNo);
                oDq.AddVarcharParam("@SchPartyName", 100, searchCriteria.PartyName);
                oDq.AddVarcharParam("@SchPartyName", 100, searchCriteria.PartyType); 
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();


                while (reader.Read())
                {
                    IAdvAdj oLine = new AdvAdjEntity(reader);
                    lstAdvAdj.Add(oLine);
                }
                reader.Close();
            }
            return lstAdvAdj;
        }

        //public static List<IAdvAdj> GetfwLineByType(SearchCriteria searchCriteria)
        //{
        //    string strExecution = "[fwd].[uspGetLineByType]";
        //    List<IAdvAdj> lstAdvAdj = new List<IAdvAdj>();

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddVarcharParam("@SchType", 100, searchCriteria.StringOption1);
        //        oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
        //        oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
        //        DataTableReader reader = oDq.GetTableReader();

        //        while (reader.Read())
        //        {
        //            lstAdvAdj.Add(new AdvAdjEntity(reader));
        //        }
        //        reader.Close();
        //    }
        //    return lstAdvAdj;
        //}

        public static int AddEditAdvAdj(IAdvAdj Line, int CompanyId, string mode, string Invoices)
        {
            string strExecution = "[fwd].[uspManageAdvAdj]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@mode", 1, mode);
                oDq.AddIntegerParam("@pk_AdvAdjInvID", Line.CreatedBy);
                oDq.AddIntegerParam("@CompanyID", CompanyId);
                oDq.AddIntegerParam("@fk_JobID", Line.JobID);
                oDq.AddVarcharParam("@AdjustmentNo", 20, Line.AdjustmentNo);
                oDq.AddDateTimeParam("@AdjustmentDate", Line.AdjustmentDate);
                oDq.AddNVarcharParam("@Adjustments", 4000, Invoices);
                oDq.AddVarcharParam("@DorC", 1, Line.DorC);
                oDq.AddIntegerParam("@Result", Result, QueryParameterDirection.Output);
                //oDq.AddIntegerParam("@LeaseId", outBookingId, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@Result"));
                ////JobId = Convert.ToInt32(oDq.GetParaValue("@JobId"));
            }
            return Result;
        }

        public static int DeleteAdvAdj(int AdvAdjID, int UserID)
        {
            string strExecution = "[fwd].[uspManageAdvAdj]";
            int ret = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@pk_AdvAdjInvID", AdvAdjID);
                oDq.AddVarcharParam("@Mode", 1, "D");
                oDq.AddBigIntegerParam("@UserID", UserID);

                ret = Convert.ToInt32(oDq.GetScalar());
            }

            return ret;
        }

        public static IAdvAdj GetAdvAdj(int ID)
        {
            string strExecution = "[fwd].[uspGetLine]";
            IAdvAdj oIH = null;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@AdvAdjId", ID);
                oDq.AddVarcharParam("@SortExpression", 30, "");
                oDq.AddVarcharParam("@SortDirection", 4, "");
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    oIH = new AdvAdjEntity(reader);
                }
                reader.Close();
            }
            return oIH;
        }
    }
}
