using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using System.Data;

namespace EMS.BLL
{
    public class TranshipmentDetailsBLL
    {
        public DataSet GetTranshipmentHeader(int ExpBLId, Int32 CountID)
        {
            return TranshipmentDetailsDAL.GetTranshipmentHeader(ExpBLId, CountID);
        }

        public DateTime TranshipmentStatus(int ExpBlID, ref bool VoyageStatus, ref int CountID)
        {
            return TranshipmentDetailsDAL.TranshipmentStatus(ExpBlID, ref VoyageStatus, ref CountID);
        }

        public DataSet GetBookingTranshipment(int BookingId)
        {
            return TranshipmentDetailsDAL.GetBookingTranshipment(BookingId);
        }
        public DataSet GetContainerTranshipment(int PortId, int ExpBLId, int BookingId)
        {
            return TranshipmentDetailsDAL.GetContainerTranshipment(PortId, ExpBLId, BookingId);
        }
        public void SaveContainerTranshipment(TranshipmentDetails obj)
        {
            TranshipmentDetailsDAL.SaveContainerTranshipment(obj);
        }
        public DataSet GetTranshipmentList(SearchCriteria searchCriteria)
        {
            return TranshipmentDetailsDAL.GetTranshipmentList(searchCriteria);
        }
        public static void DeleteTranshipmentList(int TranshipmentId)
        {
            TranshipmentDetailsDAL.DeleteTranshipmentList(TranshipmentId);
        }

        public static DataSet GetExportVoyages(int Vessel)
        {
            return TranshipmentDetailsDAL.GetExportVoyages(Vessel);
        }

        public static DataSet GetExportMainlineVoyages(int Vessel)
        {
            return TranshipmentDetailsDAL.GetExportMainlineVoyages(Vessel);
        }
    }
}
