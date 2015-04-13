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

                oDq.AddVarcharParam("@JobActive", 1, JobType);
                oDq.AddVarcharParam("@JobType", 1, searchCriteria.StringOption1);
                oDq.AddVarcharParam("@SchJobNo", 100, searchCriteria.JobNo);
                oDq.AddVarcharParam("@SchJobLoc", 100, searchCriteria.LineName);
                oDq.AddVarcharParam("@SchCustName", 100, searchCriteria.Customer);
                oDq.AddVarcharParam("@SchOpsLoc", 100, searchCriteria.OperationalControl);
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
            int outJobID = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@userID", Jobs.CreatedBy);
                oDq.AddIntegerParam("@fk_CompanyID", CompanyId);
                oDq.AddCharParam("@Mode", 1, Jobs.JobActive);
                oDq.AddBigIntegerParam("@JobId", Jobs.JobID);
                oDq.AddDateTimeParam("@JobDate", Jobs.JobDate);
                oDq.AddIntegerParam("@fk_JobTypeID", Jobs.JobTypeID);
                oDq.AddVarcharParam("@PJobNo", 30, Jobs.PJobNo);
                oDq.AddVarcharParam("@JobNo", 30, Jobs.JobNo);
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
                oDq.AddIntegerParam("@fk_CustAgentID", Jobs.fk_CustAgentID);
                oDq.AddIntegerParam("@fk_TransID", Jobs.fk_TransID);
                oDq.AddIntegerParam("@fk_OSID", Jobs.fk_OSID);
                oDq.AddCharParam("@CargoSource", 1, Jobs.CargoSource);
                oDq.AddIntegerParam("@JobScopeID", Jobs.JobScopeID);
                oDq.AddIntegerParam("@CreditDays", Jobs.CreditDays);
                oDq.AddVarcharParam("@DocumentNo", 100, Jobs.DocumentNo);
                oDq.AddVarcharParam("@Vessel", 100, Jobs.Vessel);
                oDq.AddVarcharParam("@Voyage", 100, Jobs.Voyage);
                oDq.AddVarcharParam("@JobNote1", 100, Jobs.JobNote1);
                oDq.AddVarcharParam("@JobNote2", 100, Jobs.JobNote2);
                oDq.AddIntegerParam("@CompID", Jobs.CompID);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.AddIntegerParam("@newJobId", outJobID, QueryParameterDirection.Output);

                oDq.RunActionQuery();

                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));
                JobId = Convert.ToInt32(oDq.GetParaValue("@newJobId"));

            }
            return Result;
        }

        public static int DeleteJob(int JobID, int UserID)
        {
            string strExecution = "[fwd].[uspManageJob]";
            int ret = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@JobId", JobID);
                oDq.AddVarcharParam("@Mode", 1, "D");
                oDq.AddBigIntegerParam("@UserID", UserID);

                ret = Convert.ToInt32(oDq.GetScalar());
            }

            return ret;
        }

        public static DataSet GetDashBoard(int JobId)
        {
            string strExecution = "[fwd].[uspGetDashboardData]";
            List<IJob> lstJob = new List<IJob>();
            DataSet dsDashboard = new DataSet();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@JobId", JobId);

                dsDashboard = oDq.GetTables();
            }
            return dsDashboard;
        }

        public static void UpdateJobStatus(int JobId, string Type, int UserId)
        {
            string strExecution = "[fwd].[uspUpdateJobStatus]";
            
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@JobId", JobId);
                oDq.AddVarcharParam("@type", 1, Type);
                oDq.AddIntegerParam("@UserId", UserId);
                oDq.RunActionQuery();
            }
        }

        public static void SendConfMail(int JobId, string Type, int UserId, decimal GrossProfit)
        {
            string strExecution = "[fwd].[usp_SendConfEmail]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@JobId", JobId);
                oDq.AddIntegerParam("@UserId", UserId);
                oDq.AddVarcharParam("@Type", 1, Type);
                oDq.AddDecimalParam("@GP", 12, 2, GrossProfit);
                oDq.RunActionQuery();
            }
        }

        public static void SaveEstimateFile(int EstimateId, string FileName, string OriginalFileName)
        {
            string strExecution = "[fwd].[SaveEstimateFile]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@EstimateId", EstimateId);
                oDq.AddVarcharParam("@FileName", 50, FileName);
                oDq.AddVarcharParam("@OriginalFileName", 50, OriginalFileName);
                oDq.RunActionQuery();
            }
        }

        public static void DeleteDashBoardData(int Id, string Type)
        {
            string strExecution = "[fwd].[RemoveDashboardData]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Id", Id);
                oDq.AddVarcharParam("@Type", 50, Type);
                oDq.RunActionQuery();
            }
        }

        public static DataTable GetJobNoFromJobID(int JobID)
        {
            string strExecution = "[fwd].[uspGetJobNoFromJobID]";
            DataTable dt = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@JobId", JobID);
                dt = oDq.GetTable();
            }
            return dt;
        }

        public static DataTable GetContainerCountFromJobID(int JobID)
        {
            string strExecution = "[fwd].[uspGetContainerCountFromJobID]";
            DataTable dt = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@JobId", JobID);
                dt = oDq.GetTable();
            }
            return dt;
        }


        public static int AddEditJobContainer(IBookingContainer Container)
        {
            string strExecution = "[fwd].[spAddEditJobContainers]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@JobContainerID", Container.BookingContainerID);
                oDq.AddIntegerParam("@JobID", Container.BookingID);
                oDq.AddIntegerParam("@ContainerTypeID", Container.ContainerTypeID);
                oDq.AddVarcharParam("@CntrSize", 2, Container.CntrSize);
                oDq.AddIntegerParam("@Nos", Container.NoofContainers);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@Result"));

            }
            return Result;
        }

        public static void DeactivateAllContainersAgainstJobId(int BookingId)
        {
            string strExecution = "[fwd].[spJobContainersUpdate]";
            //int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@JobId", BookingId);
                oDq.RunActionQuery();
            }
        }

        public static List<IBookingContainer> GetJobContainers(int JobID)
        {
            string strExecution = "[fwd].[spGetJobContainerList]";
            List<IBookingContainer> Containers = new List<IBookingContainer>();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@JobId", JobID);
                //oDq.AddIntegerParam("@Mode", 2); // @Mode = 2 to fetch ChargeRate
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IBookingContainer Cont = new BookingContainerEntity(reader);
                    Containers.Add(Cont);
                }
                reader.Close();
            }
            return Containers;
        }

    }
}
