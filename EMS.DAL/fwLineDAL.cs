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
        public static List<IFwLine> GetfwLine(SearchCriteria searchCriteria, int ID)
        {
            string strExecution = "[fwd].[uspGetLine]";
            List<IFwLine> lstLine = new List<IFwLine>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LineId", ID);
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

        public static int AddEditFwLine(IFwLine Line, int CompanyId)
        {
            string strExecution = "[fwd].[uspManageLine]";
            int Result = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@userID", Line.CreatedBy);
                oDq.AddIntegerParam("@fk_CompanyID", CompanyId);
                oDq.AddBigIntegerParam("@fLineId", Line.LineID);
                oDq.AddVarcharParam("@fLineName", 20, Line.LineName);
                oDq.AddVarcharParam("@fLineType", 20, Line.LineType);
                oDq.AddBooleanParam("@LineStatus", Line.LineStatus);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                //oDq.AddIntegerParam("@LeaseId", outBookingId, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));
                ////JobId = Convert.ToInt32(oDq.GetParaValue("@JobId"));
            }
            return Result;
        }

        public static int DeleteJob(int UnitTypeID, int UserID)
        {
            string strExecution = "[fwd].[uspManageLine]";
            int ret = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@fLineId", UnitTypeID);
                oDq.AddVarcharParam("@Mode", 1, "D");
                oDq.AddBigIntegerParam("@UserID", UserID);

                ret = Convert.ToInt32(oDq.GetScalar());
            }

            return ret;
        }
    }
}
