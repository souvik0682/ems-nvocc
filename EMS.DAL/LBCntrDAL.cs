using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.Entity;
using EMS.DAL.DbManager;

namespace EMS.DAL
{
    public class LBCntrDAL
    {
        public static DataSet GetLBCntrList(SearchCriteria searchCriteria, Int32 id, int LocationID)
        {
            string strExecution = "[exp].[prcLBContainerList]";
            DataSet myDataSet;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LBId", id);
                oDq.AddIntegerParam("@LocationId", LocationID);
                oDq.AddVarcharParam("@SchContainerNo", 100, searchCriteria.StringOption1);
                oDq.AddVarcharParam("@SchBookingNo", 100, searchCriteria.StringOption2);
                oDq.AddVarcharParam("@SchEDGEBLNo", 100, searchCriteria.StringOption5);
                oDq.AddVarcharParam("@SchRefBookingNo", 100, searchCriteria.StringOption4);
                oDq.AddVarcharParam("@SchPortName", 100, searchCriteria.StringOption3);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                myDataSet = oDq.GetTables();
            }
            return myDataSet;
        }

        public static int DeleteTransaction(int TransactionId)
        {

            string strExecution = "[trn].[DeleteContainerTransaction]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@TransactionId", TransactionId);
                Result = oDq.RunActionQuery();

            }
            return Result;
        }

        public static DataTable GetContainerTransactionListFiltered(int BookingID)
        {
            string strExecution = "[exp].[prcGetContainerFromBooking]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", BookingID);
                myDataTable = oDq.GetTable();
            }
            return myDataTable;
        }

        public static int AddEditLBCntr(out string TransactionCode, string OldTransactionCode, string Containers, DateTime MovementDate, 
            int BookingID, int VesselID, int VoyageID, int CreatedBy, DateTime CreatedOn, int ModifiedBy, DateTime ModifiedOn) 
        {
            //
            string strExecution = "[exp].[AddEditLBCntr]";
            int Result = 0;
            string TranCode = string.Empty;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddNVarcharParam("@OldTransactionCode", 50, OldTransactionCode);
               
                oDq.AddNVarcharParam("@Containers", 4000, Containers);
                oDq.AddDateTimeParam("@MovementDate", MovementDate);
                oDq.AddIntegerParam("@BookingID", BookingID);
                oDq.AddIntegerParam("@VesselID", VesselID);
                oDq.AddIntegerParam("@VoyageID", VoyageID);
                if (string.IsNullOrEmpty(OldTransactionCode))
                {
                    oDq.AddIntegerParam("@UserAdded", CreatedBy);
                    oDq.AddDateTimeParam("@AddedOn", CreatedOn);
                }
                oDq.AddIntegerParam("@UserLastEdited", ModifiedBy);
                oDq.AddDateTimeParam("@EditedOn", ModifiedOn);

                oDq.AddIntegerParam("@Result", Result, QueryParameterDirection.Output);
                oDq.AddVarcharParam("@TranCode", 50, TranCode, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@Result"));
                TransactionCode = Convert.ToString(oDq.GetParaValue("@TranCode"));
            }
            return Result;
        }

        public static DataSet GetBookingDetail(int BookingID)
        {
            string strExecution = "[exp].[prcGetBookingDtlforLBCntr]";
            DataSet myDataSet;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", BookingID);
                myDataSet = oDq.GetTables();
            }
            return myDataSet;
        }

    }
}
