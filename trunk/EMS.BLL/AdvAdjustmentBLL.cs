using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using System.Data;


namespace EMS.BLL
{
    public class AdvAdjustmentBLL
    {
        public DataSet GetDetailFromJob(int? JobId = null, char? DorC = null)
        {
            return AdvAdjustmentDAL.GetDetailFromJob(JobId, DorC);
        }
        public DataSet GetInvoiceFromJob(int? JobId = null, char? DorC = null, char? NorAdj = null)
        {
            return AdvAdjustmentDAL.GetInvoiceFromJob(JobId, DorC, NorAdj);
        }
        public List<AdjustmentModel> GetAdjustmentModel(ISearchCriteria searchCriteria)
        {
            return AdvAdjustmentDAL.GetAdjustmentModel(searchCriteria);
        }
        public DataSet GetAdjustmentModelDataset(ISearchCriteria searchCriteria)
        {
            return AdvAdjustmentDAL.GetAdjustmentModelDataset(searchCriteria);
        }
        public int SaveAdjustmentModel(AdjustmentModel adjustmentModel, string Mode)
        { 
            return AdvAdjustmentDAL.SaveAdjustmentModel(adjustmentModel, Mode); }

        public int DeleteAdjustment(int AdjustmentModelID, int UserID, int CompanyID)
        { return AdvAdjustmentDAL.DeleteAdjustment(AdjustmentModelID, UserID, CompanyID); }
    }
}
