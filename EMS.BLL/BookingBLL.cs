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
        public int AddEditBooking(IBooking Booking, int CompanyID)
        {
            return BookingDAL.AddEditBooking(Booking, CompanyID);
        }

        public static List<IBooking> GetBooking(SearchCriteria searchCriteria, int ID)
        {
            return BookingDAL.GetBooking(searchCriteria, ID);
        }

        public static int DeleteBooking(int BookingId)
        {
            return BookingDAL.DeleteBooking(BookingId);
        }

        public static IBooking GetBooking(int ID)
        {
            return BookingDAL.GetBooking(ID);
        }

        public List<IBookingContainer> GetBookingContainers(int ChargesID)
        {
            return BookingDAL.GetBookingContainers(ChargesID);
        }

        public static DataSet GetExportVoyages(int Vessel)
        {
            return BookingDAL.GetExportVoyages(Vessel);
        }

        public static DataSet GetExportServices(int Line, Int32 Fpod)
        {
            return BookingDAL.GetExportServices(Line, Fpod);
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


    }
}
