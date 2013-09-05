﻿using System;
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

namespace EMS.WebApp.MasterModule
{
    public partial class ManageFreeDays : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            //((AjaxControlToolkit.TextBoxWatermarkExtender)AutoCompleteCountry1.FindControl("txtWMEName")).WatermarkText = "TYPE VESSEL FLAG";

            SetAttributes();
            RetriveParameters();
            CheckUserAccess();
            //gvwLoc.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;

            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                DBInteraction dbInteract = new DBInteraction();

                //GeneralFunctions.PopulateDropDownList(ddlLocation, dbInteract.PopulateDDLDS("mstCountry", "pk_countryid", "CountryName"));
                //ddlLocation.Items[0].Text = "Select Country";
                //GeneralFunctions.PopulateDropDownList(ddlVesselPrefix, dbInteract.PopulateDDLDS("mstVesselPrefix", "pk_VesselPrefixID", "VesselPrefix"));
                //ddlVesselPrefix.Items[0].Text = "Select Prefix";
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
                LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
            LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);
            upLoc.Update();
        }

        protected void gvwLoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwLoc.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
            LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);
        }
        protected void gvwLoc_RowCommand(object sender, GridViewCommandEventArgs e)
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

                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
                LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteFreeDays(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwLoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 3);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwLoc.PageSize * gvwLoc.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_LinkID"));
                // e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "location"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Line"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LocName"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DefaultFreeDays"));
                // e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Lastport")).ToUpper();


                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00013");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_LinkID"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00012");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_LinkID"));

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
                    btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";
                }
                else
                {
                    btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                    btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                }
            }
        }


        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
            LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);
            upLoc.Update();
        }

        #endregion

        #region Private Methods

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
            // txtVesselPrefix.Attributes.Add("OnFocus", "javascript:js_waterMark_Focus('" + txtVesselPrefix.ClientID + "', 'Type CountryId')");
            // txtVesselPrefix.Attributes.Add("OnBlur", "javascript:js_waterMark_Blur('" + txtVesselPrefix.ClientID + "', 'Type CountryId')");
            // //txtVesselPrefix.Text = "Type CountryId";


            // txtVesselName.Attributes.Add("OnFocus", "javascript:js_waterMark_Focus('" + txtVesselName.ClientID + "', 'Type Country Name')");
            // txtVesselName.Attributes.Add("OnBlur", "javascript:js_waterMark_Blur('" + txtVesselName.ClientID + "', 'Type Country Name')");
            //// txtVesselName.Text = "Type Country Name";



            txtWMELineName.WatermarkText = "TYPE LINE NAME";

            TxtWMELocName.WatermarkText = "TYPE LOCATION NAME";
            //txtWMEFlag.WatermarkText = "TYPE VESSEL FLAG";


            gvwLoc.PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            gvwLoc.PagerSettings.PageButtonCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageButtonCount"]);
        }

        private void LoadData(string SortExp, string direction)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            //int VesselPrefix = Convert.ToInt32(ddlVesselPrefix.SelectedValue);
            string Location = string.IsNullOrEmpty(TxtLocation.Text) ? "" : TxtLocation.Text.Trim();
            string LineName = string.IsNullOrEmpty(txtLine.Text) ? "" : txtLine.Text.Trim();
            //int vesselFlag = 
            // int countryId=Convert.ToInt32(ddlLocation.SelectedValue);

            lblErrorMsg.Text = "";
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);

                    gvwLoc.PageIndex = searchCriteria.PageIndex;
                    if (searchCriteria.PageSize > 0) gvwLoc.PageSize = searchCriteria.PageSize;

                    try
                    {
                        //System.Data.DataSet ds = dbinteract.GetFreeDays(-1, Location, LineName, searchCriteria);
                        //System.Data.DataView dv = new System.Data.DataView(ds.Tables[0]);
                        //if (!string.IsNullOrEmpty(SortExp) && !string.IsNullOrEmpty(direction) && SortExp != "Location")
                        //    dv.Sort = SortExp + " " + direction;
                        //gvwLoc.DataSource = dv;

                        gvwLoc.DataSource = dbinteract.GetFreeDays(-1, Location, LineName, searchCriteria);
                        gvwLoc.DataBind();

                    }
                    catch (Exception ex)
                    {

                        gvwLoc.DataSource = null;
                        lblErrorMsg.Text = "Error Occured.Please try again.";
                    }

                    gvwLoc.DataBind();
                }
            }
        }

        private void DeleteFreeDays(int LinkId)
        {
            DBInteraction dinteract = new DBInteraction();
            dinteract.DeleteFreeDays(LinkId);
            SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
            LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00010") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());

            Response.Redirect("~/MasterModule/AddEditFreeDays.aspx?id=" + encryptedId);
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
                sortExpression = "Location";
                sortDirection = "ASC";
            }

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            //criteria.LocAbbr = (txtVesselFlag.Text == "TYPE VESSEL FLAG") ? string.Empty : txtVesselFlag.Text.Trim();
            criteria.LocName = (TxtLocation.Text == "TYPE LOCATION NAME") ? string.Empty : TxtLocation.Text.Trim();
            criteria.LineName = (txtLine.Text == "TYPE LINE NAME") ? string.Empty : txtLine.Text.Trim();

            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
        }

        private void RetrieveSearchCriteria()
        {
            bool isCriteriaExists = false;

            //if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            //{
            //    SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

            //    if (!ReferenceEquals(criteria, null))
            //    {
            //        if (criteria.CurrentPage != PageName.LocationMaster)
            //        {
            //            criteria.Clear();
            //            SetDefaultSearchCriteria(criteria);
            //        }
            //        else
            //        {
            //           // txtVesselFlag.Text = criteria.LocAbbr;
            //            txtVesselName.Text = criteria.LocName;
            //            gvwLoc.PageIndex = criteria.PageIndex;
            //            gvwLoc.PageSize = criteria.PageSize;
            //            ddlPaging.SelectedValue = criteria.PageSize.ToString();
            //            isCriteriaExists = true;
            //        }
            //    }
            //}

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
            criteria.PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
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


        protected void gvwLoc_Sorting(object sender, GridViewSortEventArgs e)
        {
            SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
            if (criteria.SortDirection == "ASC")
            {
                criteria.SortDirection = "DESC";
                LoadData(criteria.SortExpression, criteria.SortDirection);
            }
            else
            {
                criteria.SortDirection = "ASC";
                LoadData(criteria.SortExpression, criteria.SortDirection);
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {

            //txtVesselName.Text = "";
            //ddlVesselPrefix.SelectedIndex = 0;
            //btnSearch_Click(sender, e);
            Response.Redirect("~/MasterModule/ManageFreeDays.aspx");
        }
    }
}