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
    public class BookingBLL
    {
        public string GetLocation(int User)
        {
            return BookingDAL.GetLocation(User);
        }

        public int AddEditBooking(IBooking Booking, int CompanyID, ref int BookingId)
        {
            return BookingDAL.AddEditBooking(Booking, CompanyID, ref BookingId);
        }

        public static List<IBooking> GetBooking(SearchCriteria searchCriteria, int ID, string CalledFrom)
        {
            return BookingDAL.GetBooking(searchCriteria, ID, CalledFrom);
        }

        public static int DeleteBooking(int BookingId)
        {
            return BookingDAL.DeleteBooking(BookingId);
        }

        public static IBooking GetBooking(int ID, string CalledFrom)
        {
            return BookingDAL.GetBooking(ID, CalledFrom);
        }

        public List<IBookingContainer> GetBookingContainers(int ChargesID)
        {
            return BookingDAL.GetBookingContainers(ChargesID);
        }

        public static DataSet GetExportVoyages(int Vessel)
        {
            return BookingDAL.GetExportVoyages(Vessel);
        }

        public static DataSet GetExportMLVoyages(int Vessel)
        {
            return BookingDAL.GetExportMLVoyages(Vessel);
        }

        public static DataSet GetExportServices(int Line, Int32 Fpod)
        {
            return BookingDAL.GetExportServices(Line, Fpod);
        }

        public static DataTable GetPortWithServices(int ServiceID, Int32 Lineid)
        {
            return BookingDAL.GetPortWithServices(ServiceID, Lineid);
        }

        //Souvik
        public static IBooking GetBookingByBookingId(int BookingId)
        {
            return BookingDAL.GetBookingByBookingId(BookingId);
        }

        public static DataTable GetSlotOperators()
        {
            return BookingDAL.GetSlotOperators();
        }
        
        public static string GetBrokeragePayableId(string BrokeragePayable)
        {
            return BookingDAL.GetBrokeragePayableId(BrokeragePayable);
        }

        public static string GetRefundPayableId(string RefundPayable)
        {
            return BookingDAL.GetRefundPayableId(RefundPayable);
        }

        public static void UpdateBooking(IBooking Booking)
        {
            BookingDAL.UpdateBooking(Booking);
        }

        public static void InsertBookingCharges(List<IBookingCharges> BookingCharges)
        {
            BookingDAL.InsertBookingCharges(BookingCharges);
        }

        public static void UpdateBookingCharges(List<IBookingCharges> BookingCharges)
        {
            BookingDAL.UpdateBookingCharges(BookingCharges);
        }

        public static List<IBookingCharges> GetBookingChargesForAdd(int BookingId)
        {
            return BookingDAL.GetBookingChargesForAdd(BookingId);
        }

        public static List<IBookingCharges> GetBookingChargesForEdit(int BookingId)
        {
            return BookingDAL.GetBookingChargesForEdit(BookingId);
        }

        public void DeactivateAllContainersAgainstBookingId(int BookingId)
        {
            BookingDAL.DeactivateAllContainersAgainstBookingId(BookingId);

        }
        public int AddEditBookingContainer(IBookingContainer Containers)
        {
            return BookingDAL.AddEditBookingContainer(Containers);
        }

        public List<IBookingTranshipment> GetBookingTranshipments(int BookingId)
        {
            return BookingDAL.GetBookingTranshipments(BookingId);
        }

        public int AddEditBookingTranshipment(IBookingTranshipment Transhipments)
        {
            return BookingDAL.AddEditBookingTranshipment(Transhipments);
        }

        public DataSet GetBookingChargesList(Int32 BookingID)
        {
            return BookingDAL.GetBookingChargesList(BookingID);
        }

        public void DeactivateAllTranshipmentsAgainstBookingId(int BookingId)
        {
            BookingDAL.DeactivateAllTranshipmentsAgainstBookingId(BookingId);

        }

        public static IBooking GetBookingById(int ID)
        {
            return BookingDAL.GetBookingById(ID);
        }

        public static DataSet GetSalesman(int SalesmanID)
        {
            return BookingDAL.GetSalesman(SalesmanID);
        }

        public static string GetBookingChargeExists(int BookingID)
        {
            return BookingDAL.GetBookingChargeExists(BookingID);
        }

        public static string GetBLFromEDGE(int NVOCCID)
        {
            return BookingDAL.GetBLFromEDGE(NVOCCID);
        }

        public static int DeleteBookingCharges(int BookingId)
        {
            return BookingDAL.DeleteBookingCharges(BookingId);
        }
    }
}
