using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.Entity;
using EMS.DAL.DbManager;

namespace EMS.DAL
{
    public class ContainerTranDAL
    {
        public static DataSet GetContainerTransactionList(SearchCriteria searchCriteria, int ID)
        {
            string strExecution = "[trn].[getContainerMovementList]";
            DataSet myDataSet;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@MovementId", ID);
                oDq.AddVarcharParam("@TransactionCode", 50, searchCriteria.StringOption4);
                oDq.AddVarcharParam("@SchContainerNo", 100, searchCriteria.StringOption1);
                oDq.AddVarcharParam("@SchVessel", 100, searchCriteria.StringOption2);
                oDq.AddVarcharParam("@SchVoyage", 100, searchCriteria.StringOption3);
                oDq.AddVarcharParam("@SchStatus", 100, searchCriteria.StringOption5);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                myDataSet = oDq.GetTables();
            }
            return myDataSet;
        }

        public static DataTable GetContainerTransactionListFiltered(int Status, int LocationId, DateTime MovementDate,int LineID)
        {
            string strExecution = "[trn].[getContainerFiltered]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Status", Status);
                oDq.AddIntegerParam("@LocationId", LocationId);
                oDq.AddIntegerParam("@LineId", LineID);
                oDq.AddDateTimeParam("@MovementDate", MovementDate);
                

                myDataTable = oDq.GetTable();
            }
            return myDataTable;
        }

        public static int AddEditContainerTransaction(out string TransactionCode, string OldTransactionCode, string Containers, int MovementOptID, int TotalTEU, int TotalFEU
            , DateTime MovementDate, string Narration, int FromLocationID, int TransferLocationID, int AddressID, int CreatedBy, DateTime CreatedOn, int ModifiedBy, DateTime ModifiedOn)
        {
            //
            string strExecution = "[eqp].[AddEditContainerTransaction]";
            int Result = 0;
            string TranCode = string.Empty;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddNVarcharParam("@OldTransactionCode", 50, OldTransactionCode);
                oDq.AddNVarcharParam("@Containers", 4000, Containers);
                oDq.AddIntegerParam("@MovementOptID", MovementOptID);
                oDq.AddIntegerParam("@TotalTEU", TotalTEU);
                oDq.AddIntegerParam("@TotalFEU", TotalFEU);
                oDq.AddDateTimeParam("@MovementDate", MovementDate);
                oDq.AddVarcharParam("@Narration", 1000, Narration);
                oDq.AddIntegerParam("@FromLocationID", FromLocationID);
                oDq.AddIntegerParam("@TransferLocationID", TransferLocationID);
                oDq.AddIntegerParam("@AddressID", AddressID);

                if (string.IsNullOrEmpty(OldTransactionCode))
                {
                    oDq.AddIntegerParam("@UserAdded", CreatedBy);
                    oDq.AddDateTimeParam("@AddedOn", CreatedOn);
                }
                oDq.AddIntegerParam("@UserLastEdited", ModifiedBy);
                oDq.AddDateTimeParam("@EditedOn", ModifiedOn);

                oDq.AddIntegerParam("@Result", Result, QueryParameterDirection.Output);
                oDq.AddVarcharParam("@TranCode", 50,TranCode, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@Result"));
                TransactionCode = Convert.ToString(oDq.GetParaValue("@TranCode"));
            }
            return Result;
        }

        public static int DeleteTransaction(int TransactionId)
        {
            
            string strExecution = "[trn].[DeleteContainerTransaction]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@TransactionId", TransactionId);
                Result=oDq.RunActionQuery();
                
            }
            return Result;
        }
    }
}
