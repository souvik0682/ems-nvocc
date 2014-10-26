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
    public sealed class fwLocationDAL
    {
        public static List<IFwLocation> GetfwLoc(SearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[uspGetLocation]";
            List<IFwLocation> lstLine = new List<IFwLocation>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", searchCriteria.LocationID);
                oDq.AddVarcharParam("@SchLocName", 100, searchCriteria.LocName);
                oDq.AddVarcharParam("@SchAbbr", 100, searchCriteria.LocAbbr);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();


                while (reader.Read())
                {
                    IFwLocation oLine = new fwLocationEntity(reader);
                    lstLine.Add(oLine);
                }
                reader.Close();
            }
            return lstLine;
        }

        public static void SaveLocation(IFwLocation loc)
        {
            string strExecution = "[fwd].[uspSaveLocation]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", loc.locID);
                oDq.AddVarcharParam("@LocName", 200, loc.LocName);
                oDq.AddVarcharParam("@LocAbbr", 200, loc.Abbreviation);
                oDq.AddVarcharParam("@LocAddress", 200, loc.LocAddress.Address);
                oDq.AddVarcharParam("@LocCity", 20, loc.LocAddress.City);
                oDq.AddVarcharParam("@LocPin", 10, loc.LocAddress.Pin);
                oDq.AddVarcharParam("@LocPhone", 3, loc.Phone);
                oDq.AddIntegerParam("@ModifiedBy", loc.CreatedBy);
                oDq.AddIntegerParam("@Result", 0, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@Result"));
            }
        }

        public static int DeleteFwLoc(int LocID, int UserID)
        {
            string strExecution = "[fwd].[uspDeleteLocation]";
            int ret = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@fLocId", LocID);
                oDq.AddBigIntegerParam("@ModifiedBy", UserID);

                ret = Convert.ToInt32(oDq.GetScalar());
            }

            return ret;
        }

        public static IFwLocation GetFLoc(int ID)
        {
            string strExecution = "[fwd].[uspGetLocation]";
            IFwLocation oIH = null;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", ID);
                oDq.AddVarcharParam("@SortExpression", 30, "");
                oDq.AddVarcharParam("@SortDirection", 4, "");
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    oIH = new fwLocationEntity(reader);
                }
                reader.Close();
            }
            return oIH;
        }
    }
}
