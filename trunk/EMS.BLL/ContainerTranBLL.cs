using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.Entity;
using EMS.DAL;

namespace EMS.BLL
{
    public class ContainerTranBLL
    {
        public DataSet GetContainerTransactionList(SearchCriteria searchCriteria, int ID)
        {
            return ContainerTranDAL.GetContainerTransactionList(searchCriteria, ID);
        }

        public DataTable GetContainerTransactionListFiltered(int Status, int LocationId)
        {
            return ContainerTranDAL.GetContainerTransactionListFiltered(Status, LocationId);
        }

        public int AddEditContainerTransaction(out string TransactionCode, string OldTransactionCode, string Containers, int MovementOptID, int TotalTEU, int TotalFEU
            , DateTime MovementDate, string Narration, int FromLocationID, int TransferLocationID, int AddressID, int CreatedBy, DateTime CreatedOn, int ModifiedBy, DateTime ModifiedOn)
        {
            return ContainerTranDAL.AddEditContainerTransaction(out TransactionCode, OldTransactionCode, Containers, MovementOptID, TotalTEU, TotalFEU
            , MovementDate, Narration, FromLocationID, TransferLocationID, AddressID, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn);
        }

        public int DeleteTransaction(int TransactionId)
        {
            return ContainerTranDAL.DeleteTransaction(TransactionId);
        }
    }
}
