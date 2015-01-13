using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;

namespace EMS.BLL
{
    public  class EstimateBLL
    {
        public  DataSet GetUnitMaster(ISearchCriteria searchCriteria)
        {
            return EstimateDAL.GetUnitMaster(searchCriteria);
        }

        public DataSet GetSingleUnitType(int UnitID, int JobID)
        {
            return EstimateDAL.GetSingleUnitType(UnitID, JobID);
        }

        public  Estimate GetEstimate(ISearchCriteria searchCriteria)
        { return EstimateDAL.GetEstimate(searchCriteria); }
        public  DataSet GetBillingGroupMaster(ISearchCriteria searchCriteria)
        {
            return EstimateDAL.GetBillingGroupMaster(searchCriteria);
        }

        public  DataSet GetCharges(ISearchCriteria searchCriteria)
        {
            return EstimateDAL.GetCharges(searchCriteria);
        }

        public DataSet GetSelectedCharge(int ChargeID)
        {
            return EstimateDAL.GetSelectedCharge(ChargeID);
        }

        public DataSet GetServiceTax(DateTime EstimateDate, decimal BasicValue, int ChargeID)
        {
            return EstimateDAL.GetServiceTax(EstimateDate, BasicValue, ChargeID);
        }

        public DataSet GetContainers(int UnitTypeID, string Size, int JobID)
        {
            return EstimateDAL.GetContainers(UnitTypeID, Size, JobID);
        }

        public  DataSet GetCurrency(ISearchCriteria searchCriteria)
        {
            return EstimateDAL.GetCurrency(searchCriteria);
        }

        public  DataSet GetExchange(ISearchCriteria searchCriteria)
        {
            return EstimateDAL.GetExchange(searchCriteria);
        }

        public  int SaveEstimate(Estimate estimate, string Mode)
        {
            return EstimateDAL.SaveEstimate(estimate, Mode);
        }

        public DataSet GetParty(int PartyType)
        {
            return EstimateDAL.GetParty(PartyType);
        }

        public DataSet GetAllParty(int PartyType)
        {
            return EstimateDAL.GetAllParty(PartyType);
        }
    }
}
