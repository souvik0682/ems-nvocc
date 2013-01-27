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

namespace EMS.WebApp.MasterModule
{
    public partial class charge_list : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                PopulateDropDown();
                RetrieveSearchCriteria();
                LoadCharge();
            }
        }

        protected void PopulateDropDown()
        {
            ListItem Li = new ListItem("All", " ");
            foreach (Enums.ChargeType r in Enum.GetValues(typeof(Enums.ChargeType)))
            {
                ListItem item = new ListItem(Enum.GetName(typeof(Enums.ChargeType), r).Replace('_', ' '), ((int)r).ToString());
                ddlChargeType.Items.Add(item);
            }
            ddlChargeType.Items.Insert(0, Li);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //RedirecToAddEditPage(-1);
            Response.Redirect("~/MasterModule/charge-add-edit.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveNewPageIndex(0);
                LoadCharge();
                upLoc.Update();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            txtChargeName.Text = string.Empty;
            txtLine.Text = string.Empty;
            ddlChargeType.SelectedIndex = 0;
            LoadCharge();
        }


        protected void gvwCharge_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwCharge.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadCharge();
        }
        protected void gvwCharge_RowCommand(object sender, GridViewCommandEventArgs e)
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

                LoadCharge();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteLocation(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwCharge_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 7);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwCharge.PageSize * gvwCharge.PageIndex) + e.Row.RowIndex + 1).ToString();

                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChargeName"));

                e.Row.Cells[2].Text = ddlIEC.Items.FindByValue(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ImportExport"))).Text;

                if (!string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChargeType"))))
                    e.Row.Cells[3].Text = ((Enums.ChargeType)Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ChargeType"))).ToString().Replace("_", " ");

                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Line"));

                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DisplayOrder"));

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                //btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00008");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChargeId"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                //btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChargeId"));

                if (_hasEditAccess)
                {
                    btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";

                   
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
            LoadCharge();
            upLoc.Update();
        }

        #endregion

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
                    Response.Redirect("~/Unauthorized.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
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
            //gvwCharge.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            gvwCharge.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
        }

        private void LoadCharge()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);
                    CommonBLL commonBll = new CommonBLL();

                    gvwCharge.PageIndex = searchCriteria.PageIndex;
                    if (searchCriteria.PageSize > 0) gvwCharge.PageSize = searchCriteria.PageSize;

                    gvwCharge.DataSource = ChargeBLL.GetAllCharges(searchCriteria, 1);
                    gvwCharge.DataBind();
                }
            }
        }

        private void DeleteLocation(int ChargeId)
        {
            ChargeBLL.DeleteCharge(ChargeId);
            LoadCharge();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00006") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/MasterModule/charge-add-edit.aspx?id=" + encryptedId);
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
                sortExpression = "Name";
                sortDirection = "ASC";
            }

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.ChargeName = txtChargeName.Text.Trim();
            criteria.ChargeType = Convert.ToChar(ddlChargeType.SelectedValue);
            criteria.LineName = txtLine.Text.Trim();
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
                        txtChargeName.Text = criteria.ChargeName;
                        txtLine.Text = criteria.LineName;
                        ddlChargeType.SelectedIndex = ddlChargeType.Items.IndexOf(ddlChargeType.Items.FindByValue(criteria.ChargeType.ToString()));
                        gvwCharge.PageIndex = criteria.PageIndex;
                        gvwCharge.PageSize = criteria.PageSize;
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