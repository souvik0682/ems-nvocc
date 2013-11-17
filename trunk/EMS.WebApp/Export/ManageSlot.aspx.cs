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
    public partial class ManageSlot : System.Web.UI.Page
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
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                PopulateDropDown();
                RetrieveSearchCriteria();
                LoadSlot();
            }
        }

        protected void PopulateDropDown()
        {
            ListItem Li = new ListItem("ALL", " ");
            #region Line

            PopulateDropDown((int)Enums.DropDownPopulationFor.Line, ddlLine, 0);
            Li = new ListItem("SELECT LINE", "0");
            ddlLine.Items.Insert(0, Li);
            #endregion

        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter, 0);
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
                LoadSlot();
                //upLoc.Update();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSlotOperator.Text = string.Empty;
            ddlLine.SelectedIndex = 0;
            txtPOD.Text = string.Empty;
            txtPOL.Text = string.Empty;
            LoadSlot();
        }

        protected void gvwCharge_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwCharge.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadSlot();
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

                LoadSlot();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteSlot(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwCharge_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 7);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwCharge.PageSize * gvwCharge.PageIndex) + e.Row.RowIndex + 1).ToString();

                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SlotOperator"));

                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "POL"));

                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "POD"));

                string Line = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Line"));
                if (Line != "0")
                    e.Row.Cells[4].Text = ddlLine.Items.FindByValue(Line).Text;

                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Terms"));

                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EffectDate"));
                
                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                //btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00008");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SlotId"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                //btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SlotId"));

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
                    btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00009") + "');return false;";
                    btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00009") + "');return false;";
                }
            }

        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            LoadSlot();
            //upLoc.Update();
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

        private void LoadSlot()
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

                    gvwCharge.DataSource = SlotBLL.GetAllSlots(searchCriteria, 1);
                    gvwCharge.DataBind();   
                }
            }
        }

        private void DeleteSlot(int SlotId)
        {
            
            SlotBLL.DeleteSlot(SlotId);
            LoadSlot();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00006") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Export/AddEditSlot.aspx?id=" + encryptedId);
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
            criteria.SlotOperatorName = txtSlotOperator.Text.Trim();
            criteria.POD = txtPOD.Text.Trim();
            criteria.POL = txtPOL.Text.Trim();
            criteria.LineName = ddlLine.Text.Trim();
            criteria.Location = ddlLine.SelectedValue;
            //if (!String.IsNullOrEmpty(txtEfectDate.Text))
            //    criteria.Date = Convert.ToDateTime(txtEfectDate.Text);
            //else
            //    criteria.Date = null;

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
                    if (criteria.CurrentPage != PageName.SlotMaster)
                    {
                        criteria.Clear();
                        SetDefaultSearchCriteria(criteria);
                    }
                    else
                    {
                        txtSlotOperator.Text = criteria.SlotOperatorName;
                        ddlLine.SelectedValue = criteria.LineName;
                        txtPOD.Text = criteria.POD;
                        txtPOL.Text = criteria.POL;
                        //ddlChargeType.SelectedIndex = ddlChargeType.Items.IndexOf(ddlChargeType.Items.FindByValue(criteria.ChargeType.ToString()));

                        //if (criteria.Date.HasValue)
                        //    txtEfectDate.Text = criteria.Date.Value.ToString("dd/MM/yyyy");
                        //ddlLocation.SelectedValue = criteria.Location;


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
            criteria.CurrentPage = PageName.SlotMaster;
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