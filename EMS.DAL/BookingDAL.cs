using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;
using System.Data;

namespace EMS.DAL
{
    public sealed class BookingDAL
    {
        private BookingDAL()
        { }


        public static int DeleteBooking(int BookingId)
        {
            string strExecution = "[exp].[prcDeleteBooking]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_BookingID", BookingId);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }

        public static List<IBooking> GetBooking(SearchCriteria searchCriteria, int ID)
        {
            string strExecution = "[exp].[prcGetBookingList]";
            List<IBooking> lstBooking = new List<IBooking>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", ID);
                oDq.AddVarcharParam("@SchBookingNo", 100, searchCriteria.BookingNo);
                oDq.AddVarcharParam("@SchBookingRefNo", 100, searchCriteria.StringOption1);
                oDq.AddVarcharParam("@SchBookingParty", 100, searchCriteria.StringOption2);
                oDq.AddVarcharParam("@SchVesselName", 100, searchCriteria.Vessel);
                oDq.AddVarcharParam("@SchVoyageNo", 100, searchCriteria.Voyage);
                oDq.AddVarcharParam("@SchLocation", 100, searchCriteria.Location);
                oDq.AddVarcharParam("@SchLineName", 100, searchCriteria.LineName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IBooking oIH = new BookingEntity(reader);
                    lstBooking.Add(oIH);
                }
                reader.Close();
            }
            return lstBooking;
        }

        public static IBooking GetBooking(int ID)
        {
            string strExecution = "[exp].[prcGetBookingList]";
            IBooking oIH = null;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", ID);
                oDq.AddVarcharParam("@SortExpression", 30, "");
                oDq.AddVarcharParam("@SortDirection", 4, "");
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    oIH = new BookingEntity(reader);
                }
                reader.Close();
            }
            return oIH;
        }

        public static int AddEditBooking(IBooking Booking, int CompanyId)
        {
            string strExecution = "[exp].[prcAddEditAgent]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@userID", Booking.CreatedBy);
                oDq.AddIntegerParam("@fk_CompanyID", CompanyId);
                oDq.AddBooleanParam("@isedit", Booking.Action);
                oDq.AddIntegerParam("@pk_BookingID", Booking.BookingID);
                oDq.AddIntegerParam("@fk_LineID", Booking.LinerID);
                oDq.AddIntegerParam("@fk_LocationID", Booking.LocationID);
                oDq.AddIntegerParam("@fk_FPOD", Convert.ToInt32(Booking.FPODID));
                oDq.AddIntegerParam("@fk_POR", Convert.ToInt32(Booking.PORID));
                oDq.AddIntegerParam("@fk_POD", Convert.ToInt32(Booking.PODID));
                oDq.AddIntegerParam("@fk_POL", Convert.ToInt32(Booking.POLID));
                oDq.AddIntegerParam("@fk_VesselID", Convert.ToInt32(Booking.VesselID));
                oDq.AddIntegerParam("@fk_VoyageID", Convert.ToInt32(Booking.VoyageID));
                oDq.AddIntegerParam("@fk_MainLineVesselID", Convert.ToInt32(Booking.MainLineVesselID));
                oDq.AddIntegerParam("@fk_MainLineVoyageID", Convert.ToInt32(Booking.MainLineVoyageID));
                oDq.AddDateTimeParam("@BookingDate", Booking.BookingDate);
                oDq.AddVarcharParam("@Bookingno", 40, Booking.BookingNo);
                oDq.AddIntegerParam("@fk_CustomerID", Booking.CustID);
                oDq.AddVarcharParam("@RefBookingNo", 50, Booking.RefBookingNo);
                oDq.AddDateTimeParam("@RefBookingDate", Booking.RefBookingDate);
                oDq.AddBooleanParam("@HazCargo", Booking.HazCargo);
                oDq.AddVarcharParam("@IMO", 50, Booking.IMO);
                oDq.AddVarcharParam("@UNO", 50, Booking.UNO);
                oDq.AddVarcharParam("@Commodity", 200, Booking.Commodity);
                oDq.AddCharParam("@ShipmentType", 1, Booking.ShipmentType);
                oDq.AddBooleanParam("@BLThruApp", Booking.BLThruApp);
                oDq.AddDecimalParam("@GrossWt", 12, 3, Convert.ToDecimal(Booking.GrossWt));
                oDq.AddDecimalParam("@CBM", 12, 3, Convert.ToDecimal(Booking.CBM));
                oDq.AddIntegerParam("@TotalFEU", Booking.TotalFEU);
                oDq.AddIntegerParam("@TotalTEU", Booking.TotalTEU);
                oDq.AddBooleanParam("@AcceptBooking", Booking.AcceptBooking);
                oDq.AddBooleanParam("@Reefer", Booking.Reefer);
                oDq.AddDecimalParam("@TempMax", 12, 3, Convert.ToDecimal(Booking.TempMax));
                oDq.AddDecimalParam("@TempMin", 12, 3, Convert.ToDecimal(Booking.TempMin));
                //oDq.AddDecimalParam("@HaulageRate", 12, 2, Convert.ToDecimal(ImportHaulage.HaulageRate));
                //oDq.AddBooleanParam("@HaulageStatus", ImportHaulage.HaulageStatus);

                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));
            }
            return Result;
        }

        public static DataSet GetExportVoyages(int Vessel)
        {
            string ProcName = "[exp].[spGetVoyageByVesselID]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@Vessel", Vessel);

            return dquery.GetTables();
        }

        public static DataSet GetExportServices(int Line, Int32 fpod)
        {
            string ProcName = "[exp].[spGetServiceByLineFPOD]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@Line", Line);
            dquery.AddBigIntegerParam("@FPOD", fpod);

            return dquery.GetTables();
        }

    }
}
