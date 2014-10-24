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
    public sealed class fwLineDAL
    {
        public static List<IFwLine> GetfwLine(SearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[uspGetLine]";
            List<IFwLine> lstLine = new List<IFwLine>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LineId", searchCriteria.LineID);
                oDq.AddVarcharParam("@SchLineName", 100, searchCriteria.LineName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();


                while (reader.Read())
                {
                    IFwLine oLine = new fwLineEntity(reader);
                    lstLine.Add(oLine);
                }
                reader.Close();
            }
            return lstLine;
        }

        public static List<IFwLine> GetfwLineByType(SearchCriteria searchCriteria)
        {
            string strExecution = "[fwd].[uspGetLineByType]";
            List<IFwLine> lstLine = new List<IFwLine>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {                           
                oDq.AddVarcharParam("@SchType", 100, searchCriteria.StringOption1);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();      

                while (reader.Read())
                {
                    lstLine.Add(new fwLineEntity(reader));
                }
                reader.Close();
            }
            return lstLine;
        }

        public static int AddEditFwLine(IFwLine Line, int CompanyId, string mode)
        {
            string strExecution = "[fwd].[uspManageLine]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@mode", 1, mode);
                oDq.AddIntegerParam("@UserID", Line.CreatedBy);
                oDq.AddIntegerParam("@CompanyID", CompanyId);
                oDq.AddIntegerParam("@fLineId", Line.LineID);
                oDq.AddVarcharParam("@fLineName", 20, Line.LineName);
                oDq.AddVarcharParam("@fLineType", 20, Line.LineType);
                oDq.AddVarcharParam("@Prefix", 20, Line.Prefix);
                oDq.AddBooleanParam("@LineStatus", Line.LineStatus);
                oDq.AddIntegerParam("@Result", Result, QueryParameterDirection.Output);
                //oDq.AddIntegerParam("@LeaseId", outBookingId, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@Result"));
                ////JobId = Convert.ToInt32(oDq.GetParaValue("@JobId"));
            }
            return Result;
        }

        public static int DeleteFwLine(int LineID, int UserID)
        {
            string strExecution = "[fwd].[uspManageLine]";
            int ret = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@fLineId", LineID);
                oDq.AddVarcharParam("@Mode", 1, "D");
                oDq.AddBigIntegerParam("@UserID", UserID);

                ret = Convert.ToInt32(oDq.GetScalar());
            }

            return ret;
        }

        public static IFwLine GetFLine(int ID)
        {
            string strExecution = "[fwd].[uspGetLine]";
            IFwLine oIH = null;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LineId", ID);
                oDq.AddVarcharParam("@SortExpression", 30, "");
                oDq.AddVarcharParam("@SortDirection", 4, "");
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    oIH = new fwLineEntity(reader);
                }
                reader.Close();
            }
            return oIH;
        }
    }
}
