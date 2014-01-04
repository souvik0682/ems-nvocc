using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;
using EMS.Utilities;
namespace EMS.DAL
{
    public sealed class CommonDAL
    {
        private CommonDAL()
        {
        }

        #region Common

        public static DataTable GetVoyageList(string prefixText)
        {
            string strExecution = "[common].[uspGetVoyageList]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@InitialChar", 100, prefixText);
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetVesselList(string prefixText)
        {
            string strExecution = "[common].[uspGetVesselList]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@InitialChar", 100, prefixText);
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        /// <summary>
        /// Saves the error log.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="message">The message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>02/12/2012</createddate>
        public static void SaveErrorLog(int userId, string message, string stackTrace)
        {
            string strExecution = "[admin].[uspSaveError]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                oDq.AddVarcharParam("@ErrorMessage", 255, message);
                oDq.AddVarcharParam("@StackTrace", -1, stackTrace);
                oDq.RunActionQuery();
            }
        }


        /// <summary>
        /// Common method to populate all the dropdownlist throughout the application
        /// </summary>
        /// <param name="Number">Unique Number</param>
        /// <returns>DataTable</returns>
        /// <createdby>Rajen Saha</createdby>
        /// <createddate>01/12/2012</createddate>
        public static DataTable PopulateDropdown(int Number, int? Filter1, int? Filter2)
        {
            string strExecution = "[common].[spPopulateDropDownList]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Number", Number);
                oDq.AddIntegerParam("@Filter", Filter1.Value);
                oDq.AddIntegerParam("@Type", Filter2.Value);


                return oDq.GetTable();
            }
        }

        #endregion

        #region Location

        //New Function Added By Souvik - 11-06-2013
        public static List<ILocation> GetLocation_New(char isActiveOnly, int UserId, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetLocation_New]"; //Create a new SP with this Name (Previous one was : uspGetLocation)
            List<ILocation> lstLoc = new List<ILocation>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddIntegerParam("@UserId", UserId);
                oDq.AddVarcharParam("@SchAbbr", 3, searchCriteria.LocAbbr);
                oDq.AddVarcharParam("@SchLocName", 50, searchCriteria.LocName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ILocation loc = new LocationEntity(reader);
                    lstLoc.Add(loc);
                }

                reader.Close();
            }

            return lstLoc;
        }

        public static List<ILocation> GetLocation(char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetLocation]";
            List<ILocation> lstLoc = new List<ILocation>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SchAbbr", 3, searchCriteria.LocAbbr);
                oDq.AddVarcharParam("@SchLocName", 50, searchCriteria.LocName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ILocation loc = new LocationEntity(reader);
                    lstLoc.Add(loc);
                }

                reader.Close();
            }

            return lstLoc;
        }

        //public static List<ISlot> GetOperator(SearchCriteria searchCriteria)
        //{
        //    string strExecution = "[exp].[prcGetSlotOperatorList]";
        //    List<ISlot> lstSlot = new List<ISlot>();

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddVarcharParam("@SchOperatorName", 50, searchCriteria.SlotOperatorName);
        //        oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
        //        oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
        //        DataTableReader reader = oDq.GetTableReader();

        //        while (reader.Read())
        //        {
        //            ISlot Oper = new SlotEntity(reader);
        //            lstSlot.Add(Oper);
        //        }

        //        reader.Close();
        //    }

        //    return lstSlot;
        //}
        public static ILocation GetLocation(int locId, char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetLocation]";
            ILocation loc = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    loc = new LocationEntity(reader);
                }

                reader.Close();
            }

            return loc;
        }

        public static void SaveLocation(ILocation loc, int modifiedBy)
        {
            string strExecution = "[common].[uspSaveLocation]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", loc.Id);
                oDq.AddIntegerParam("@PGRFreeDays", loc.PGRFreeDays);
                oDq.AddVarcharParam("@CanFooter", 300, loc.CanFooter);
                oDq.AddVarcharParam("@SlotFooter", 300, loc.SlotFooter);
                oDq.AddVarcharParam("@CartingFooter", 300, loc.CartingFooter);
                oDq.AddVarcharParam("@PickUpFooter", 3000, loc.PickUpFooter);
                oDq.AddVarcharParam("@CustomHouseCode", 6, loc.CustomHouseCode);
                oDq.AddVarcharParam("@GatewayPort", 6, loc.GatewayPort);
                oDq.AddVarcharParam("@ICEGateLoginD", 20, loc.ICEGateLoginD);
                oDq.AddVarcharParam("@PCSLoginID", 8, loc.PCSLoginID);
                oDq.AddVarcharParam("@ISO20", 4, loc.ISO20);
                oDq.AddVarcharParam("@ISO40", 4, loc.ISO40);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        //public static void DeleteLocation(int locId, int modifiedBy)
        //{
        //    string strExecution = "[common].[uspDeleteLocation]";

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@LocId", locId);
        //        oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
        //        oDq.RunActionQuery();
        //    }
        //}

        public static List<ILocation> GetLocationByUser(int userId)
        {
            string strExecution = "[common].[uspGetLocationByUser]";
            List<ILocation> lstLoc = new List<ILocation>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ILocation loc = new LocationEntity(reader);
                    lstLoc.Add(loc);
                }

                reader.Close();
            }

            return lstLoc;
        }

        #endregion

        #region Area

        //public static List<IArea> GetArea(char isActiveOnly, SearchCriteria searchCriteria)
        //{
        //    string strExecution = "[common].[uspGetArea]";
        //    List<IArea> lstArea = new List<IArea>();

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
        //        oDq.AddVarcharParam("@SchLocName", 50, searchCriteria.LocName);
        //        oDq.AddVarcharParam("@SchAreaName", 50, searchCriteria.AreaName);
        //        oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
        //        oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
        //        DataTableReader reader = oDq.GetTableReader();

        //        while (reader.Read())
        //        {
        //            IArea area = new AreaEntity(reader);
        //            lstArea.Add(area);
        //        }

        //        reader.Close();
        //    }

        //    return lstArea;
        //}

        //public static IArea GetArea(int areaId, char isActiveOnly, SearchCriteria searchCriteria)
        //{
        //    string strExecution = "[common].[uspGetArea]";
        //    IArea area = null;

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@AreaId", areaId);
        //        oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
        //        oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
        //        oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
        //        DataTableReader reader = oDq.GetTableReader();

        //        while (reader.Read())
        //        {
        //            area = new AreaEntity(reader);
        //        }

        //        reader.Close();
        //    }

        //    return area;
        //}

        //public static int SaveArea(IArea area, int modifiedBy)
        //{
        //    string strExecution = "[common].[uspSaveArea]";
        //    int result = 0;

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@AreaId", area.Id);
        //        oDq.AddVarcharParam("@AreaName", 50, area.Name);
        //        oDq.AddVarcharParam("@PinCode", 10, area.PinCode);
        //        oDq.AddIntegerParam("@LocId", area.Location.Id);
        //        oDq.AddCharParam("@IsActive", 1, area.IsActive);
        //        oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
        //        oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
        //        oDq.RunActionQuery();
        //        result = Convert.ToInt32(oDq.GetParaValue("@Result"));
        //    }

        //    return result;
        //}

        //public static void DeleteArea(int areaId, int modifiedBy)
        //{
        //    string strExecution = "[common].[uspDeleteArea]";

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@AreaId", areaId);
        //        oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
        //        oDq.RunActionQuery();
        //    }
        //}

        //public static List<IArea> GetAreaByLocation(int locId)
        //{
        //    string strExecution = "[common].[uspGetAreaByLocation]";
        //    List<IArea> lstArea = new List<IArea>();

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@LocId", locId);
        //        DataTableReader reader = oDq.GetTableReader();

        //        while (reader.Read())
        //        {
        //            IArea area = new AreaEntity(reader);
        //            lstArea.Add(area);
        //        }

        //        reader.Close();
        //    }

        //    return lstArea;
        //}

        public static List<IArea> GetAreaByLocationAndPinCode(int locId, string pinCode)
        {
            string strExecution = "[common].[uspAreaByLocationAndPinCode]";
            List<IArea> lstArea = new List<IArea>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddVarcharParam("@PinCode", 10, pinCode);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IArea area = new AreaEntity(reader);
                    lstArea.Add(area);
                }

                reader.Close();
            }

            return lstArea;
        }

        #endregion

        #region Group Company

        public static List<IGroupCompany> GetGroupCompany(char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetGroupCompany]";
            List<IGroupCompany> lstGroupCompany = new List<IGroupCompany>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SchGroupName", 50, searchCriteria.GroupName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IGroupCompany groupCompany = new GroupCompanyEntity(reader);
                    lstGroupCompany.Add(groupCompany);
                }

                reader.Close();
            }

            return lstGroupCompany;
        }

        #endregion

        #region Customer

        public static List<ICustomer> GetCustomerList(char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetCustomerList]";
            List<ICustomer> lstCustomer = new List<ICustomer>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", searchCriteria.UserId);
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SchLocAbbr", 3, searchCriteria.LocAbbr);
                oDq.AddVarcharParam("@SchCustName", 60, searchCriteria.CustomerName);
                oDq.AddVarcharParam("@SchGroupName", 50, searchCriteria.GroupName);
                oDq.AddVarcharParam("@SchExecutiveName", 50, searchCriteria.ExecutiveName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICustomer customer = new CustomerEntity(reader);
                    lstCustomer.Add(customer);
                }

                reader.Close();
            }

            return lstCustomer;
        }

        public static List<ICustomer> GetCustomer(char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetCustomer]";
            List<ICustomer> lstCustomer = new List<ICustomer>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SchLocAbbr", 3, searchCriteria.LocAbbr);
                oDq.AddVarcharParam("@SchCustName", 60, searchCriteria.CustomerName);
                oDq.AddVarcharParam("@SchGroupName", 50, searchCriteria.GroupName);
                oDq.AddIntegerParam("@SalesExecutiveId", searchCriteria.UserId);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICustomer customer = new CustomerEntity(reader);
                    lstCustomer.Add(customer);
                }

                reader.Close();
            }

            return lstCustomer;
        }

        public static ICustomer GetCustomer(int customerId, char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetCustomer]";
            ICustomer customer = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CustId", customerId);
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    customer = new CustomerEntity(reader);
                }

                reader.Close();
            }

            return customer;
        }

        //public static int SaveCustomer(ICustomer customer, int modifiedBy)
        //{
        //    string strExecution = "[common].[uspSaveCustomer]";
        //    int result = 0;

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@CustId", customer.Id);
        //        oDq.AddIntegerParam("@GroupId", customer.Group.Id);
        //        oDq.AddIntegerParam("@LocId", customer.Location.Id);
        //        oDq.AddIntegerParam("@AreaId", customer.Area.Id);
        //        oDq.AddIntegerParam("@CustTypeId", customer.CustType.Id);
        //        oDq.AddCharParam("@CorporateOrLocal", 1, customer.CorporateOrLocal);
        //        oDq.AddVarcharParam("@CustomerName", 60, customer.Name);
        //        oDq.AddVarcharParam("@Address", 200, customer.Address.Address);
        //        oDq.AddVarcharParam("@City", 50, customer.Address.City);
        //        oDq.AddVarcharParam("@Pin", 10, customer.Address.Pin);
        //        oDq.AddVarcharParam("@Phone1", 50, customer.Phone1);
        //        oDq.AddVarcharParam("@Phone2", 50, customer.Phone2);
        //        oDq.AddVarcharParam("@ContactPerson1", 50, customer.ContactPerson1.Name);
        //        oDq.AddVarcharParam("@ContactDesignation1", 50, customer.ContactPerson1.Designation);
        //        oDq.AddVarcharParam("@ContactMobile1", 15, customer.ContactPerson1.Mobile);
        //        oDq.AddVarcharParam("@ContactEmailId1", 50, customer.ContactPerson1.EmailId);
        //        oDq.AddVarcharParam("@ContactPerson2", 50, customer.ContactPerson2.Name);
        //        oDq.AddVarcharParam("@ContactDesignation2", 50, customer.ContactPerson2.Designation);
        //        oDq.AddVarcharParam("@ContactMobile2", 15, customer.ContactPerson2.Mobile);
        //        oDq.AddVarcharParam("@ContactEmailId2", 50, customer.ContactPerson2.EmailId);
        //        oDq.AddVarcharParam("@CustomerProfile", 500, customer.CustomerProfile);
        //        oDq.AddVarcharParam("@PAN", 10, customer.PAN);
        //        oDq.AddVarcharParam("@TAN", 15, customer.TAN);
        //        oDq.AddVarcharParam("@BIN", 15, customer.BIN);
        //        oDq.AddVarcharParam("@IEC", 15, customer.IEC);
        //        oDq.AddIntegerParam("@SalesExecutiveId", customer.SalesExecutiveId);
        //        oDq.AddCharParam("@IsActive", 1, customer.IsActive);
        //        oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
        //        oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
        //        oDq.RunActionQuery();
        //        result = Convert.ToInt32(oDq.GetParaValue("@Result"));
        //    }

        //    return result;
        //}

        //public static void DeleteCustomer(int customerId, int modifiedBy)
        //{
        //    string strExecution = "[common].[uspDeleteCustomer]";

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@CustId", customerId);
        //        oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
        //        oDq.RunActionQuery();
        //    }
        //}

        //public static List<ICustomer> GetCustomerByUser(int salesExecutiveId)
        //{
        //    string strExecution = "[common].[uspGetCustomerByUser]";
        //    List<ICustomer> lstCustomer = new List<ICustomer>();

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@SalesExecutiveId", salesExecutiveId);
        //        DataTableReader reader = oDq.GetTableReader();

        //        while (reader.Read())
        //        {
        //            ICustomer customer = new CustomerEntity(reader);
        //            lstCustomer.Add(customer);
        //        }

        //        reader.Close();
        //    }

        //    return lstCustomer;
        //}

        #endregion

        #region Customer Type

        public static List<ICustomerType> GetCustomerType(char isActiveOnly)
        {
            string strExecution = "[common].[uspGetCustomerType]";
            List<ICustomerType> lstCustomerType = new List<ICustomerType>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICustomerType customerType = new CustomerTypeEntity(reader);
                    lstCustomerType.Add(customerType);
                }

                reader.Close();
            }

            return lstCustomerType;
        }

        public static ICustomerType GetCustomerType(int custTypeId, char isActiveOnly)
        {
            string strExecution = "[common].[uspGetCustomerType]";
            ICustomerType customerType = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CustTypeId", custTypeId);
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    customerType = new CustomerTypeEntity(reader);
                }

                reader.Close();
            }

            return customerType;
        }

        #endregion

        #region User

        public static List<IUser> GetSalesExecutiveNew(int userId)
        {
            string strExecution = "[common].[uspGetSalesExecutiveNew]";
            List<IUser> lstUser = new List<IUser>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IUser user = new UserEntity(reader);
                    lstUser.Add(user);
                }

                reader.Close();
            }

            return lstUser;
        }

        #endregion

        #region Container Type

        public static IList<IContainerType> GetContainerType()
        {
            string strExecution = "[common].[uspGetContainerType]";
            List<IContainerType> lstContainerType = new List<IContainerType>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    lstContainerType.Add(new ContainerType(reader));
                }

                reader.Close();
            }

            return lstContainerType;
        }

        #endregion


        public static void SavePrintCount(string blNo)
        {
            string strExecution = "prcSaveInvoicePrintCount";
            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddVarcharParam("@BlNo", 60, blNo);
                oDq.RunActionQuery();
            }

        }

        #region BLHeader
        public static DataTable GetBLHeaderByBLNo(long LocationId)
        {
            string strExecution = "[dbo].[uspGetBLHeaderByBLNo]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@LocationId", LocationId);
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }
        #endregion

        #region ExpBLHeader
        public static DataTable GetExpBLHeaderByBLNo(long LocationId)
        {
            string strExecution = "[exp].[uspExpGetBLHeaderByBLNo]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@LocationId", LocationId);
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetExpBL(long LocationId, long LineID, long VoyageID)
        {
            string strExecution = "[exp].[uspGetExpBLno]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@LocationId", LocationId);
                oDq.AddBigIntegerParam("@NVOCCId", LineID);
                oDq.AddBigIntegerParam("@VoyageID", VoyageID);
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }
        #endregion
        //#region Export
        //public static DataTable GetExportVoyages(string Vessel)
        //{
        //    string strExecution = "[exp].[spGetVoyageByVesselID]";
        //    DataTable myDataTable;

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@Vessel", Vessel.ToInt());
        //        myDataTable = oDq.GetTable();
        //    }

        //    return myDataTable;
        //}

        //#endregion

        #region Report

        public static DataTable GenerateExcel(string Location, string Vessel, string PortOfDischarge, string Line, string Voyage, string VIANo)
        {
            string strExecution = "GenerateExcel";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {

                //oDq.AddVarcharParam("@filename", 1000, filename);
                oDq.AddVarcharParam("@Location", 60, Location);
                oDq.AddVarcharParam("@Vessel", 60, Vessel);
                oDq.AddVarcharParam("@PortOfDischarge", 60, PortOfDischarge);
                oDq.AddVarcharParam("@Line", 60, Line);
                oDq.AddVarcharParam("@Voyage", 60, Voyage);
                oDq.AddVarcharParam("@VIANo", 200, VIANo);
                dt = oDq.GetTable();
                //oDq.AddIntegerParam("@return", 0, QueryParameterDirection.Output);
                //oDq.RunActionQuery();
                //var result = Convert.ToInt32(oDq.GetParaValue("@return"));
                //if (result == 1) return true;
            }
            return dt;
            //return false;
        }

        public static bool GenerateTxt(string filename, int Location, int Vessel, int PortOfDischarge, int Line, int Voyage, int VIANo)
        {
            string strExecution = "[trn].[GenAdvanceContList]";


            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddNVarcharParam("@filename", 1000, filename);
                oDq.AddIntegerParam("@Loc", Location);
                oDq.AddIntegerParam("@Vsl", Vessel);
                oDq.AddIntegerParam("@Pod", PortOfDischarge);
                oDq.AddIntegerParam("@Line", Line);
                oDq.AddIntegerParam("@Vog", Voyage);
                oDq.AddIntegerParam("@VIA", VIANo);
                oDq.AddBooleanParam("@Result", false,QueryParameterDirection.Output);
                oDq.RunActionQuery();
                return Convert.ToBoolean(oDq.GetParaValue("@Result"));
            }
            
        }


        public static string GetTerminalType(int VoyageID, int VesselID, int PortOfDischarge)
        {
            //
            string strExecution = "[trn].[GetTerminalType]";
            string TerminalType = string.Empty;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@VoyageID",VoyageID);
                oDq.AddIntegerParam("@VesselID", VesselID);
                oDq.AddIntegerParam("@PortOfDischarge", PortOfDischarge);
                TerminalType = Convert.ToString(oDq.GetScalar());
            }
            return TerminalType;
        }

        public static DataTable GetLine(string Location)
        {
            string strExecution = "rptUspGetLineByLoc";
            DataTable myDataTable;
            if (Location == "All")
                Location = "0";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Location", Location.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetExpLine(string Location)
        {
            string strExecution = "[exp].[rptUspGetLineByLoc]";
            DataTable myDataTable;
            if (Location == "All")
                Location = "0";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Location", Location.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetExpPOL(string Location, string Line, string Vessel, string Voyage)
        {
            string strExecution = "[exp].[rptUspGetPOL]";
            DataTable myDataTable;
            //if (Location == "All")
            //    Location = "0";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Location", Location.ToInt());
                oDq.AddIntegerParam("@Line", Line.ToInt());
                oDq.AddIntegerParam("@Vessel", Vessel.ToInt());
                oDq.AddIntegerParam("@Voyage", Voyage.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetLineForHire(string Location)
        {
            string strExecution = "rptUspGetLineByLocForHireContainer";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Location", Location.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }
        public static DataTable GetVessels(string Line)
        {
            string strExecution = "rptUspGetVesselByNVOCCID";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@line", Line.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetVoyages(string Vessel, string Line)
        {
            string strExecution = "rptUspGetVoyageByVesselID";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Vessel", Vessel.ToInt()); oDq.AddIntegerParam("@line", Line.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetExpVoyages(string Vessel, string Location)
        {
            string strExecution = "[exp].[spGetVoyageByVesselID]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Vessel", Vessel.ToInt());
                oDq.AddIntegerParam("@LocationID", Location.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }


        public static DataTable GetVoyages(string Vessel)
        {
            string strExecution = "[dbo].[rptUspGetVoyageByVessel]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Vessel", Vessel.ToInt()); 
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetBLNo(string line, string Vessel, string Voyage)
        {
            string strExecution = "rptUspGetLineBLNo";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@line", line.ToInt());
                oDq.AddIntegerParam("@Vessel", Vessel.ToInt());
                oDq.AddIntegerParam("@voyage", Voyage.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }
        public static DataTable GetInvoiceByBLNo(string BLNo)
        {
            string strExecution = "rptPrcGetInvoice";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@LineBLNo", 60, BLNo);
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }
        #endregion

        #region Currency
        public static DataTable GetAllCurrency()
        {
            string strExecution = "[exp].[prcGetAllCurrency]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {              
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }
        #endregion

        public static DataSet GetCompanyDetails(Int32 companyId)
        {
            string ProcName = "[dbo].[PrcRptCompanyDetails]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@CompanyId", companyId);
            return dquery.GetTables();
        }
    }
}
