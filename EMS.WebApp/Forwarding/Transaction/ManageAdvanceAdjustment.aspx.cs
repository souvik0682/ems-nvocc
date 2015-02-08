using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EMS.BLL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;


namespace EMS.WebApp.Forwarding.Transaction
{
    public partial class ManageAdvanceAdjustment : System.Web.UI.Page
    {
        #region Private Member Variables

        IUser user = null;

        public ISearchCriteria SearchCriteriaProp
        {
            get { return (ISearchCriteria)Session["SearchCriteria"]; }
            set { Session["SearchCriteria"] = value; }
        }
        private bool _hasEditAccess = true;
        //private bool _canAdd = false;
        //private bool _canEdit = false;
        //private bool _canDelete = false;
        //private bool _canView = false;
        private bool _canAdd = true;
        private bool _canEdit = true;
        private bool _canDelete = true;
        private bool _canView = true;
        private int _userId = 0;

        #endregion
        public int counter = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            if (!IsPostBack)
            {

                // CheckUserAccess();
                SetDeafaultSetting();
                FillData();
            }
        }

        private void SetDeafaultSetting()
        {
            //var jobs = new AdvAdjustmentBLL().GetDetailFromJob();
            //ddlJobNo.DataSource = jobs;
            //ddlJobNo.DataTextField = "JobNo";
            //ddlJobNo.DataValueField = "pk_JobID";
            //ddlJobNo.DataBind();
            //ddlJobNo.Items.Insert(0, new ListItem("--JOB NUMBER.--", "0"));
            //string jobNo = ddlJobNo.SelectedValue == "0" ? "" : ddlJobNo.SelectedValue;
            string adjNo = string.IsNullOrEmpty(txtAdjNo.Text) ? "" : txtAdjNo.Text.Trim();
            string JobNo = string.IsNullOrEmpty(txtJobNo.Text) ? "" : txtJobNo.Text.Trim();
            string invoiceNo = string.IsNullOrEmpty(txtInvoiceNo.Text) ? "" : txtInvoiceNo.Text.Trim();
            string AdvOrAdj = ddlAdvOrAdj.SelectedValue;
            string PartyType = ddlParyType.SelectedValue;
            SearchCriteria searchCriteria = new SearchCriteria
            {
                PageIndex = 0,
                PageSize = 10,
                SortDirection = "",
                SortExpression = "",

                StringParams = new List<string>() { "-1", adjNo, JobNo, invoiceNo, AdvOrAdj, PartyType }

            };
            Session["SearchCriteria"] = searchCriteria;

        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

        }

        private void CheckUserAccess()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    if (_canView == false)
                    {
                        Response.Redirect("~/Unauthorized.aspx");
                    }

                    if (_canAdd == false)
                    {
                        btnAdd.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        private void FillData()
        {
            counter = 1;
            gvwHire.PageSize = SearchCriteriaProp.PageSize;
            gvwHire.DataSource = new BLL.AdvAdjustmentBLL().GetAdjustmentModelDataset(SearchCriteriaProp);
            gvwHire.DataBind();
            upLoc.Update();
        }

        protected void gvwHire_OnSorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ISearchCriteria searchCriteria = SearchCriteriaProp;
            //string jobNo = ddlJobNo.SelectedValue == "0" ? "" : ddlJobNo.SelectedValue;
            string adjNo = string.IsNullOrEmpty(txtAdjNo.Text) ? "" : txtAdjNo.Text.Trim();
            string jobNo = string.IsNullOrEmpty(txtJobNo.Text) ? "" : txtJobNo.Text.Trim();
            string invoiceNo = string.IsNullOrEmpty(txtInvoiceNo.Text) ? "" : txtInvoiceNo.Text.Trim();
            string AdvOrAdj = ddlAdvOrAdj.SelectedValue;
            string PartyType = ddlParyType.SelectedValue;
            searchCriteria.StringParams = new List<string>() { "-1", adjNo, jobNo, invoiceNo, AdvOrAdj, PartyType };
            SearchCriteriaProp = searchCriteria;
            FillData();
        }

        protected void gvwHire_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwHire.PageIndex = e.NewPageIndex;
            FillData();
        }

        protected void gvwHire_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                ISearchCriteria searchCriteria = SearchCriteriaProp;

                if (searchCriteria.SortExpression == e.CommandArgument.ToString())
                {
                    if (searchCriteria.SortDirection == "Asc")
                    {
                        searchCriteria.SortDirection = "Desc";
                    }
                    else { searchCriteria.SortDirection = "Asc"; }
                }
                else { searchCriteria.SortDirection = "Asc"; }
                searchCriteria.SortExpression = e.CommandArgument.ToString();
                SearchCriteriaProp = searchCriteria;
                FillData();
            }
            else if (e.CommandName == "Edit")
            {
                if (ddlAdvOrAdj.SelectedValue == "A")
                {
                    string adjNo = string.IsNullOrEmpty(txtAdjNo.Text) ? "" : txtAdjNo.Text.Trim();
                    string jobNo = string.IsNullOrEmpty(txtJobNo.Text) ? "" : txtJobNo.Text.Trim();
                    string invoiceNo = string.IsNullOrEmpty(txtInvoiceNo.Text) ? "" : txtInvoiceNo.Text.Trim();
                    string AdvOrAdj = ddlAdvOrAdj.SelectedValue;
                    string PartyType = ddlParyType.SelectedValue;
                    int AdvanceID = e.CommandArgument.ToInt();
                    
                    Response.Redirect("~/Forwarding/Transaction/Advance.aspx?&jobid=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32("0").ToString())
                        + "&AdvanceID=" + GeneralFunctions.EncryptQueryString(AdvanceID.ToString())
                        + "&paymenttype=" + GeneralFunctions.EncryptQueryString("C")
                    );
                }
                else
                {
                    int AdvanceAdjID = e.CommandArgument.ToInt();
                    Response.Redirect("~/Forwarding/Transaction/AddEditAdvanceAdjustment.aspx?&AdvAdjId=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32(AdvanceAdjID).ToString())
                    );
                }
            }
            else if (e.CommandName == "Remove")
            {
                try
                {
                    var tempId = GeneralFunctions.DecryptQueryString(e.CommandArgument.ToString());
                    var companyId = 1;
                    new BLL.AdvAdjustmentBLL().DeleteAdjustment(tempId.IntRequired(), _userId, companyId);
                    FillData();
                }
                catch
                {
                    throw;
                }
            }
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            ISearchCriteria searchCriteria = SearchCriteriaProp;
            searchCriteria.PageSize = newPageSize;
            searchCriteria.PageIndex = 0;
            SearchCriteriaProp = searchCriteria;
            gvwHire.PageSize = newPageSize;
            FillData();
        }

        protected void gvwHire_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (_canDelete == true)
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = true; //btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLID"));
                }
                else
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = false;
                }
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = "Edit";
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_AdvAdjInvID"));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Transaction/AddEditAdvanceAdjustment.aspx");
        }

        protected void ddlAdvOrAdj_SelectedIndexChanged(object sender, EventArgs e)
        {
            ISearchCriteria searchCriteria = SearchCriteriaProp;
            //string jobNo = ddlJobNo.SelectedValue == "0" ? "" : ddlJobNo.SelectedValue;
            string adjNo = string.IsNullOrEmpty(txtAdjNo.Text) ? "" : txtAdjNo.Text.Trim();
            string jobNo = string.IsNullOrEmpty(txtJobNo.Text) ? "" : txtJobNo.Text.Trim();
            string invoiceNo = string.IsNullOrEmpty(txtInvoiceNo.Text) ? "" : txtInvoiceNo.Text.Trim();
            string AdvOrAdj = ddlAdvOrAdj.SelectedValue;
            string PartyType = ddlParyType.SelectedValue;
            searchCriteria.StringParams = new List<string>() { "-1", adjNo, jobNo, invoiceNo, AdvOrAdj, PartyType };
            SearchCriteriaProp = searchCriteria;
            FillData();
        }

        protected void ddlParyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ISearchCriteria searchCriteria = SearchCriteriaProp;
            //string jobNo = ddlJobNo.SelectedValue == "0" ? "" : ddlJobNo.SelectedValue;
            string adjNo = string.IsNullOrEmpty(txtAdjNo.Text) ? "" : txtAdjNo.Text.Trim();
            string jobNo = string.IsNullOrEmpty(txtJobNo.Text) ? "" : txtJobNo.Text.Trim();
            string invoiceNo = string.IsNullOrEmpty(txtInvoiceNo.Text) ? "" : txtInvoiceNo.Text.Trim();
            string AdvOrAdj = ddlAdvOrAdj.SelectedValue;
            string PartyType = ddlParyType.SelectedValue;
            searchCriteria.StringParams = new List<string>() { "-1", adjNo, jobNo, invoiceNo, AdvOrAdj, PartyType };
            SearchCriteriaProp = searchCriteria;
            FillData();
        }
    }
}