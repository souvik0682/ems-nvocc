using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.Entity;
using EMS.DAL;

namespace EMS.BLL
{
    public class LBCntrBLL
    {
        public DataSet GetLBCntrList(SearchCriteria searchCriteria, Int32 id, int Locid)
        {
             return LBCntrDAL.GetLBCntrList(searchCriteria, id, Locid);
        }

        public int DeleteTransaction(int TransactionId)
        {
            return LBCntrDAL.DeleteTransaction(TransactionId);
        }

        public DataTable GetContainerTransactionListFiltered(Int32 BookingId, int LineID)
        {
            return LBCntrDAL.GetContainerTransactionListFiltered(BookingId, LineID);
        }

        public int AddEditLBCntr(out string TransactionCode, string OldTransactionCode, string Containers, DateTime MovementDate, int BookingID, 
            int VesselID, int VoyageID, int CreatedBy, DateTime CreatedOn, int ModifiedBy, DateTime ModifiedOn)
        {
            return LBCntrDAL.AddEditLBCntr(out TransactionCode, OldTransactionCode, Containers, MovementDate, BookingID, VesselID, VoyageID, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn);
        }

        public DataSet GetBookingDetail(Int32 BookingId)
        {
            return LBCntrDAL.GetBookingDetail(BookingId);
        }

    }
}
