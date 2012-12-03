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

        public static int SaveLocation(ILocation loc, int modifiedBy)
        {
            string strExecution = "[common].[uspSaveLocation]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", loc.Id);
                oDq.AddVarcharParam("@LocName", 50, loc.Name);
                oDq.AddVarcharParam("@LocAddress", 200, loc.LocAddress.Address);
                oDq.AddVarcharParam("@LocCity", 20, loc.LocAddress.City);
                oDq.AddVarcharParam("@LocPin", 10, loc.LocAddress.Pin);
                oDq.AddVarcharParam("@LocAbbr", 3, loc.Abbreviation);
                oDq.AddVarcharParam("@LocPhone", 30, loc.Phone);
                oDq.AddIntegerParam("@ManagerId", loc.ManagerId);
                oDq.AddCharParam("@IsActive", 1, loc.IsActive);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));

            }

            return result;
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
