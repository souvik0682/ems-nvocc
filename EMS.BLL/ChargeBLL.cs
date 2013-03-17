﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;
using EMS.Common;
using EMS.DAL;
using EMS.Entity;
using EMS.Utilities;
using EMS.Utilities.Cryptography;
using EMS.Utilities.ResourceManager;

namespace EMS.BLL
{
    public class ChargeBLL
    {
        #region Haulage Charge

        //private void SetDefaultSearchCriteriaForHaulageCharge(SearchCriteria searchCriteria)
        //{
        //    searchCriteria.SortExpression = "Location";
        //    searchCriteria.SortDirection = "ASC";
        //}

        //public List<IImportHaulage> GetHaulageCharge(SearchCriteria searchCriteria)
        //{
        //    return ChargeDAL.GetHaulageCharge(searchCriteria);
        //}

        //public IImportHaulage GetHaulageCharge(int haulageChgID)
        //{
        //    SearchCriteria searchCriteria = new SearchCriteria();
        //    SetDefaultSearchCriteriaForHaulageCharge(searchCriteria);
        //    return ChargeDAL.GetHaulageCharge(haulageChgID, searchCriteria);
        //}

        //public void SaveHaulageCharge(IImportHaulage haulageChgID, int modifiedBy)
        //{
        //    ChargeDAL.SaveHaulageCharge(haulageChgID, modifiedBy);
        //}

        //public void DeleteHaulageCharge(int haulageChgID, int modifiedBy)
        //{
        //    ChargeDAL.DeleteHaulageCharge(haulageChgID, modifiedBy);
        //}

        #endregion

        #region Charge
        public int AddEditCharge(ICharge Charge)
        {
            return ChargeDAL.AddEditCharge(Charge);
        }

        public int AddEditChargeRates(IChargeRate ChargeRate)
        {
            return ChargeDAL.AddEditChargeRates(ChargeRate);
        }

        public ICharge GetChargeDetails(int ChargesID)
        {
            return ChargeDAL.GetChargeDetails(ChargesID);
        }

        public List<IChargeRate> GetChargeRates(int ChargesID)
        {
            return ChargeDAL.GetChargeRates(ChargesID);
        }

        public static DataTable GetAllCharges(SearchCriteria searchCriteria, int CompanyId)
        {
            return ChargeDAL.GetAllCharges(searchCriteria, CompanyId);
        }

        public static int DeleteCharge(int ChargeId)
        {
            return ChargeDAL.DeleteCharge(ChargeId);
        }

        public static int DeleteChargeRate(int ChargeRateId)
        {
            return ChargeDAL.DeleteChargeRate(ChargeRateId);
        }

        public void DeactivateAllRatesAgainstChargeId(int ChargeId)
        {
            ChargeDAL.DeactivateAllRatesAgainstChargeId(ChargeId);
        }

        #endregion

        #region Exchange Rate

        private void SetDefaultSearchCriteriaForExchangeRate(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Date";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IExchangeRate> GetExchangeRate(SearchCriteria searchCriteria)
        {
            return ChargeDAL.GetExchangeRate(searchCriteria);
        }

        public IExchangeRate GetExchangeRate(int exchangeRateID)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForExchangeRate(searchCriteria);
            return ChargeDAL.GetExchangeRate(exchangeRateID, searchCriteria);
        }

        public string SaveExchangeRate(IExchangeRate exchangeRate, int modifiedBy)
        {
            exchangeRate.CompanyID = Constants.DEFAULT_COMPANY_ID;

            int result = 0;
            string errMessage = string.Empty;
            result = ChargeDAL.SaveExchangeRate(exchangeRate, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00073");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteExchangeRate(int exchangeRateID, int modifiedBy)
        {
            ChargeDAL.DeleteExchangeRate(exchangeRateID, modifiedBy);
        }

        #endregion
    }
}