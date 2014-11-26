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

        public DataSet GetParty()
        {
            return EstimateDAL.GetParty();
        }
    }
}
