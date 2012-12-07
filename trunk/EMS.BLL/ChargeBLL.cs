using System;
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

        private void SetDefaultSearchCriteriaForHaulageCharge(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Location";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IImportHaulage> GetHaulageCharge(SearchCriteria searchCriteria)
        {
            return ChargeDAL.GetHaulageCharge(searchCriteria);
        }

        public IImportHaulage GetHaulageCharge(int haulageChgID)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForHaulageCharge(searchCriteria);
            return ChargeDAL.GetHaulageCharge(haulageChgID, searchCriteria);
        }

        public void SaveHaulageCharge(IImportHaulage haulageChgID, int modifiedBy)
        {
            ChargeDAL.SaveHaulageCharge(haulageChgID, modifiedBy);
        }

        public void DeleteHaulageCharge(int haulageChgID, int modifiedBy)
        {
            ChargeDAL.DeleteHaulageCharge(haulageChgID, modifiedBy);
        }

        #endregion

        #region Exchange Rate

        #endregion
    }
}
