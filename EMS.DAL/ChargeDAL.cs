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
    public sealed class ChargeDAL
    {
        private ChargeDAL()
        {

        }

        #region Haulage Charge

        public static List<IImportHaulage> GetHaulageCharge(SearchCriteria searchCriteria)
        {
            string strExecution = "[chg].[uspGetHaulageCharge]";
            List<IImportHaulage> lstChg = new List<IImportHaulage>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@LocationFrom", 50, searchCriteria.LocAbbr);
                oDq.AddVarcharParam("@LocationTo", 50, searchCriteria.LocName);
                oDq.AddVarcharParam("@ContainerSize", 2, searchCriteria.LocName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IImportHaulage chg = new ImportHaulageEntity(reader);
                    lstChg.Add(chg);
                }

                reader.Close();
            }

            return lstChg;
        }

        public static IImportHaulage GetHaulageCharge(int haulageChgID, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetHaulageCharge]";
            IImportHaulage chg = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@HaulageChgID", haulageChgID);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    chg = new ImportHaulageEntity(reader);
                }

                reader.Close();
            }

            return chg;
        }

        public static void SaveHaulageCharge(IImportHaulage chg, int modifiedBy)
        {
            string strExecution = "[chg].[uspHaulageCharge]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@HaulageChgID", chg.HaulageChgID);
                oDq.AddIntegerParam("@LocationFrom", chg.LocationFrom);
                oDq.AddIntegerParam("@LocationTo", chg.LocationTo);
                oDq.AddVarcharParam("@ContainerSize", 2, chg.ContainerSize);
                //oDq.AddDecimalParam("@WeightFrom", chg.WeightFrom);
                //oDq.AddDecimalParam("@WeightTo", chg.WeightTo);
                //oDq.AddIntegerParam("@HaulageRate", chg.HaulageRate);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        public static void DeleteHaulageCharge(int haulageChgID, int modifiedBy)
        {
            string strExecution = "[chg].[uspDeleteHaulageCharge]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@HaulageChgID", haulageChgID);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        #endregion

        #region Exchange Rate

        #endregion
    }
}
