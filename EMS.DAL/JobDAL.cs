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
    public sealed class JobDAL
    {

        public static List<IJob> GetJobs(SearchCriteria searchCriteria, int ID, string JobType)
        {
            string strExecution = "[fwd].[uspGetJob]";
            List<IJob> lstJob = new List<IJob>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@JobId", ID);
                oDq.AddVarcharParam("@JobType", 1, JobType);
                oDq.AddVarcharParam("@SchJobNo", 100, searchCriteria.StringOption1);
                oDq.AddVarcharParam("@SchLineName", 100, searchCriteria.LineName);
                oDq.AddVarcharParam("@SchLocationName", 100, searchCriteria.Location);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();


                while (reader.Read())
                {
                    IJob oIH = new JobEntity(reader);
                    lstJob.Add(oIH);
                }
                reader.Close();
            }
            return lstJob;
        }

        public static int AddEditJob(IJob Jobs, int CompanyId, ref int JobId)
        {
            string strExecution = "[fwd].[uspManageJob]";
            int Result = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@userID", Jobs.CreatedBy);
                oDq.AddIntegerParam("@fk_FinYearID", 1);
                oDq.AddIntegerParam("@fk_CompanyID", CompanyId);
                oDq.AddCharParam("@Mode", 1, Jobs.Action);
                oDq.AddBigIntegerParam("@JobId", Jobs.JobID);
                oDq.AddDateTimeParam("@JobDate", Jobs.JobDate);
                oDq.AddIntegerParam("@fk_JobTypeID", Jobs.JobTypeID);
                oDq.AddVarcharParam("@PJobNo", 20, Jobs.PJobNo);
                oDq.AddVarcharParam("@JobNo", 20, Jobs.JobNo);
                oDq.AddIntegerParam("@fk_OpsLocID", Jobs.OpsLocID);
                oDq.AddIntegerParam("@fk_JobLocID", Jobs.jobLocID);
                oDq.AddIntegerParam("@fk_SalesManID", Jobs.SalesmanID);
                oDq.AddIntegerParam("@fk_SModeID", Jobs.SmodeID);
                oDq.AddIntegerParam("@fk_PrDocID", Jobs.PrDocID);
                oDq.AddBooleanParam("@PrintHBL", Jobs.PrintHBL);
                oDq.AddIntegerParam("@fk_HBLFormatID", Jobs.HBLFormatID);
                oDq.AddIntegerParam("@ttl20", Jobs.ttl20);
                oDq.AddIntegerParam("@ttl40", Jobs.ttl40);
                oDq.AddDecimalParam("@grwt", 12,3,Jobs.grwt);
                oDq.AddDecimalParam("@VolWt", 12, 3, Jobs.VolWt);
                oDq.AddDecimalParam("@weightMT", 12, 3, Jobs.weightMT);
                oDq.AddDecimalParam("@volCBM", 12, 3, Jobs.volCBM);
                oDq.AddDecimalParam("@RevTon", 12, 3, Jobs.RevTon);
                oDq.AddIntegerParam("@fk_FlineID", Jobs.FLineID);
                oDq.AddIntegerParam("@fk_LportID", Jobs.fk_LportID);
                oDq.AddIntegerParam("@fk_DportID", Jobs.fk_DportID);
                oDq.AddVarcharParam("@PlaceOfReceipt", 100, Jobs.PlaceOfReceipt);
                oDq.AddVarcharParam("@PlaceOfDelivery", 100, Jobs.PlaceOfDelivery);
                oDq.AddIntegerParam("@fk_CustID", Jobs.fk_CustID);
                oDq.AddIntegerParam("@fk_CustID", Jobs.fk_CustAgentID);
                oDq.AddIntegerParam("@fk_TransID", Jobs.fk_TransID);
                oDq.AddIntegerParam("@fk_OSID", Jobs.fk_OSID);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                //oDq.AddIntegerParam("@LeaseId", outBookingId, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));
                //JobId = Convert.ToInt32(oDq.GetParaValue("@JobId"));
            }
            return Result;
        }

        public static int DeleteJob(int JobID, int UserID, int CompanyID)
        {
            string strExecution = "[fwd].[prcDeleteVoyage]";
            int ret = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@JobId", JobID);
                oDq.AddVarcharParam("@Mode", 1, "D");
                oDq.AddBigIntegerParam("@ModifiedBy", UserID);

                ret = Convert.ToInt32(oDq.GetScalar());
            }

            return ret;
        }

        
    }
}
