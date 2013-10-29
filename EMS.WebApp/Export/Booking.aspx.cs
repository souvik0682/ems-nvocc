using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.BLL;
using System.Configuration;
using EMS.Utilities.ResourceManager;
using EMS.Entity;
using EMS.Common;

namespace EMS.WebApp.Export
{
    public partial class Booking : System.Web.UI.Page
    {
        #region Private Member Variables

        //private int _userId = 0;
        //private int _roleId = 0;
        //private bool _canAdd = false;
        //private bool _canEdit = false;
        //private bool _canDelete = false;
        //private bool _canView = false;
        //private bool _LocationSpecific = true;

        private int _userId = 0;
        private IUser oUser = null;
        private bool _hasEditAccess = true;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userLocation = 0;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            oUser = (IUser)Session[Constants.SESSION_USER_INFO];
            _userId = UserBLL.GetLoggedInUserId();
            _userLocation = UserBLL.GetUserLocation();

            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                LoadBooking();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //RedirecToAddEditPage(-1);
            Response.Redirect("~/Export/ManageBooking.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveNewPageIndex(0);
                LoadBooking();
                upBooking.Update();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtBookingNo.Text = string.Empty;
            txtLine.Text = string.Empty;
            txtBookingRef.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            txtVessel.Text = string.Empty;
            txtVoyage.Text = string.Empty;
            txtLocation.Text = string.Empty;
            LoadBooking();
        }

        protected void gvBooking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvBooking.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadBooking();
        }
        protected void gvBooking_RowCommand(object sender, GridViewCommandEventArgs e)
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

                LoadBooking();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Status")
            {
                DeleteBooking(Convert.ToInt32(e.CommandArgument));
            }

            else if (e.CommandName == "Charge")
            {
                RedirecToChargePage(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvBooking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnCharge = (ImageButton)e.Row.FindControl("btnCharges");

                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ChgExist")) == 0)
                    btnCharge.ImageUrl = "~/Images/BookingWithcharge.ico";

                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DOExist")) == 1 || (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "BLExist")) == 1))
                    _canDelete = false;
                else
                    _canDelete = true;

                //if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "BLExist")) == 1)
                //    _canDelete = false;
                //else
                //    _canDelete = true;
                //else
                //    btnCharge.ImageUrl = "~/Images/Status.jpg";
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 7);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                //e.Row.Cells[0].Text = ((gvBooking.PageSize * gvBooking.PageIndex) + e.Row.RowIndex + 1).ToString();
                //e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Abbreviation"));

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LocationName"));// +"<br/><font style='font-size:x-small;font-weight:bold;'>CODE : (" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LFCode")) + ")</font>";

                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "NVOCC"));// +"<br/><font style='font-size:x-small;font-weight:bold;'>CODE : (" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LTCode")) + ")</font>";

                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BookingNo"));

                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BookingDate")).Split(' ')[0];
                //e.Row.Cells[4].Text = Convert.ToString(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "WeightFrom")));

                //e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "WeightTo"));

                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VesselName"));

                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VoyageNo"));

                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "POL"));

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00013");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BookingID"));

                //Status link
                ImageButton btnStatus = (ImageButton)e.Row.FindControl("btnStatus");
                btnStatus.ToolTip = ResourceManager.GetStringWithoutName("ERR00084");
                btnStatus.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BookingID"));


                //Status link
                ImageButton btnCharges = (ImageButton)e.Row.FindControl("btnCharges");
                btnCharges.ToolTip = ResourceManager.GetStringWithoutName("ERR00082");
                btnCharges.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BookingID"));

                if (_canEdit == true)
                {
                    //ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnStatus.Visible = true;
                    btnStatus.ToolTip = ResourceManager.GetStringWithoutName("ERR00084");
                    btnCharges.ToolTip = ResourceManager.GetStringWithoutName("ERR00082");
                    //btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLID"));

                }
                else
                {
                    //ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnStatus.Visible = false;
                    btnCharges.Visible = false;
                }

                if (_canDelete)
                {
                    btnStatus.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";
                }
                else
                {
                    btnStatus.Visible = false;
                    
                    //btnStatus.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                }
                //btnStatus.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                //btnCharges.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                
            }
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            LoadBooking();
            upBooking.Update();
        }

        #endregion

        #region Private Methods

        //private void CheckUserAccess()
        //{
        //    if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
        //    {
        //        IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

        //        if (ReferenceEquals(user, null) || user.Id == 0)
        //        {
        //            Response.Redirect("~/Login.aspx");
        //        }

        //        if (user.UserRole.Id != (int)UserRole.Admin)
        //        {
        //            Response.Redirect("~/Unauthorized.aspx");
        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }
        //}

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
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
                    //Response.Redirect("~/Unauthorized.aspx");
                }
                else
                    _userLocation = 0;
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

        private void SetAttributes()
        {
            gvBooking.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
        }

        private void LoadBooking()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);
                    CommonBLL commonBll = new CommonBLL();

                    gvBooking.PageIndex = searchCriteria.PageIndex;
                    if (searchCriteria.PageSize > 0) gvBooking.PageSize = searchCriteria.PageSize;

                    gvBooking.DataSource = BookingBLL.GetBooking(searchCriteria, 0, "C");
                    gvBooking.DataBind();
                }
            }
        }

        private void DeleteBooking(int locId)
        {
            BookingBLL.DeleteBooking(locId);
            LoadBooking();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00010") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Export/ManageBooking.aspx?id=" + encryptedId);
        }

        private void RedirecToChargePage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Export/BookingCharges.aspx?BookingId=" + encryptedId);
        }

        private void BuildSearchCriteria(SearchCriteria criteria)
        {
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            if (!ReferenceEquals(ViewState[Constants.SORT_EXPRESSION], null) && !ReferenceEquals(ViewState[Constants.SORT_DIRECTION], null))
            {
                sortExpression = Convert.ToString(ViewState[Constants.SORT_EXPRESSION]);
                sortDirection = Convert.ToString(ViewState[Constants.SORT_DIRECTION]);
            }
            else
            {
                sortExpression = "BookingNo";
                sortDirection = "ASC";
            }

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.BookingNo = txtBookingNo.Text.Trim();
            criteria.StringOption1 = txtBookingRef.Text.Trim();
            criteria.StringOption2 = txtCustomer.Text.Trim();
            criteria.LineName = txtLine.Text.Trim();

            if (_userLocation != 0)
                criteria.Location = new BookingBLL().GetLocation(_userId);
            else
                criteria.Location = txtLocation.Text.Trim();
            criteria.Vessel = txtVessel.Text.Trim();
            criteria.Voyage = txtVoyage.Text.Trim();

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
                    if (criteria.CurrentPage != PageName.Booking)
                    {
                        criteria.Clear();
                        SetDefaultSearchCriteria(criteria);
                    }
                    else
                    {
                        txtBookingNo.Text = criteria.BookingNo;
                        txtLine.Text = criteria.LineName;
                        txtBookingRef.Text = criteria.StringOption1;
                        txtCustomer.Text = criteria.StringOption2;
                        txtLocation.Text = criteria.Location;
                        txtVoyage.Text = criteria.Voyage;
                        txtVessel.Text = criteria.Vessel;
                        gvBooking.PageIndex = criteria.PageIndex;
                        gvBooking.PageSize = criteria.PageSize;
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
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.CurrentPage = PageName.Booking;
            criteria.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
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

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //}



        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Export/ManageBooking.aspx");
        //}

        //protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}

        //protected void gvBooking_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //}

        //protected void gvBooking_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //}

        //protected void gvBooking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //}
    
}