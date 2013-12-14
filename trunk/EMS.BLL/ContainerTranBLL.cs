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
        public DataSet GetContainerTransactionList(SearchCriteria searchCriteria, int ID, int LocationID)
        {
            return ContainerTranDAL.GetContainerTransactionList(searchCriteria, ID, LocationID);
        }

        public DataTable GetContainerTransactionListFiltered(int Status, int LocationId, DateTime MovementDate, int LineID)
        {
            return ContainerTranDAL.GetContainerTransactionListFiltered(Status, LocationId,MovementDate,LineID);
        }

        public int AddEditContainerTransaction(out string TransactionCode, string OldTransactionCode, string Containers, int MovementOptID, int TotalTEU, int TotalFEU
            , DateTime MovementDate, string Narration, int FromLocationID, int TransferLocationID, int AddressID, int CreatedBy, DateTime CreatedOn, int ModifiedBy
            , DateTime ModifiedOn, Int32 BookingID, Int32 DOID)
        {
            return ContainerTranDAL.AddEditContainerTransaction(out TransactionCode, OldTransactionCode, Containers, MovementOptID, TotalTEU, TotalFEU
            , MovementDate, Narration, FromLocationID, TransferLocationID, AddressID, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, BookingID, DOID);
        }

        public int DeleteTransaction(int TransactionId)
        {
            return ContainerTranDAL.DeleteTransaction(TransactionId);
        }

        public static DataTable GetBookingList(int Loc, int Line, int EmptyYard)
        {
            return ContainerTranDAL.GetBookingList(Loc, Line, EmptyYard);
        }

        public DataTable GetExpBLno(string Initial)
        {
            return ContainerTranDAL.GetExpBLno(Initial);
        }

        public DataSet GetPendingDOList(Int32 DOID)
        {
            return ContainerTranDAL.GetPendingDOList(DOID);
        }
    }
}
