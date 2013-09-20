﻿using System;
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
    public partial class ManageLBCntr : System.Web.UI.Page
    {

        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private IUser oUser = null;
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

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);

            CheckUserAccess();
            //CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                LoadContainer(0, _userLocation);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //RedirecToAddEditPage(-1);
            Response.Redirect("AddEditLBCntr.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveNewPageIndex(0);
                LoadContainer(0, _userLocation);
                upLoc.Update();
            }
        }

        protected void gvwContainerTran_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwContainerTran.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadContainer(0, _userLocation);
        }
        protected void gvwContainerTran_RowCommand(object sender, GridViewCommandEventArgs e)
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

                LoadContainer(0, _userLocation);
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteTransaction(Convert.ToInt32(e.CommandArgument));
            }
        }



        protected void gvwContainerTran_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 7);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwContainerTran.PageSize * gvwContainerTran.PageIndex) + e.Row.RowIndex + 1).ToString();

                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ContainerNo"));

                //e.Row.Cells[2].Text = ddlIEC.Items.FindByValue(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Status"))).Text;
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BookingNo"));

                e.Row.Cells[3].Text = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "LBDate")).ToShortDateString();

                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Location"));

                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Line"));

                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RefBookingNo"));

                //string Ld = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LandingDate"));
                //e.Row.Cells[6].Text = string.IsNullOrEmpty(Ld) ? "" : Convert.ToDateTime(Ld).ToShortDateString();

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                //btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00008");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PK_LBID"));

                if (oUser.UserRole.Id == 6 || oUser.UserRole.Id == 2)
                    btnEdit.Visible = true;
                else
                    btnEdit.Visible = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Editable"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                //btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PK_LBID"));
                btnRemove.Visible = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Editable"));

                if (_canDelete == true)
                {
                    //ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = true;
                    btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                    //btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLID"));

                }
                else
                {
                    //ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = false;
                }

                if (_hasEditAccess)
                {
                    btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00012") + "');";
                }
                else
                {
                    btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00009") + "');return false;";
                    btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00009") + "');return false;";
                }
            }
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            LoadContainer(0, _userLocation);
            upLoc.Update();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            txtContainerNo.Text = string.Empty;
            txtBookingNo.Text = string.Empty;
            txtPortName.Text = string.Empty;
            txtBLNo.Text = string.Empty;
            txtRefBookingNo.Text = string.Empty;
            LoadContainer(0, _userLocation);
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

        //        if (user.UserRole.Id != (int)UserRole.Admin && user.UserRole.Id != (int)UserRole.Manager)
        //        {
        //            Response.Redirect("~/Unauthorized.aspx");
        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }
        //}
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
                {
                    _userLocation = 0;
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

        private void SetAttributes()
        {
            //txtAbbreviation.Attributes.Add("OnFocus", "javascript:js_waterMark_Focus('" + txtAbbreviation.ClientID + "', 'Type Abbreviation')");
            //txtAbbreviation.Attributes.Add("OnBlur", "javascript:js_waterMark_Blur('" + txtAbbreviation.ClientID + "', 'Type Abbreviation')");
            //txtAbbreviation.Text = "Type Abbreviation";


            //txtLocationName.Attributes.Add("OnFocus", "javascript:js_waterMark_Focus('" + txtLocationName.ClientID + "', 'Type Location Name')");
            //txtLocationName.Attributes.Add("OnBlur", "javascript:js_waterMark_Blur('" + txtLocationName.ClientID + "', 'Type Location Name')");
            //txtLocationName.Text = "Type Location Name";


            //txtWMEAbbr.WatermarkText = ResourceManager.GetStringWithoutName("ERR00017");
            //txtWMEName.WatermarkText = ResourceManager.GetStringWithoutName("ERR00018");
            //gvwContainerTran.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            gvwContainerTran.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
        }

        private void LoadContainer(int MovementId, int Locationid)
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);
                    //CommonBLL commonBll = new CommonBLL();
                    LBCntrBLL oContainerTranBLL = new LBCntrBLL();

                    gvwContainerTran.PageIndex = searchCriteria.PageIndex;
                    if (searchCriteria.PageSize > 0) gvwContainerTran.PageSize = searchCriteria.PageSize;

                    gvwContainerTran.DataSource = oContainerTranBLL.GetLBCntrList(searchCriteria, 0, _userLocation).Tables[0];
                    gvwContainerTran.DataBind();
                }
            }
        }

        private void DeleteTransaction(int TranId)
        {
            LBCntrBLL oBll = new LBCntrBLL();
            oBll.DeleteTransaction(TranId);
            LoadContainer(0, _userLocation);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00006") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("AddEditLBCntr.aspx?id=" + encryptedId);
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
                //sortExpression = "ContainerNo";
                sortExpression = "Date";
                sortDirection = "DESC";
            }

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.StringOption1 = txtContainerNo.Text.Trim();
            criteria.StringOption2 = txtBookingNo.Text.Trim();
            criteria.StringOption3 = txtPortName.Text.Trim();
            criteria.StringOption4 = txtRefBookingNo.Text.Trim();
            criteria.StringOption5 = txtBLNo.Text.Trim();
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
                    if (criteria.CurrentPage != PageName.LocationMaster)
                    {
                        criteria.Clear();
                        SetDefaultSearchCriteria(criteria);
                    }
                    else
                    {
                        txtContainerNo.Text = criteria.StringOption1;
                        txtBookingNo.Text = criteria.StringOption2;
                        txtPortName.Text = criteria.StringOption3;
                        txtRefBookingNo.Text = criteria.StringOption4;
                        txtBLNo.Text = criteria.StringOption5;
                        gvwContainerTran.PageIndex = criteria.PageIndex;
                        gvwContainerTran.PageSize = criteria.PageSize;
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
            criteria.CurrentPage = PageName.LocationMaster;
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
   
}