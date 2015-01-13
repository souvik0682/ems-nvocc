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
    public sealed class UnitDAL
    {
        public static List<IUnit> GetUnits(SearchCriteria searchCriteria, int ID, int CompanyID)
        {
            string strExecution = "[fwd].[uspGetUnitType]";
            List<IUnit> lstUnit = new List<IUnit>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UnitTypeId", ID);
                oDq.AddIntegerParam("@CompanyID", CompanyID);
                oDq.AddVarcharParam("@SchUnitName", 100, searchCriteria.LineName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();


                while (reader.Read())
                {
                    IUnit oUnit = new UnitEntity(reader);
                    lstUnit.Add(oUnit);
                }
                reader.Close();
            }
            return lstUnit;
        }

        public static int AddEditUnit(IUnit Units, int CompanyId, string Mode)
        {
            string strExecution = "[fwd].[uspManageUnitType]";
            int Result = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@Mode", 1, Mode);
                oDq.AddIntegerParam("@userID", Units.CreatedBy);
                oDq.AddIntegerParam("@fk_CompanyID", CompanyId);
                oDq.AddBigIntegerParam("@UnitTypeId", Units.UnitTypeID);
                oDq.AddVarcharParam("@UnitType", 1, Units.UnitType);
                oDq.AddVarcharParam("@UnitName", 20, Units.UnitName);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                //oDq.AddIntegerParam("@LeaseId", outBookingId, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));
                //JobId = Convert.ToInt32(oDq.GetParaValue("@JobId"));
            }
            return Result;
        }

        public static int DeleteJob(int UnitTypeID, int UserID)
        {
            string strExecution = "[fwd].[uspManageUnitType]";
            int ret = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@UnitTypeId", UnitTypeID);
                oDq.AddVarcharParam("@Mode", 1, "D");
                oDq.AddBigIntegerParam("@UserID", UserID);

                ret = Convert.ToInt32(oDq.GetScalar());
            }

            return ret;
        }
    }
}
