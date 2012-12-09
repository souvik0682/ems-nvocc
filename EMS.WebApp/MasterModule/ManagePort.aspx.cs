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

namespace EMS.WebApp.MasterModule
{
    public partial class ManagePort : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {

            SetAttributes();

            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                LoadData();// portId=-1 means for all
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            LoadData();
            upLoc.Update();
        }

        protected void gvwLoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwLoc.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadData();
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

                LoadData();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeletePort(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwLoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 3);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwLoc.PageSize * gvwLoc.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_PortID"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PortCode"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PortName"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ICDIndicator"));


                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00008");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_PortId"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_PortId"));

                if (_hasEditAccess)
                {
                    btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00010") + "');";
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
            LoadData();
            upLoc.Update();
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            // txtPortCode.Attributes.Add("OnFocus", "javascript:js_waterMark_Focus('" + txtPortCode.ClientID + "', 'Type CountryId')");
            // txtPortCode.Attributes.Add("OnBlur", "javascript:js_waterMark_Blur('" + txtPortCode.ClientID + "', 'Type CountryId')");
            // //txtPortCode.Text = "Type CountryId";


            // txtPortName.Attributes.Add("OnFocus", "javascript:js_waterMark_Focus('" + txtPortName.ClientID + "', 'Type Country Name')");
            // txtPortName.Attributes.Add("OnBlur", "javascript:js_waterMark_Blur('" + txtPortName.ClientID + "', 'Type Country Name')");
            //// txtPortName.Text = "Type Country Name";


            txtWMEAbbr.WatermarkText = "TYPE PORT CODE";
            txtWMEName.WatermarkText = "TYPE PORT NAME";
            gvwLoc.PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            gvwLoc.PagerSettings.PageButtonCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageButtonCount"]);
        }

        private void LoadData()
        {
            string portCode = string.IsNullOrEmpty(txtPortCode.Text) ? "" : txtPortCode.Text.Trim();
            string portname = string.IsNullOrEmpty(txtPortName.Text) ? "" : txtPortName.Text.Trim();
            
            lblErrorMsg.Text = "";
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);


                    gvwLoc.PageIndex = searchCriteria.PageIndex;
                    if (searchCriteria.PageSize > 0) gvwLoc.PageSize = searchCriteria.PageSize;

                    BLL.DBInteraction dbinteract = new BLL.DBInteraction();
                   
                    try
                    {
                        gvwLoc.DataSource = dbinteract.GetPort(-1,portCode,portname);
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

        private void DeletePort(int portId)
        {
            DBInteraction dinteract = new DBInteraction();
            dinteract.DeletePort(portId);
            LoadData();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00006") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());

            Response.Redirect("~/MasterModule/AddEditPort.aspx?id=" + encryptedId);
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
            criteria.LocAbbr = (txtPortCode.Text == ResourceManager.GetStringWithoutName("ERR00017")) ? string.Empty : txtPortCode.Text.Trim();
            criteria.LocName = (txtPortName.Text == ResourceManager.GetStringWithoutName("ERR00018")) ? string.Empty : txtPortName.Text.Trim();
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
                        txtPortCode.Text = criteria.LocAbbr;
                        txtPortName.Text = criteria.LocName;
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
    }
}