using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.View
{
    public partial class ManageRole : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
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
                RetrieveSearchCriteria();
                LoadRole();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            LoadRole();
            upRole.Update();
        }

        protected void gvwRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwRole.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadRole();
        }
        protected void gvwRole_RowCommand(object sender, GridViewCommandEventArgs e)
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

                LoadRole();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            //else if (e.CommandName == "Remove")
            //{
            //    DeleteRole(Convert.ToInt32(e.CommandArgument));
            //}
            else if (e.CommandName == "Status")
            {
                string[] arg = Convert.ToString(e.CommandArgument).Split('|');

                if (arg.Length > 1)
                {
                    if (Convert.ToInt32(arg[1]) == 1)
                        ChangeRoleStatus(Convert.ToInt32(arg[0]), true);
                    else
                        ChangeRoleStatus(Convert.ToInt32(arg[0]), false);
                }
            }
        }

        protected void gvwRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 4);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwRole.PageSize * gvwRole.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Name"));

                LinkButton lnkStatus = (LinkButton)e.Row.FindControl("lnkStatus");
                lnkStatus.CommandName = "Status";
                lnkStatus.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00074") + "');";

                if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Active")))
                {
                    lnkStatus.Text = "Active";
                    lnkStatus.ToolTip = "Click to deactivate";
                    lnkStatus.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id")) + "|0";
                }
                else
                {
                    lnkStatus.Text = "Inactive";
                    lnkStatus.ToolTip = "Click to activate";
                    lnkStatus.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id")) + "|1";
                }

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00013");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id"));

                ////Delete link
                //ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                //btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00012");
                //btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id"));

                //if (_hasEditAccess)
                //{
                    //btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";
                //}
                //else
                //{
                //    btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                    //btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                //}
            }
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            LoadRole();
            upRole.Update();
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
                    Response.Redirect("~/Unauthorized.aspx");
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
            txtWMERole.WatermarkText = ResourceManager.GetStringWithoutName("ERR00064");

            //gvwRole.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            gvwRole.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
        }

        private void LoadRole()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);
                    UserBLL userBll = new UserBLL();

                    gvwRole.PageIndex = searchCriteria.PageIndex;
                    if (searchCriteria.PageSize > 0) gvwRole.PageSize = searchCriteria.PageSize;

                    gvwRole.DataSource = userBll.GetAllRole(searchCriteria);
                    gvwRole.DataBind();
                }
            }
        }

        //private void DeleteRole(int roleId)
        //{
        //    UserBLL userBll = new UserBLL();
        //    userBll.DeleteRole(roleId, _userId);
        //    LoadRole();
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00010") + "');</script>", false);
        //}

        private void ChangeRoleStatus(int roleId, bool status)
        {
            new UserBLL().ChangeRoleStatus(roleId, status, _userId);
            LoadRole();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Status successfully changed');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/View/AddEditRole.aspx?id=" + encryptedId);
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
                sortExpression = "Role";
                sortDirection = "ASC";
            }

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.RoleName = (txtRole.Text == ResourceManager.GetStringWithoutName("ERR00064")) ? string.Empty : txtRole.Text.Trim();
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
                        txtRole.Text = criteria.RoleName;
                        gvwRole.PageIndex = criteria.PageIndex;
                        gvwRole.PageSize = criteria.PageSize;
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