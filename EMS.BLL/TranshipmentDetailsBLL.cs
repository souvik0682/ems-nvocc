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
        public DataSet GetTranshipmentHeader(int ExpBLId)
        {
            return TranshipmentDetailsDAL.GetTranshipmentHeader(ExpBLId);
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
    }
}
