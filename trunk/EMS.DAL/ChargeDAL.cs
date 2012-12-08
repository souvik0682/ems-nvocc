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
                oDq.AddDecimalParam("@WeightFrom", 9, 3, chg.WeightFrom);
                oDq.AddDecimalParam("@WeightTo", 9, 3, chg.WeightTo);
                oDq.AddDecimalParam("@HaulageRate", 10, 2, chg.HaulageRate);
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

        public static List<IExchangeRate> GetExchangeRate(SearchCriteria searchCriteria)
        {
            string strExecution = "[chg].[uspGetExchangeRate]";
            List<IExchangeRate> lstChg = new List<IExchangeRate>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (searchCriteria.Date.HasValue)
                    oDq.AddDateTimeParam("@SchExchangeDate", searchCriteria.Date.Value);

                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IExchangeRate chg = new ExchangeRateEntity(reader);
                    lstChg.Add(chg);
                }

                reader.Close();
            }

            return lstChg;
        }

        public static IExchangeRate GetExchangeRate(int exchangeRateID, SearchCriteria searchCriteria)
        {
            string strExecution = "[chg].[uspGetExchangeRate]";
            IExchangeRate chg = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ExchangeRateID", exchangeRateID);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    chg = new ExchangeRateEntity(reader);
                }

                reader.Close();
            }

            return chg;
        }

        public static void SaveExchangeRate(IExchangeRate chg, int modifiedBy)
        {
            string strExecution = "[chg].[uspExchangeRate]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ExchangeRateID", chg.ExchangeRateID);
                oDq.AddIntegerParam("@CompanyID", chg.CompanyID);
                oDq.AddDateTimeParam("@ExchangeDate", chg.ExchangeDate);
                oDq.AddDecimalParam("@USDExchangeRate", 10, 2, chg.USDExchangeRate);
                oDq.AddIntegerParam("@FreeDays", chg.FreeDays);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        public static void DeleteExchangeRate(int exchangeRateID, int modifiedBy)
        {
            string strExecution = "[chg].[uspDeleteExchangeRate]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ExchangeRateID", exchangeRateID);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        #endregion
    }
}
