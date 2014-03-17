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
    public sealed class LeaseDAL
    {
        public static List<ILease> GetLease(SearchCriteria searchCriteria, int ID)
        {
            string strExecution = "[dbo].[prcGetLeaseList]";
            List<ILease> lstService = new List<ILease>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_LeaseID", ID);
                oDq.AddVarcharParam("@SchLeaseNo", 100, searchCriteria.StringOption1);
                oDq.AddVarcharParam("@SchLineName", 100, searchCriteria.LineName);
                oDq.AddVarcharParam("@SchLocationName", 100, searchCriteria.Location);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();


                while (reader.Read())
                {
                    ILease oIH = new LeaseEntity(reader);
                    lstService.Add(oIH);
                }
                reader.Close();
            }
            return lstService;
        }

        public static ILease GetLease(int ID)
        {
            string strExecution = "[dbo].[prcGetLeaseList]";
            ILease oIH = null;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_LeaseID", ID);
                oDq.AddVarcharParam("@SortExpression", 30, "");
                oDq.AddVarcharParam("@SortDirection", 4, "");
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    oIH = new LeaseEntity(reader);
                }
                reader.Close();
            }
            return oIH;
        }

        public static int AddEditLease(ILease Booking, int CompanyId, ref int LeaseId)
        {
            string strExecution = "[dbo].[prcAddEditLease]";
            int Result = 0;
            int outBookingId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@userID", Booking.CreatedBy);
                oDq.AddIntegerParam("@fk_FinYearID", 1);
                oDq.AddIntegerParam("@fk_CompanyID", CompanyId);
                oDq.AddBooleanParam("@isedit", Booking.Action);
                oDq.AddBigIntegerParam("@pk_LeaseID", Booking.LeaseID);
                oDq.AddIntegerParam("@fk_LineID", Booking.LinerID);
                oDq.AddIntegerParam("@fk_LocationID", Booking.LocationID);
                oDq.AddVarcharParam("@LeaseNo", 20, Booking.LeaseNo);
                oDq.AddIntegerParam("@fk_EmptyYardID", Booking.fk_EmptyYardID);
                oDq.AddDateTimeParam("@LeaseDate", Booking.LeaseDate);
                oDq.AddDateTimeParam("@LeaseValidTill", Booking.LeaseValidTill);
                oDq.AddVarcharParam("@LeaseCompany", 100, Booking.LeaseCompany);
                oDq.AddVarcharParam("@LeaseDescription", 100, Booking.Description);
                

                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.AddIntegerParam("@LeaseId", outBookingId, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));
                LeaseId = Convert.ToInt32(oDq.GetParaValue("@LeaseId"));
            }
            return Result;
        }

        public static void DeactivateAllContainersAgainstLeaseId(int LeaseId)
        {
            string strExecution = "[dbo].[sp_LeaseContainersUpdate]";
            //int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LeaseId", LeaseId);
                oDq.RunActionQuery();
            }
        }

        public static int AddEditLeaseContainer(IBookingContainer Container)
        {
            string strExecution = "[dbo].[spAddEditLeaseContainers]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LeaseContainerID", Container.BookingContainerID);
                oDq.AddIntegerParam("@LeaseID", Container.BookingID);
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

        public static List<IBookingContainer> GetLeaseContainers(int LeaseID)
        {
            string strExecution = "[dbo].[spGetLeaseContainerList]";
            List<IBookingContainer> Containers = new List<IBookingContainer>();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LeaseId", LeaseID);
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

        public static int CheckForDuplicateLease(string LeaseNo)
        {
            string strExecution = "[dbo].[spCheckLeaseReference]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@LeaseNo", 20, LeaseNo);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@Result"));

            }
            return Result;
        }

        public static int DeleteLease(int LeaseId)
        {
            string strExecution = "[dbo].[prcDeleteLease]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_LeaseID", LeaseId);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }

    }
}
