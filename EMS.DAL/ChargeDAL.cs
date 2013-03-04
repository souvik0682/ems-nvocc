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

        //public static List<IImportHaulage> GetHaulageCharge(SearchCriteria searchCriteria)
        //{
        //    string strExecution = "[chg].[uspGetHaulageCharge]";
        //    List<IImportHaulage> lstChg = new List<IImportHaulage>();

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddVarcharParam("@LocationFrom", 50, searchCriteria.LocAbbr);
        //        oDq.AddVarcharParam("@LocationTo", 50, searchCriteria.LocName);
        //        oDq.AddVarcharParam("@ContainerSize", 2, searchCriteria.LocName);
        //        oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
        //        oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
        //        DataTableReader reader = oDq.GetTableReader();

        //        while (reader.Read())
        //        {
        //            IImportHaulage chg = new ImportHaulageEntity(reader);
        //            lstChg.Add(chg);
        //        }

        //        reader.Close();
        //    }

        //    return lstChg;
        //}

        //public static IImportHaulage GetHaulageCharge(int haulageChgID, SearchCriteria searchCriteria)
        //{
        //    string strExecution = "[common].[uspGetHaulageCharge]";
        //    IImportHaulage chg = null;

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@HaulageChgID", haulageChgID);
        //        oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
        //        oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
        //        DataTableReader reader = oDq.GetTableReader();

        //        while (reader.Read())
        //        {
        //            chg = new ImportHaulageEntity(reader);
        //        }

        //        reader.Close();
        //    }

        //    return chg;
        //}

        //public static void SaveHaulageCharge(IImportHaulage chg, int modifiedBy)
        //{
        //    string strExecution = "[chg].[uspHaulageCharge]";

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@HaulageChgID", chg.HaulageChgID);
        //        oDq.AddIntegerParam("@LocationFrom", chg.LocationFrom);
        //        oDq.AddIntegerParam("@LocationTo", chg.LocationTo);
        //        oDq.AddVarcharParam("@ContainerSize", 2, chg.ContainerSize);
        //        oDq.AddDecimalParam("@WeightFrom", 9, 3, chg.WeightFrom);
        //        oDq.AddDecimalParam("@WeightTo", 9, 3, chg.WeightTo);
        //        oDq.AddDecimalParam("@HaulageRate", 10, 2, chg.HaulageRate);
        //        oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
        //        oDq.RunActionQuery();
        //    }
        //}

        //public static void DeleteHaulageCharge(int haulageChgID, int modifiedBy)
        //{
        //    string strExecution = "[chg].[uspDeleteHaulageCharge]";

        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        oDq.AddIntegerParam("@HaulageChgID", haulageChgID);
        //        oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
        //        oDq.RunActionQuery();
        //    }
        //}

        #endregion

        #region Chargr Master

        public static DataTable GetAllCharges(SearchCriteria searchCriteria, int CompanyId)
        {
            string strExecution = "[mst].[spGetCharge]";
            //List<ICharge> lstCharge = new List<ICharge>();
            DataTable dt = new DataTable();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CompanyId", CompanyId);
                oDq.AddVarcharParam("@SchName", 200, searchCriteria.ChargeName);
                oDq.AddCharParam("@SchType", 1, searchCriteria.ChargeType);
                oDq.AddVarcharParam("@SchLine", 200, searchCriteria.LineName);
                oDq.AddVarcharParam("@SortExpression", 20, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);

                dt = oDq.GetTable();
                //DataTableReader reader = oDq.GetTableReader();

                //while (reader.Read())
                //{
                //    ICharge Chg = new ChargeEntity(reader);
                //    lstCharge.Add(Chg);
                //}
                //reader.Close();
            }
            return dt;
        }

        public static int AddEditCharge(ICharge Charge)
        {
            string strExecution = "[mst].[spAddEditCharge]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ChargesID", Charge.ChargesID);
                oDq.AddIntegerParam("@CompanyID", Charge.CompanyID);
                oDq.AddVarcharParam("@ChargeDescr", 40, Charge.ChargeDescr);
                oDq.AddIntegerParam("@ChargeType", Charge.ChargeType);
                oDq.AddCharParam("@IEC", 1, Charge.IEC);
                oDq.AddIntegerParam("@NVOCCID", Charge.NVOCCID);
                oDq.AddIntegerParam("@Sequence", Charge.Sequence);
                oDq.AddBooleanParam("@RateChangeable", Charge.RateChangeable);
                oDq.AddBooleanParam("@ChargeActive", Charge.ChargeActive);
                oDq.AddBooleanParam("@IsFreightComponent", Charge.IsFreightComponent);
                oDq.AddDateTimeParam("@EffectDt", Charge.EffectDt);
                oDq.AddBooleanParam("@TerminalRequired", Charge.IsTerminal);
                oDq.AddBooleanParam("@IsWashing", Charge.IsWashing);
                oDq.AddBooleanParam("@PrincipleSharing", Charge.PrincipleSharing);
                oDq.AddIntegerParam("@Currency", Charge.Currency);
                oDq.AddBooleanParam("@ServiceTax", Charge.ServiceTax);
                oDq.AddIntegerParam("@LocationId", Charge.Location);
                //oDq.AddNVarcharParam("@ChargeRate",8000,Charge.ConvertListToXML((List<IChargeRate>)Charge.ChargeRates));


                if (Charge.ChargesID <= 0)
                {
                    oDq.AddIntegerParam("@CreatedBy", Charge.CreatedBy);
                    oDq.AddDateTimeParam("@CreatedOn", Charge.CreatedOn);
                }
                oDq.AddIntegerParam("@ModifiedBy", Charge.ModifiedBy);
                oDq.AddDateTimeParam("@ModifiedOn", Charge.ModifiedOn);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));

            }
            return Result;
        }

        public static int AddEditChargeRates(IChargeRate ChargeRate)
        {
            string strExecution = "[mst].[spAddEditChargeRates]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ChargesRateID", ChargeRate.ChargesRateID);
                oDq.AddIntegerParam("@ChargesID", ChargeRate.ChargesID);
                oDq.AddIntegerParam("@LocationId", ChargeRate.LocationId);
                oDq.AddIntegerParam("@TerminalId", ChargeRate.TerminalId);
                oDq.AddIntegerParam("@WashingType", ChargeRate.WashingType);
                oDq.AddIntegerParam("@High", ChargeRate.High);
                oDq.AddIntegerParam("@Low", ChargeRate.Low);
                oDq.AddDecimalParam("@RatePerBL", 12, 2, ChargeRate.RatePerBL);
                oDq.AddDecimalParam("@RatePerTEU", 12, 2, ChargeRate.RatePerTEU);
                oDq.AddDecimalParam("@RatePerFEU", 12, 2, ChargeRate.RatePerFEU);
                oDq.AddDecimalParam("@RatePerTON", 12, 2, ChargeRate.RatePerTON);
                oDq.AddDecimalParam("@RatePerCBM", 12, 2, ChargeRate.RatePerCBM);
                oDq.AddDecimalParam("@SharingBL", 12, 2, ChargeRate.SharingBL);
                oDq.AddDecimalParam("@SharingTEU", 12, 2, ChargeRate.SharingTEU);
                oDq.AddDecimalParam("@SharingFEU", 12, 2, ChargeRate.SharingFEU);
                //oDq.AddDecimalParam("@ServiceTax", 12, 2, ChargeRate.ServiceTax);
                oDq.AddBooleanParam("@RateActive", ChargeRate.RateActive);


                //if (ChargeRate.ChargesRateID <= 0)
                //{
                //    oDq.AddIntegerParam("@CreatedBy", Charge.CreatedBy);
                //    oDq.AddDateTimeParam("@CreatedOn", Charge.CreatedOn);
                //}
                //oDq.AddIntegerParam("@ModifiedBy", Charge.ModifiedBy);
                //oDq.AddDateTimeParam("@ModifiedOn", Charge.ModifiedOn);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@Result"));

            }
            return Result;
        }

        public static ICharge GetChargeDetails(int ChargesID)
        {
            string strExecution = "[mst].[spGetChargeDetailsAndRate]";
            ICharge Chrg = null;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ChargeId", ChargesID);
                oDq.AddIntegerParam("@Mode", 1); // @Mode = 1 to fetch ChargeDetails
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    Chrg = new ChargeEntity(reader);
                }
                reader.Close();
            }
            return Chrg;
        }

        public static List<IChargeRate> GetChargeRates(int ChargesID)
        {
            string strExecution = "[mst].[spGetChargeDetailsAndRate]";
            List<IChargeRate> ChrgRates = new List<IChargeRate>();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ChargeId", ChargesID);
                oDq.AddIntegerParam("@Mode", 2); // @Mode = 2 to fetch ChargeRate
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IChargeRate charg = new ChargeRateEntity(reader);
                    ChrgRates.Add(charg);
                }
                reader.Close();
            }
            return ChrgRates;
        }

        public static int DeleteCharge(int ChargeId)
        {
            string strExecution = "[mst].[spDeleteCharge]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ChargeId", ChargeId);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }

        public static int DeleteChargeRate(int ChargeRateId)
        {
            string strExecution = "[mst].[spDeleteChargeRate]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ChargeRateId", ChargeRateId);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }

        public static void DeactivateAllRatesAgainstChargeId(int ChargeId)
        {
            string strExecution = "[mst].[spChargeRateStatusUpdate]";
            //int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ChargesId", ChargeId);
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

        public static int SaveExchangeRate(IExchangeRate chg, int modifiedBy)
        {
            string strExecution = "[chg].[uspSaveExchangeRate]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ExchangeRateID", chg.ExchangeRateID);
                oDq.AddIntegerParam("@CompanyID", chg.CompanyID);
                oDq.AddDateTimeParam("@ExchangeDate", chg.ExchangeDate);
                oDq.AddDecimalParam("@USDExchangeRate", 10, 2, chg.USDExchangeRate);
                //oDq.AddIntegerParam("@FreeDays", chg.FreeDays);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));
            }

            return result;
        }

        public static void DeleteExchangeRate(int exchangeRateID, int modifiedBy)
        {
            string strExecution = "[chg].[uspDeleteExchangeRate]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ExchangeRateID", exchangeRateID);
                oDq.RunActionQuery();
            }
        }

        #endregion
    }
}
