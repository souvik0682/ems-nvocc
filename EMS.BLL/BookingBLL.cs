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
        
        public static DataSet GetExportVoyages(int Vessel)
        {
            return BookingDAL.GetExportVoyages(Vessel);
        }

        public static DataSet GetExportServices(int Line, Int32 Fpod)
        {
            return BookingDAL.GetExportServices(Line, Fpod);
        }
    }
}
