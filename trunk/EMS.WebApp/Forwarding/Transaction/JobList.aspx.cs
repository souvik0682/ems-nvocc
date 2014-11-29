using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;
using EMS.Common;
using EMS.BLL;
using System.Configuration;
using EMS.Entity;

namespace EMS.WebApp.Forwarding.Transaction
{
    public partial class JobList : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _roleId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private bool _LocationSpecific = true;
        //private int _locId = 0;
        //private bool _hasEditAccess = true;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                LoadImportBL();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            LoadImportBL();
            upBL.Update();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtCustomer.Text = "";
            txtJobNo.Text = "";
            ddlJobStatus.SelectedValue = "P";
            //txtJobType.Text = "";
            txtOpControl.Text = "";
            txtLine.Text = "";

            SaveNewPageIndex(0);
            LoadImportBL();
            upBL.Update();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Transaction/Job.aspx");
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            LoadImportBL();
            upBL.Update();
        }

        protected void gvImportBL_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                if (ViewState[Constants.SORT_EXPRESSION] == null)
                {
                    ViewState[Constants.SORT_EXPRESSION] = e.CommandArgument.ToString();
                    ViewState[Constants.SORT_DIRECTION] = "ASC";
                }
                else
                {
                    if (ViewState[Constants.SORT_EXPRESSION].ToString() == e.CommandArgument.ToString())
                    {
                        if (ViewState[Constants.SORT_DIRECTION].ToString() == "ASC")
                            ViewState[Constants.SORT_DIRECTION] = "DESC";
                        else
                            ViewState[Constants.SORT_DIRECTION] = "ASC";
                    }
                    else
                    {
                        ViewState[Constants.SORT_DIRECTION] = "ASC";
                        ViewState[Constants.SORT_EXPRESSION] = e.CommandArgument.ToString();
                    }
                }

                LoadImportBL();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteImportBL(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Dashboard")
            {
                string encryptedId = GeneralFunctions.EncryptQueryString(Convert.ToInt32(e.CommandArgument).ToString());
                Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + encryptedId);
            }
            else if (e.CommandName == "HblEntry")
            {
                string encryptedId = GeneralFunctions.EncryptQueryString(Convert.ToInt32(e.CommandArgument).ToString());
                Response.Redirect("~/Forwarding/Transaction/ManageJobBL.aspx?JobId=" + encryptedId);
            }
            
        }

        protected void gvImportBL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 6);
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "JobNo"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "JobDate")).Split(' ')[0];
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "JobType"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PlaceOfReceipt"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PlaceOfDelivery"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EstPayable"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EstReceivable"));
                e.Row.Cells[7].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EstProfit"));

                ImageButton btnDashboard = (ImageButton)e.Row.FindControl("btnDashboard");
                btnDashboard.ToolTip = "Dashboard";
                btnDashboard.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "JobId"));

                ImageButton btnHBLEntry = (ImageButton)e.Row.FindControl("btnHBLEntry");
                btnHBLEntry.ToolTip = "HBL Entry";
                btnHBLEntry.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "JobId"));

                //Edit Link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00070");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "JobId"));

                //Delete link
                if (_canDelete == true)
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = true;
                    btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                    btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "JobId"));

                }
                else
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = false;
                }
            }
        }

        protected void gvImportBL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvImportBL.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadImportBL();
        }


        #region Private Methods

        private void CheckUserAccess()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (user.UserRole.Id != (int)UserRole.Admin && user.UserRole.Id != (int)UserRole.Manager)
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

                //if (user.UserRole.Id != (int)UserRole.Admin && user.UserRole.Id != (int)UserRole.Manager && user.UserRole.Id != (int)UserRole.SalesExecutive)
                //{
                //    Response.Redirect("~/Unauthorized.aspx");
                //}
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();
            _LocationSpecific = UserBLL.GetUserLocationSpecific();

            IUser user = new UserBLL().GetUser(_userId);

            if (!ReferenceEquals(user, null))
            {
                if (!ReferenceEquals(user.UserRole, null))
                {
                    _roleId = user.UserRole.Id;
                    UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
                }

                //if (!ReferenceEquals(user.UserLocation, null))
                //{
                //    _locId = user.UserLocation.Id;
                //}
            }
        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                //txtWMELoc.WatermarkText = ResourceManager.GetStringWithoutName("ERR00031");
                //txtWMECust.WatermarkText = ResourceManager.GetStringWithoutName("ERR00035");
                //txtWMEGr.WatermarkText = ResourceManager.GetStringWithoutName("ERR00034");
                //txtWMEExec.WatermarkText = ResourceManager.GetStringWithoutName("ERR00069");
                ////gvwCust.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);

                gvImportBL.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }
        }

        private void LoadImportBL()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
                {
                    SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                    if (!ReferenceEquals(searchCriteria, null))
                    {
                        BuildSearchCriteria(searchCriteria);

                        gvImportBL.PageIndex = searchCriteria.PageIndex;

                        if (searchCriteria.PageSize > 0) 
                            gvImportBL.PageSize = searchCriteria.PageSize;

                        gvImportBL.DataSource = JobBLL.GetJobs(searchCriteria, 0, searchCriteria.JobType);
                        gvImportBL.DataBind();
                    }
                }
            }
        }

        private void DeleteImportBL(int JobId)
        {
            int jobId = JobBLL.DeleteJob(JobId, _userId);
            LoadImportBL();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Job has been deleted successfully!');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Forwarding/Transaction/Job.aspx?JobId=" + encryptedId);
        }

        private void BuildSearchCriteria(SearchCriteria criteria)
        {
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            int roleId = UserBLL.GetLoggedInUserRoleId();

            if (!ReferenceEquals(ViewState[Constants.SORT_EXPRESSION], null) && !ReferenceEquals(ViewState[Constants.SORT_DIRECTION], null))
            {
                sortExpression = Convert.ToString(ViewState[Constants.SORT_EXPRESSION]);
                sortDirection = Convert.ToString(ViewState[Constants.SORT_DIRECTION]);
            }
            else
            {
                sortExpression = "JOBNO";
                sortDirection = "DESC";
            }

            criteria.UserId = _userId;
            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.Customer = (txtCustomer.Text == "") ? string.Empty : txtCustomer.Text.Trim();
            criteria.JobNo = (txtJobNo.Text == "") ? string.Empty : txtJobNo.Text.Trim();
            criteria.JobType = ddlJobStatus.SelectedValue.ToString();
            //criteria.JobType = (txtJobType.Text == "") ? string.Empty : txtJobType.Text.Trim();
            criteria.OperationalControl = (txtOpControl.Text == "") ? string.Empty : txtOpControl.Text.Trim();
            criteria.LineName = (txtLine.Text == "") ? string.Empty : txtLine.Text.Trim();

            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
        }

        private void RetrieveSearchCriteria()
        {
            bool isCriteriaExists = false;

            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(criteria, null))
                {
                    if (criteria.CurrentPage != PageName.ImportBL)
                    {
                        criteria.Clear();
                        SetDefaultSearchCriteria(criteria);
                    }
                    else
                    {
                        //txtContainerNo.Text = criteria.ContainerNo;
                        txtCustomer.Text = criteria.Customer;
                        txtJobNo.Text = criteria.JobNo;
                        ddlJobStatus.SelectedValue = criteria.JobType;
                        //txtJobType.Text = criteria.JobType;
                        txtOpControl.Text = criteria.OperationalControl;
                        txtLine.Text = criteria.LineName;

                        gvImportBL.PageIndex = criteria.PageIndex;
                        gvImportBL.PageSize = criteria.PageSize;
                        ddlPaging.SelectedValue = criteria.PageSize.ToString();
                        isCriteriaExists = true;
                    }
                }
            }

            if (!isCriteriaExists)
            {
                SearchCriteria newcriteria = new SearchCriteria();
                SetDefaultSearchCriteria(newcriteria);
            }
        }

        private void SetDefaultSearchCriteria(SearchCriteria criteria)
        {
            string sortExpression = "JobNo";
            string sortDirection = "ASC";

            criteria.CurrentPage = PageName.ImportBL;
            criteria.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;

            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
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

        protected void ddlJobStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            LoadImportBL();
            upBL.Update();
        }
    }
}