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

        public static string GetLocation(int UserID)
        {
            string strExecution = "[exp].[uspGetLocationForExport]";
            string Location = "";


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", UserID);

                Location = Convert.ToString(oDq.GetScalar());
            }

            return Location;
        }

        public static int DeleteBooking(int BookingId)
        {
            string strExecution = "[exp].[uspDeleteBooking]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", BookingId);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }

        public static int CheckBookingCharges(int BookingId)
        {
            string strExecution = "[exp].[uspCheckBookingCharges]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", BookingId);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }

        public static List<IBooking> GetBooking(SearchCriteria searchCriteria, int ID, string CalledFrom)
        {
            string strExecution = "[exp].[prcGetBookingList]";
            List<IBooking> lstBooking = new List<IBooking>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", ID);
                oDq.AddVarcharParam("@CalledFrom", 1, CalledFrom);
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

        public static IBooking GetBooking(int ID, string CalledFrom)
        {
            string strExecution = "[exp].[prcGetBookingList]";
            IBooking oIH = null;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", ID);
                oDq.AddVarcharParam("@CalledFrom", 1, CalledFrom);
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

        public static List<IBookingContainer> GetBookingContainers(int BookingID)
        {
            string strExecution = "[exp].[spGetBookingContainerList]";
            List<IBookingContainer> Containers = new List<IBookingContainer>();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingId", BookingID);
                //oDq.AddIntegerParam("@Mode", 2); // @Mode = 2 to fetch ChargeRate
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IBookingContainer Cont = new BookingContainerEntity(reader);
                    Containers.Add(Cont);
                }
                reader.Close();
            }
            return Containers;
        }

        public static int AddEditBooking(IBooking Booking, int CompanyId, ref int BookingId)
        {
            string strExecution = "[exp].[prcAddEditBooking]";
            int Result = 0;
            int outBookingId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@userID", Booking.CreatedBy);
                oDq.AddIntegerParam("@fk_FinYearID", 1);
                oDq.AddIntegerParam("@fk_CompanyID", CompanyId);
                oDq.AddBooleanParam("@isedit", Booking.Action);
                oDq.AddBigIntegerParam("@pk_BookingID", Booking.BookingID);
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
                oDq.AddVarcharParam("@Accounts", 200, Booking.Accounts);
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
                oDq.AddIntegerParam("@fk_ServiceID", Booking.ServicesID);
                //oDq.AddDecimalParam("@HaulageRate", 12, 2, Convert.ToDecimal(ImportHaulage.HaulageRate));
                //oDq.AddBooleanParam("@HaulageStatus", ImportHaulage.HaulageStatus);

                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.AddIntegerParam("@BookingId", outBookingId, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));
                BookingId = Convert.ToInt32(oDq.GetParaValue("@BookingId"));
            }
            return Result;
        }

        public static DataSet GetExportVoyages(int Vessel, int LocationID)
        {
            string ProcName = "[exp].[spGetVoyageByVesselID]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@Vessel", Vessel);
            dquery.AddIntegerParam("@LocationID", LocationID);

            return dquery.GetTables();
        }

        public static DataSet GetExportVoyagesWithPOL(int Vessel, int POLID)
        {
            string ProcName = "[exp].[spGetVoyageByVesselnPOL]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@Vessel", Vessel);
            dquery.AddIntegerParam("@POL", POLID);

            return dquery.GetTables();
        }

        public static DataSet GetExportMLVoyages(int Vessel)
        {
            string ProcName = "[exp].[spGetMLVoyageByVesselID]";
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

        public static DataTable GetPortWithServices(int ServiceID, Int32 Lineid)
        {
            string ProcName = "[exp].[spGetPortWithServices]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@ServiceID", ServiceID);
            dquery.AddIntegerParam("@LineID", Lineid);

            return dquery.GetTable();
            //return dquery.GetTables();
        }

        //SA Souvik
        public static IBooking GetBookingByBookingId(int BookingId)
        {
            string strExecution = "[exp].[uspGetBookingByBookingId]";
            IBooking objBooking = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", BookingId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    objBooking = new BookingEntity(reader);
                }
                reader.Close();
            }
            return objBooking;
        }

        public static List<IBookingCharges> GetBookingChargesForAdd(int BookingId)
        {
            List<IBookingCharges> lstCharge = new List<IBookingCharges>();
            string strExecution = "[exp].[usp_GetBookingChg]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", BookingId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IBookingCharges objCharge = new BookingChargesEntity(reader);
                    lstCharge.Add(objCharge);
                }
                reader.Close();
            }

            return lstCharge;
        }

        public static List<IBookingCharges> GetBookingChargesForEdit(int BookingId)
        {
            List<IBookingCharges> lstCharge = new List<IBookingCharges>();
            string strExecution = "[exp].[usp_GetBookingChgForEdit]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", BookingId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IBookingCharges objCharge = new BookingChargesEntity(reader);
                    lstCharge.Add(objCharge);
                }
                reader.Close();
            }

            return lstCharge;
        }

        public static DataTable GetSlotOperators()
        {
            string ProcName = "[exp].[usp_GetSlotOperators]";
            DbQuery dquery = new DbQuery(ProcName);

            return dquery.GetTable();
        }

        public static string GetBrokeragePayableId(string BrokeragePayable)
        {
            string strExecution = "[exp].[usp_GetBrokeragePayableId]";
            string BrokeragePayableId = string.Empty;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BrokeragePayable", 50, BrokeragePayable);
                BrokeragePayableId = Convert.ToString(oDq.GetScalar());
            }
            return BrokeragePayableId;
        }

        public static string GetRefundPayableId(string RefundPayable)
        {
            string strExecution = "[exp].[usp_GetRefundPayableId]";
            string RefundPayableId = string.Empty;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@RefundPayable", 50, RefundPayable);
                RefundPayableId = Convert.ToString(oDq.GetScalar());
            }
            return RefundPayableId;
        }

        public static void UpdateBooking(IBooking Booking)
        {
            string strExecution = "[exp].[usp_UpdateBookingFromCharges]";
            long bookingId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BookingId", Booking.BookingID);

                oDq.AddBooleanParam("@BrokeragePayable", Booking.BrokeragePayable);
                oDq.AddDecimalParam("@BrokeragePercent", 12, 2, Booking.BrokeragePercentage);
                oDq.AddIntegerParam("@BrokeragePayableId", Booking.BrokeragePayableId);
                oDq.AddBooleanParam("@RefundPayable", Booking.RefundPayable);
                oDq.AddIntegerParam("@RefundPayableId", Booking.RefundPayableId);
                oDq.AddVarcharParam("@ExportRemarks", 300, Booking.ExportRemarks);
                oDq.AddVarcharParam("@RateReference", 50, Booking.RateReference);
                oDq.AddVarcharParam("@RateType", 20, Booking.RateType);
                oDq.AddIntegerParam("@fk_FreightPayable", Booking.FreightPayableId);
                oDq.AddVarcharParam("@UploadFilePath", 200, Booking.UploadPath);
                oDq.AddIntegerParam("@SlotOperatorId", Booking.SlotOperatorId);
                oDq.AddVarcharParam("@Shipper", 300, Booking.Shipper);
                oDq.AddVarcharParam("@PpCc", 10, Booking.PpCc);
                oDq.AddIntegerParam("@ModifiedBy", Booking.ModifiedBy);
                oDq.AddDateTimeParam("@ModifiedOn", Booking.ModifiedOn);

                bookingId = Convert.ToInt64(oDq.GetScalar());
            }
        }

        public static void InsertBookingCharges(List<IBookingCharges> BookingCharges)
        {
            if (!ReferenceEquals(BookingCharges, null))
            {
                foreach (IBookingCharges charges in BookingCharges)
                {
                    string strExecution = "[exp].[usp_BookingCharge_Insert]";
                    long bookingChargeId = 0;

                    using (DbQuery oDq = new DbQuery(strExecution))
                    {
                        //oDq.AddBigIntegerParam("@BookingChargeId", charges.BookngChargeId);
                        oDq.AddBigIntegerParam("@BookingId", charges.BookingId);
                        oDq.AddIntegerParam("@ChargeId", charges.ChargeId);
                        oDq.AddIntegerParam("@ChargeRateId", charges.ChargeRateId);
                        oDq.AddIntegerParam("@CurrencyId", charges.CurrencyId);
                        oDq.AddIntegerParam("@Unit", charges.Unit);
                        oDq.AddIntegerParam("@ContainerTypeId", charges.ContainerTypeId);
                        oDq.AddBooleanParam("@ChargeApplicable", charges.ChargeApplicable);
                        oDq.AddVarcharParam("@Size", 2, charges.Size);
                        oDq.AddDecimalParam("@WtInCBM", 12, 2, charges.WtInCBM);
                        oDq.AddDecimalParam("@WtInTon", 12, 2, charges.WtInTon);
                        oDq.AddDecimalParam("@ActualRate", 12, 2, charges.ActualRate);
                        oDq.AddDecimalParam("@ManifestRate", 12, 2, charges.ManifestRate);
                        oDq.AddDecimalParam("@BrokerageBasic", 12, 2, charges.BrokerageBasic);
                        oDq.AddDecimalParam("@RefundAmount", 12, 2, charges.RefundAmount);
                        oDq.AddBooleanParam("@ChargeStatus", charges.ChargeStatus);

                        bookingChargeId = Convert.ToInt64(oDq.GetScalar());
                    }
                }
            }
        }

        public static void UpdateBookingCharges(List<IBookingCharges> BookingCharges)
        {
            if (!ReferenceEquals(BookingCharges, null))
            {
                foreach (IBookingCharges charges in BookingCharges)
                {
                    string strExecution = "[exp].[usp_BookingCharge_Update]";
                    long bookingChargeId = 0;

                    using (DbQuery oDq = new DbQuery(strExecution))
                    {
                        oDq.AddBigIntegerParam("@BookingChargeId", charges.BookingChargeId);
                        oDq.AddBigIntegerParam("@BookingId", charges.BookingId);
                        oDq.AddIntegerParam("@ChargeId", charges.ChargeId);
                        oDq.AddIntegerParam("@Unit", charges.Unit);
                        oDq.AddIntegerParam("@ChargeRateId", charges.ChargeRateId);
                        oDq.AddIntegerParam("@CurrencyId", charges.CurrencyId);
                        oDq.AddIntegerParam("@ContainerTypeId", charges.ContainerTypeId);
                        oDq.AddBooleanParam("@ChargeApplicable", charges.ChargeApplicable);
                        oDq.AddVarcharParam("@Size", 2, charges.Size);
                        oDq.AddDecimalParam("@WtInCBM", 12, 2, charges.WtInCBM);
                        oDq.AddDecimalParam("@WtInTon", 12, 2, charges.WtInTon);
                        oDq.AddDecimalParam("@ActualRate", 12, 2, charges.ActualRate);
                        oDq.AddDecimalParam("@ManifestRate", 12, 2, charges.ManifestRate);
                        oDq.AddDecimalParam("@BrokerageBasic", 12, 2, charges.BrokerageBasic);
                        oDq.AddDecimalParam("@RefundAmount", 12, 2, charges.RefundAmount);
                        oDq.AddBooleanParam("@ChargeStatus", charges.ChargeStatus);

                        bookingChargeId = Convert.ToInt64(oDq.GetScalar());
                    }
                }
            }
        }
        //EA Souvik
        public static void DeactivateAllContainersAgainstBookingId(int BookingId)
        {
            string strExecution = "[exp].[spBookingContainersUpdate]";
            //int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingId", BookingId);
                oDq.RunActionQuery();
            }
        }

        public static void DeactivateAllTranshipmentsAgainstBookingId(int BookingId)
        {
            string strExecution = "[exp].[spBookingTranshipmentsDeactive]";
            //int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingId", BookingId);
                oDq.RunActionQuery();
            }
        }

        public static int AddEditBookingContainer(IBookingContainer Container)
        {
            string strExecution = "[exp].[spAddEditBookingContainers]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingContainerID", Container.BookingContainerID);
                oDq.AddIntegerParam("@BookingID", Container.BookingID);
                oDq.AddIntegerParam("@ContainerTypeID", Container.ContainerTypeID);
                oDq.AddVarcharParam("@CntrSize", 2, Container.CntrSize);
                oDq.AddDecimalParam("@WtPerCntr", 12, 3, Container.wtPerCntr);
                oDq.AddIntegerParam("@Nos", Container.NoofContainers);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@Result"));

            }
            return Result;
        }

        public static List<IBookingTranshipment> GetBookingTranshipments(int BookingID)
        {
            string strExecution = "[exp].[spGetBookingTranshipmentList]";
            List<IBookingTranshipment> Transhipments = new List<IBookingTranshipment>();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingId", BookingID);
                //oDq.AddIntegerParam("@Mode", 2); // @Mode = 2 to fetch ChargeRate
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IBookingTranshipment trans = new BookingTranshipmentEntity(reader);
                    Transhipments.Add(trans);
                }
                reader.Close();
            }
            return Transhipments;
        }

        public static int AddEditBookingTranshipment(IBookingTranshipment Transhipment)
        {
            string strExecution = "[exp].[spAddEditBookingTranshipments]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingTranshipmentID", Transhipment.BookingTranshipmentID);
                oDq.AddIntegerParam("@BookingID", Transhipment.BookingID);
                oDq.AddIntegerParam("@PortID", Transhipment.PortId);
                oDq.AddIntegerParam("@OrderNo", Transhipment.OrderNo);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@Result"));

            }
            return Result;
        }

        public static DataSet GetBookingChargesList(int BookingID)
        {
            string strExecution = "[exp].[prcGetBookingChargesList]";
            DataSet myDataSet;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", BookingID);
                myDataSet = oDq.GetTables();
            }
            return myDataSet;
        }

        public static IBooking GetBookingById(int ID)
        {
            string strExecution = "[exp].[prcGetBookingById]";
            IBooking oIH = null;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", ID);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    oIH = new BookingEntity(reader);
                }
                reader.Close();
            }
            return oIH;
        }

        public static DataSet GetSalesman(int SalesmanID)
        {
            string ProcName = "[exp].[prcGetSalesman]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@fk_CustID", SalesmanID);

            return dquery.GetTables();
        }

        public static string GetBookingChargeExists(int BookingID)
        {
            string ProcName = "[exp].[prcBookingChargesExist]";
            string Approver = "";
            //DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            //string Result = "";

            using (DbQuery oDq = new DbQuery(ProcName))
            {
                oDq.AddIntegerParam("@BookingID", BookingID);
                //oDq.AddTextParam("@RESULT", Result, QueryParameterDirection.Output);

                Approver = Convert.ToString(oDq.GetScalar());
            }

            return Approver;

         }

        public static string GetBLFromEDGE(int NVOCCID)
        {
            string ProcName = "[exp].[prcGetBLFromEDGE]";
            string Approver = "";
            //DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            //string Result = "";

            using (DbQuery oDq = new DbQuery(ProcName))
            {
                oDq.AddIntegerParam("@NVOCCID", NVOCCID);
                //oDq.AddTextParam("@RESULT", Result, QueryParameterDirection.Output);

                Approver = Convert.ToString(oDq.GetScalar());
            }

            return Approver;

        }

        public static int DeleteBookingCharges(int BookingId)
        {
            string strExecution = "[exp].[uspDeleteBookingCharges]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BookingID", BookingId);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }

    }
}
