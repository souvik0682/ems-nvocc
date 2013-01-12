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
    public partial class ManageVessel : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((AjaxControlToolkit.TextBoxWatermarkExtender)AutoCompleteCountry1.FindControl("txtWMEName")).WatermarkText="TYPE VESSEL FLAG";
            
            SetAttributes();

            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                DBInteraction dbInteract = new DBInteraction();
              
                //GeneralFunctions.PopulateDropDownList(ddlLocation, dbInteract.PopulateDDLDS("mstCountry", "pk_countryid", "CountryName"));
                //ddlLocation.Items[0].Text = "Select Country";
                GeneralFunctions.PopulateDropDownList(ddlVesselPrefix, dbInteract.PopulateDDLDS("mstVesselPrefix", "pk_VesselPrefixID", "VesselPrefix"));
                ddlVesselPrefix.Items[0].Text = "Select Prefix";
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
                DeleteVessel(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwLoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 3);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwLoc.PageSize * gvwLoc.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_VesselID"));
               // e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "location"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VesselPrefix"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VesselName"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "flag"));
               // e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Lastport")).ToUpper();
                

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00013");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_VesselID"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00012");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_VesselID"));

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

        private void SetAttributes()
        {
            // txtVesselPrefix.Attributes.Add("OnFocus", "javascript:js_waterMark_Focus('" + txtVesselPrefix.ClientID + "', 'Type CountryId')");
            // txtVesselPrefix.Attributes.Add("OnBlur", "javascript:js_waterMark_Blur('" + txtVesselPrefix.ClientID + "', 'Type CountryId')");
            // //txtVesselPrefix.Text = "Type CountryId";


            // txtVesselName.Attributes.Add("OnFocus", "javascript:js_waterMark_Focus('" + txtVesselName.ClientID + "', 'Type Country Name')");
            // txtVesselName.Attributes.Add("OnBlur", "javascript:js_waterMark_Blur('" + txtVesselName.ClientID + "', 'Type Country Name')");
            //// txtVesselName.Text = "Type Country Name";


            
            txtWMEName.WatermarkText = "TYPE VESSEL NAME";
            //txtWMEFlag.WatermarkText = "TYPE VESSEL FLAG";
            

            gvwLoc.PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            gvwLoc.PagerSettings.PageButtonCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageButtonCount"]);
        }

        private void LoadData(string SortExp, string direction)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            int VesselPrefix = Convert.ToInt32(ddlVesselPrefix.SelectedValue);
            string VesselName = string.IsNullOrEmpty(txtVesselName.Text) ? "" : txtVesselName.Text.Trim();
            int vesselFlag = dbinteract.GetId("Country", ((TextBox)AutoCompleteCountry1.FindControl("txtCountry")).Text);
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
                        System.Data.DataSet ds = dbinteract.GetVessel(-1, VesselPrefix, VesselName, vesselFlag);
                        System.Data.DataView dv = new System.Data.DataView(ds.Tables[0]);
                        if (!string.IsNullOrEmpty(SortExp) && !string.IsNullOrEmpty(direction) && SortExp!="Location")
                            dv.Sort = SortExp + " " + direction;
                        gvwLoc.DataSource = dv;
                        
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

        private void DeleteVessel(int VesselId)
        {
            DBInteraction dinteract = new DBInteraction();
            dinteract.DeleteVessel(VesselId);
            SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
            LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00010") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());

            Response.Redirect("~/MasterModule/AddEditVessel.aspx?id=" + encryptedId);
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
            criteria.LocName = (txtVesselName.Text == "TYPE VESSEL NAME") ? string.Empty : txtVesselName.Text.Trim();
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
                       // txtVesselFlag.Text = criteria.LocAbbr;
                        txtVesselName.Text = criteria.LocName;
                        gvwLoc.PageIndex = criteria.PageIndex;
                        gvwLoc.PageSize = criteria.PageSize;
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
            Response.Redirect("~/MasterModule/ManageVessel.aspx");
        }
    }
}