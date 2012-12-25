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
        public static DataTable GetContainerTransactionList(SearchCriteria searchCriteria, int ID)
        {
            string strExecution = "[trn].[getContainerMovementList]";
            DataTable myDataTable;
            
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@MovementId", ID);
                oDq.AddVarcharParam("@TransactionCode", 20, searchCriteria.StringOption4);
                oDq.AddVarcharParam("@SchContainerNo", 100, searchCriteria.StringOption1);
                oDq.AddVarcharParam("@SchVessel", 100, searchCriteria.StringOption2);
                oDq.AddVarcharParam("@SchVoyage", 100, searchCriteria.StringOption3);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                myDataTable = oDq.GetTable();                
            }
            return myDataTable;
        }

        public static DataTable GetContainerTransactionListFiltered(int Status, int LocationId)
        {
            string strExecution = "[trn].[getContainerFiltered]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Status", Status);
                oDq.AddIntegerParam("@LocationId", LocationId);
                
                myDataTable = oDq.GetTable();
            }
            return myDataTable;
        }
    }
}
