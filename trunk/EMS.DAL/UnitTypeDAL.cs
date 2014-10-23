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
    public sealed class UnitTypeDAL
    {

        //public static List<IUnitType> GetUnitType(SearchCriteria searchCriteria, int ID, int CompanyID)
        //{
        //    string strExecution = "[fwd].[uspGetParty]";
        //    List<IUnitType> lstUnit = new List<IUnitType>();
        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@UnitTypeId", ID);
        //        oDq.AddIntegerParam("@CompanyID", CompanyID);
        //        oDq.AddVarcharParam("@SchUnitName", 100, searchCriteria.LineName);
        //        oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
        //        oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
        //        DataTableReader reader = oDq.GetTableReader();
        //        while (reader.Read())
        //        {
        //            IUnitType oUnit = new UnitTypeEntity(reader);
        //            lstUnit.Add(oUnit);
        //        }

        //        reader.Close();
        //    }
        //    return lstUnit;
        //}

        public static int AddEditUnit(IUnitType Units, int CompanyId)
        {
            string strExecution = "[fwd].[uspManageUnitType]";
            int Result = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@userID", Units.CreatedBy);
                oDq.AddIntegerParam("@fk_CompanyID", CompanyId);
                oDq.AddBigIntegerParam("@UnitTypeId", Units.UnitTypeID);
                oDq.AddVarcharParam("@UnitName", 20, Units.UnitName);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                //oDq.AddIntegerParam("@LeaseId", outBookingId, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));
                //JobId = Convert.ToInt32(oDq.GetParaValue("@JobId"));
            }
            return Result;
        }

        public static int DeleteUnit(int UnitTypeID, int UserID)
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
