using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;

namespace EMS.DAL
{
    public sealed class CommonDAL
    {
        private CommonDAL()
        {
        }

        #region Common

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

        #endregion

        #region Location

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
                oDq.AddVarcharParam("@PickUpFooter", 300, loc.PickUpFooter);
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

        public static void DeleteLocation(int locId, int modifiedBy)
        {
            string strExecution = "[common].[uspDeleteLocation]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

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
    }
}
