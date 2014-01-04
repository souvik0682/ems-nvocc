using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Utilities;
using System.Configuration;
using EMS.Entity;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.Export
{
    public partial class Voyage : System.Web.UI.Page
    {
        private int _userId = 0;
        private int _roleId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();
            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                LoadVoyage();
              
            }
        }
        private void RetrieveSearchCriteria()
        {
            bool isCriteriaExists = false;

            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(criteria, null))
                {
                    if (criteria.CurrentPage != PageName.Invoice)
                    {
                        criteria.Clear();
                        SetDefaultSearchCriteria(criteria);
                    }
                    else
                    {                        
                        txtVesselName.Text = criteria.Vessel;
                        txtVoyageNo.Text = criteria.Voyage;
                        txtLocation.Text = criteria.Location;
                        txtTerminal.Text = criteria.Terminal;
                        gvVoyage.PageIndex = criteria.PageIndex;
                        gvVoyage.PageSize = criteria.PageSize;
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
            string sortExpression = "VoyageNo";
            string sortDirection = "ASC";
            criteria.CurrentPage = PageName.ImportBL;
            criteria.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;

            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
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
                sortExpression = "VoyageNo";
                sortDirection = "ASC";
            }            
            criteria.UserId = _userId;
            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.Vessel = (txtVesselName.Text == "") ? string.Empty : txtVesselName.Text.Trim();
            criteria.Voyage = (txtVoyageNo.Text == "") ? string.Empty : txtVoyageNo.Text.Trim();
            criteria.Location = (txtLocation.Text == "") ? string.Empty : txtLocation.Text.Trim();
            criteria.Terminal = (txtTerminal.Text == "") ? string.Empty : txtTerminal.Text.Trim();

            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            LoadVoyage();
            upVoyage.Update();
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
        private void LoadVoyage()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);
                    gvVoyage.PageIndex = searchCriteria.PageIndex;
                    searchCriteria.VoyageID = -1;
                    if (searchCriteria.PageSize > 0) gvVoyage.PageSize = searchCriteria.PageSize;
                    gvVoyage.DataSource = new expVoyageBLL().GetVoyage(searchCriteria);
                    gvVoyage.DataBind();
                }
            }
        }
        private void DeleteVoyage(int voyageID)
        {
            int voyageid = new expVoyageBLL().DeleteVoyage(voyageID);           
            LoadVoyage();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00006") + "');</script>", false);
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
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            IUser user = new UserBLL().GetUser(_userId);

            if (!ReferenceEquals(user, null))
            {
                if (!ReferenceEquals(user.UserRole, null))
                {
                    _roleId = user.UserRole.Id;
                }

            }
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
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

                gvVoyage.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
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
        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Export/ManageVoyage.aspx?VoyageID=" + encryptedId);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtVoyageNo.Text = "";
            txtVesselName.Text = "";
            txtTerminal.Text = "";
            txtLocation.Text = "";

            SaveNewPageIndex(0);
            LoadVoyage();
            upVoyage.Update();
        }
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            LoadVoyage();
            upVoyage.Update();
        }
        protected void gvVoyage_RowCommand(object sender, GridViewCommandEventArgs e)
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
                LoadVoyage();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteVoyage(Convert.ToInt32(e.CommandArgument));
            }
        }
        protected void gvVoyage_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvVoyage.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadVoyage();
        }
        protected void gvVoyage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 6);
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvVoyage.PageSize * gvVoyage.PageIndex) + e.Row.RowIndex + 1).ToString();
                //need to be changed
                //e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TerminalID"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VesselName"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VoyageNo"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LoadPort"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "NextPort"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TerminalName"));

                ////Edit Link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00070");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VoyageID"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VoyageID"));

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
            }
        }
      





























    
      
      
        
        
      
    }

}