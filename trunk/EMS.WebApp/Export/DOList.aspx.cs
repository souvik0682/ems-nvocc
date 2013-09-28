using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Entity;
using EMS.Utilities;

namespace EMS.WebApp.Export
{
    public partial class DOList : System.Web.UI.Page
    {
        #region Private Member Variables

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add(new System.Data.DataColumn("ID"));
                System.Data.DataRow row = dt.NewRow();
                dt.Rows.Add(row);

                System.Data.DataRow row1 = dt.NewRow();
                dt.Rows.Add(row1);

                gvwList.DataSource = dt;
                gvwList.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvwList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvwList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvwList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void CheckUserAccess()
        {

        }

        private void RetriveParameters()
        {

        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                gvwList.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
                gvwList.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Export/DOEdit.aspx?id=" + encryptedId);
        }

        private void LoadDeliveryOrder()
        {

        }

        private void DeleteDeliveryOrder()
        {

        }

        private void GenerateReport()
        {

        }

        private void BuildSearchCriteria(SearchCriteria criteria)
        {

        }

        private void RetrieveSearchCriteria()
        {

        }

        private void SetDefaultSearchCriteria(SearchCriteria criteria)
        {

        }

        private void SaveNewPageIndex(int newIndex)
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(criteria, null))
                {
                    criteria.PageIndex = newIndex;
                }
            }
        }

        private void SaveNewPageSize(int newPageSize)
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(criteria, null))
                {
                    criteria.PageSize = newPageSize;
                }
            }
        }

        #endregion
    }
}