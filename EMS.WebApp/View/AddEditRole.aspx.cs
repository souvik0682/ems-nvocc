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

namespace EMS.WebApp.View
{
    public partial class AddEditRole : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _roleId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        private enum MainMenuItem
        {
            Home = 1,
            Master = 2,
            Import = 17,
            Finance = 40,
            Logistic = 66,
            Export = 92,
            Forwarding = 146,
            Change_Password = 110
        };

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void gvwMst_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopulateControlsForGridView(gvwMst, e.Row);
            }
        }

        protected void gvwImp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopulateControlsForGridView(gvwImp, e.Row);
            }
        }

        protected void gvwFin_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopulateControlsForGridView(gvwFin, e.Row);
            }
        }

        protected void gvwLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopulateControlsForGridView(gvwLog, e.Row);
            }
        }

        protected void gvwExp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopulateControlsForGridView(gvwExp, e.Row);
            }
        }

        protected void gvwFwd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopulateControlsForGridView(gvwFwd, e.Row);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveRole();
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                if (_roleId == -1) //Add mode
                {
                    if (!_canAdd) btnSave.Visible = false;
                }
                else
                {
                    if (!_canEdit) btnSave.Visible = false;
                }

                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageRole.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                rfvRole.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00052");
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();
            
            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _roleId);
            }
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

            if (_roleId == 0)
                Response.Redirect("~/View/ManageRole.aspx");
        }

        private void PopulateControlsForGridView(GridView gvw, GridViewRow row)
        {
            GeneralFunctions.ApplyGridViewAlternateItemStyle(row, 6);

            //ScriptManager sManager = ScriptManager.GetCurrent(this);

            ((Label)row.FindControl("lblSlNo")).Text = ((gvw.PageSize * gvw.PageIndex) + row.RowIndex + 1).ToString();
            ((HiddenField)row.FindControl("hdnAccessId")).Value = Convert.ToString(DataBinder.Eval(row.DataItem, "MenuAccessID"));
            ((HiddenField)row.FindControl("hdnMenuId")).Value = Convert.ToString(DataBinder.Eval(row.DataItem, "MenuID"));

            row.Cells[1].Text = Convert.ToString(DataBinder.Eval(row.DataItem, "MenuName"));

            //Add
            if (Convert.ToBoolean(DataBinder.Eval(row.DataItem, "CanAdd")))
                ((CheckBox)row.FindControl("chkAdd")).Checked = true;
            else
                ((CheckBox)row.FindControl("chkAdd")).Checked = false;

            //Edit
            if (Convert.ToBoolean(DataBinder.Eval(row.DataItem, "CanEdit")))
                ((CheckBox)row.FindControl("chkEdit")).Checked = true;
            else
                ((CheckBox)row.FindControl("chkEdit")).Checked = false;

            //Delete
            if (Convert.ToBoolean(DataBinder.Eval(row.DataItem, "CanDelete")))
                ((CheckBox)row.FindControl("chkDel")).Checked = true;
            else
                ((CheckBox)row.FindControl("chkDel")).Checked = false;

            //View
            if (Convert.ToBoolean(DataBinder.Eval(row.DataItem, "CanView")))
                ((CheckBox)row.FindControl("chkView")).Checked = true;
            else
                ((CheckBox)row.FindControl("chkView")).Checked = false;
        }

        private void LoadData()
        {
            UserBLL userBll = new UserBLL();
            IRole role = userBll.GetRole(_roleId);

            if (!ReferenceEquals(role, null))
            {
                txtRole.Text=role.Name;
            }

            gvwMst.DataSource = userBll.GetMenuByRole(_roleId, (int)MainMenuItem.Master);
            gvwMst.DataBind();

            gvwImp.DataSource = userBll.GetMenuByRole(_roleId, (int)MainMenuItem.Import);
            gvwImp.DataBind();

            gvwFin.DataSource = userBll.GetMenuByRole(_roleId, (int)MainMenuItem.Finance);
            gvwFin.DataBind();

            gvwLog.DataSource = userBll.GetMenuByRole(_roleId, (int)MainMenuItem.Logistic);
            gvwLog.DataBind();

            gvwExp.DataSource = userBll.GetMenuByRole(_roleId, (int)MainMenuItem.Export);
            gvwExp.DataBind();

            gvwFwd.DataSource = userBll.GetMenuByRole(_roleId, (int)MainMenuItem.Forwarding);
            gvwFwd.DataBind();
        }

        private void SaveRole()
        {
            UserBLL userBll = new UserBLL();
            List<RoleMenuEntity> lstRoleMenu = new List<RoleMenuEntity>();
            IRole role = new RoleEntity();

            string message = string.Empty;
            BuildRoleEntity(role);
            BuildRoleMenuEntity(lstRoleMenu);
            message = userBll.SaveRole(lstRoleMenu, role, _userId);

            if (message == string.Empty)
                Response.Redirect("~/View/ManageRole.aspx");
            else
                GeneralFunctions.RegisterAlertScript(this, message);
        }

        private void BuildRoleEntity(IRole role)
        {
            role.Id = _roleId;
            role.Name = txtRole.Text.Trim();
        }

        private void BuildRoleMenu(List<RoleMenuEntity> lstRoleMenu, GridView gvw)
        {
            for (int index = 0; index < gvw.Rows.Count; index++)
            {
                RoleMenuEntity roleMenu = new RoleMenuEntity();

                HiddenField hdnAccessId = (HiddenField)gvw.Rows[index].FindControl("hdnAccessId");
                HiddenField hdnMenuId = (HiddenField)gvw.Rows[index].FindControl("hdnMenuId");
                CheckBox chkAdd = (CheckBox)gvw.Rows[index].FindControl("chkAdd");
                CheckBox chkEdit = (CheckBox)gvw.Rows[index].FindControl("chkEdit");
                CheckBox chkDel = (CheckBox)gvw.Rows[index].FindControl("chkDel");
                CheckBox chkView = (CheckBox)gvw.Rows[index].FindControl("chkView");

                int menuAccessId = 0;
                int menuId = 0;
                int.TryParse(hdnAccessId.Value, out menuAccessId);
                int.TryParse(hdnMenuId.Value, out menuId);

                roleMenu.MenuAccessID = menuAccessId;
                roleMenu.MenuID = menuId;
                roleMenu.CanAdd = chkAdd.Checked;
                roleMenu.CanEdit = chkEdit.Checked;
                roleMenu.CanDelete = chkDel.Checked;
                roleMenu.CanView = chkView.Checked;

                lstRoleMenu.Add(roleMenu);
            }
        }

        private void BuildRoleMenuEntity(List<RoleMenuEntity> lstRoleMenu)
        {
            BuildRoleMenu(lstRoleMenu, gvwMst);
            BuildRoleMenu(lstRoleMenu, gvwImp);
            BuildRoleMenu(lstRoleMenu, gvwFin);
            BuildRoleMenu(lstRoleMenu, gvwLog);
            BuildRoleMenu(lstRoleMenu, gvwExp);
        }

        #endregion
    }
}