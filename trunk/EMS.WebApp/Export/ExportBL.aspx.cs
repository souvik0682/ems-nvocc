using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;
using EMS.Entity;
using EMS.Common;
using EMS.BLL;
using System.Configuration;

namespace EMS.WebApp.Export
{
    public partial class ExportBL : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _roleId = 0;
        private IUser oUser = null;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private bool _LocationSpecific = true;
        private int _userLocation = 0;
        //private int _locId = 0;
        //private bool _hasEditAccess = true;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            oUser = (IUser)Session[Constants.SESSION_USER_INFO];
            _userId = UserBLL.GetLoggedInUserId();
            _userLocation = UserBLL.GetUserLocation();
            RetriveParameters();
            CheckUserAccess();
            //SetAttributes();

            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                LoadExportBL();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            LoadExportBL();
            upBL.Update();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtBookingNo.Text = "";
            txtEdgeBLNo.Text = "";
            txtRefBLNo.Text = "";
            txtPOL.Text = "";
            txtLine.Text = "";
            txtLocation.Text = "";

            SaveNewPageIndex(0);
            LoadExportBL();
            upBL.Update();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/ManageExportBL.aspx");
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            LoadExportBL();
            upBL.Update();
        }

        protected void gvExportBL_RowCommand(object sender, GridViewCommandEventArgs e)
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

                LoadExportBL();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToString(e.CommandArgument));
            }
            else if (e.CommandName == "Dashboard")
            {
                string encryptedId = GeneralFunctions.EncryptQueryString(Convert.ToString(e.CommandArgument));
                Response.Redirect("~/Export/Export-bl-query.aspx?BLNumber=" + encryptedId);
            }
            else if (e.CommandName == "Status")
            {
                bool IsActive = false;

                if (((System.Web.UI.WebControls.LinkButton)(e.CommandSource)).Text == "Active")
                    IsActive = false;
                else
                    IsActive = true;

                //Active/InActive
                ExportBLBLL.ChangeBLStatus(Convert.ToString(e.CommandArgument), IsActive);

                LoadExportBL();
            }
        }

        protected void gvExportBL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 6);
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Nvocc"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Location"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BookingNumber"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EdgeBLNumber"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RefBLNumber"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "POD"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "POL"));

                LinkButton lnkStatus = (LinkButton)e.Row.FindControl("lnkStatus");
                lnkStatus.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLNumber"));

                if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "BLStatus")))
                {
                    lnkStatus.ToolTip = "Active";
                    lnkStatus.Text = "Active";
                    lnkStatus.Attributes.Add("onclick", "javascript:return confirm('Are you sure about make it InActive?');");
                }
                else
                {
                    lnkStatus.ToolTip = "InActive";
                    lnkStatus.Text = "InActive";
                    lnkStatus.Attributes.Add("onclick", "javascript:return confirm('Are you sure about make it Active?');");
                }

                //Edit Link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00070");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLNumber"));

                //Dashboard Link
                ImageButton btnDashboard = (ImageButton)e.Row.FindControl("btnDashboard");
                btnDashboard.ToolTip = "Go to Dashboard";
                btnDashboard.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLNumber"));
            }
        }

        protected void gvExportBL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvExportBL.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadExportBL();
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
                else
                    _userLocation = 0;

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

                gvExportBL.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }
        }

        private void LoadExportBL()
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

                        gvExportBL.PageIndex = searchCriteria.PageIndex;

                        if (searchCriteria.PageSize > 0) gvExportBL.PageSize = searchCriteria.PageSize;

                        gvExportBL.DataSource = ExportBLBLL.GetExportBLForListing(searchCriteria);
                        gvExportBL.DataBind();
                    }
                }
            }
        }

        private void RedirecToAddEditPage(string blNumber)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(blNumber);
            Response.Redirect("~/Export/ManageExportBL.aspx?BLNumber=" + encryptedId);
        }

        private void BuildSearchCriteria(SearchCriteria criteria)
        {
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            //int roleId = UserBLL.GetLoggedInUserRoleId();

            if (!ReferenceEquals(ViewState[Constants.SORT_EXPRESSION], null) && !ReferenceEquals(ViewState[Constants.SORT_DIRECTION], null))
            {
                sortExpression = Convert.ToString(ViewState[Constants.SORT_EXPRESSION]);
                sortDirection = Convert.ToString(ViewState[Constants.SORT_DIRECTION]);
            }

            criteria.UserId = _userId;
            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.BookingNo = (txtBookingNo.Text == "") ? string.Empty : txtBookingNo.Text.Trim();
            criteria.EdgeBLNumber = (txtEdgeBLNo.Text == "") ? string.Empty : txtEdgeBLNo.Text.Trim();
            criteria.RefBLNumber = (txtRefBLNo.Text == "") ? string.Empty : txtRefBLNo.Text.Trim();
            criteria.POL = (txtPOL.Text == "") ? string.Empty : txtPOL.Text.Trim();
            criteria.LineName = (txtLine.Text == "") ? string.Empty : txtLine.Text.Trim();
            if (_userLocation != 0)
                criteria.Location = new BookingBLL().GetLocation(_userId);
            else
                criteria.Location = (txtLocation.Text == "") ? string.Empty : txtLocation.Text.Trim();

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
                    if (criteria.CurrentPage != PageName.ExportBL)
                    {
                        criteria.Clear();
                        SetDefaultSearchCriteria(criteria);
                    }
                    else
                    {
                        txtBookingNo.Text = criteria.BookingNo;
                        txtEdgeBLNo.Text = criteria.EdgeBLNumber;
                        txtRefBLNo.Text = criteria.RefBLNumber;
                        txtPOL.Text = criteria.POL;
                        txtLine.Text = criteria.LineName;
                        txtLocation.Text = criteria.Location;

                        gvExportBL.PageIndex = criteria.PageIndex;
                        gvExportBL.PageSize = criteria.PageSize;
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
            string sortExpression = "BookingNumber";
            string sortDirection = "ASC";

            criteria.CurrentPage = PageName.ExportBL;
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
    }
}